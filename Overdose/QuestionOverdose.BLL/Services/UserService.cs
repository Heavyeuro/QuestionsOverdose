using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Repositories;
using QuestionOverdose.Domain.Email;
using QuestionOverdose.Domain.Entities;
using QuestionOverdose.DTO;

namespace QuestionOverdose.BLL.Services
{
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TagRepository _tagRepository;
        private readonly UserTagRepository _userTag;
        private readonly UserRepository _userRepository;
        private readonly RoleRepository _roleRepository;
        private readonly EmailSender _emailSender;

        public UserService(
            UnitOfWork unitOfWork,
            IMapper mapper,
            UserRepository userRepository,
            RoleRepository roleRepository,
            EmailSender emailSender,
            UserTagRepository userTag,
            TagRepository tagRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _emailSender = emailSender;
            _userTag = userTag;
            _tagRepository = tagRepository;
        }

        // UserDTO with Tag property mapped.
        public async Task<UserDTO> GetUserByNickAndPass(string username, string pass)
        {
            var user = await _userRepository.GetUserByNickAndPass(username, pass);
            var userDTO = _mapper.Map<UserDTO>(user);

            var userTags = await _userTag.GetByUserIdAsync(userDTO.Id);
            userTags.ForEach(x => userDTO.SubscribedTags.Add(_mapper.Map<TagDTO>(x.Tag)));
            return userDTO;
        }

        public async Task<UserDTO> GetUserByNick(string username)
        {
            var user = await _userRepository.GetUserByNick(username);
            var userDTO = _mapper.Map<UserDTO>(user);

            return userDTO;
        }

        public async Task<UserDTO> GetUserById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> CreateUser(UserDTO userDTO)
        {
            if (userDTO == null)
            {
                return false;
            }

            // Set initial data
            var user = _mapper.Map<User>(userDTO);
            user.UserRole = await _roleRepository.GetByIdAsync(1);
            user.IsEmailVerified = false;

            await _userRepository.CreateAsync(user);
            await _unitOfWork.Context.SaveChangesAsync();
            return true;
        }

        public async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            const string content = "Thanks for creating an account with us.\n" +
                                   "To continue please verify your email with us.\n" +
                                   "Verify Email";

            return await _emailSender.SendGeneralEmailAsync(
                new SendEmailDetails
                {
                ToEmail = email,
                ToName = displayName,
                Subject = "Verify Your Email",
                Content = content
                },
                verificationUrl);
        }

        public async Task ConfirmEmailAsync(UserDTO user)
        {
            user.IsEmailVerified = true;
            await _unitOfWork.Commit();
        }

        public async Task Update(UserDTO userDTO)
        {
            var user = _mapper.Map<User>(userDTO);
            _userRepository.Update(user);
            await _unitOfWork.Commit();
        }

        public ClaimsPrincipal GetPrincipal(string token, string securityKey)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                if (!(tokenHandler.ReadToken(token) is JwtSecurityToken jwtToken))
                {
                    return null;
                }

                var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey));

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = secretKey
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

                return principal;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task SubscribeOnTag(int userId, string tagName)
        {
            var tag = await _tagRepository.GetByNameAsync(tagName);

            await _userTag.CreateAsync(new UserTag { TagId = tag.Id, UserId = userId });

            await _unitOfWork.Commit();
        }

        public async Task<List<TagDTO>> GetAllTags()
        {
            var tags = await _tagRepository.FindAllAsync();

            return _mapper.Map<List<TagDTO>>(tags);
        }
    }
}