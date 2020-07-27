using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuestionOverdose.BLL.Services;
using QuestionOverdose.DTO;
using QuestionOverdose.Helpers;
using QuestionOverdose.Models;
using QuestionOverdose.ViewModels;
using Serilog;

namespace QuestionOverdose.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly AuthHelper _authHelper;
        private readonly UserService _userService;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _config;

        public UserController(
            ILogger logger,
            AuthHelper authHelper,
            UserService userService,
            IMapper mapper,
            IOptions<AppSettings> config)
        {
            _authHelper = authHelper;
            _log = logger;
            _userService = userService;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost, Route("login")]
        public async Task<ActionResult> LoginAsync(AuthenticateModel user)
        {
            if (user == null)
            {
                _log.Information("Invalid Login request");
                return BadRequest("Invalid client request");
            }

            var existingUser = await _userService.GetUserByNickAndPass(user.Nickname, user.Password);

            if (existingUser == null)
            {
                _log.Information("Login attempt of nonexistent user");
                return Unauthorized();
            }

            var token = _authHelper.GetJwtoken(existingUser);
            _log.Information($"User {existingUser.Username} is logged in ");
            user = _mapper.Map<AuthenticateModel>(existingUser);
            user.Token = token;

            return Ok(user);
        }

        [HttpPost, Route("register")]
        public async Task<ActionResult> RegisterAsync([FromBody]RegisterModel user)
        {
            if (user == null)
            {
                _log.Information("Invalid register request");
                return BadRequest("Invalid client request");
            }

            if (await _userService.GetUserByNick(user.Nickname) != null)
            {
                _log.Information("Invalid register request. Nickname already exists");
                return BadRequest("Nickname already exists");
            }

            if (!_authHelper.IsPassStrong(user.Password))
            {
                _log.Information("Invalid register request. Password isn`t enough strong");
                return BadRequest("Password isn`t enough strong");
            }

            var userDTO = _mapper.Map<UserDTO>(user);
            await _userService.CreateUser(userDTO);

            var createdUser = await _userService.GetUserByNick(user.Nickname);
            await SendConfEmailAsync(createdUser);

            _log.Information($"User {user.Nickname} successfully registered");
            return Ok("Please, confirm your email");
        }

        [Authorize]
        [HttpPost, Route("verify/email")]
        public async Task<ActionResult> VerifyEmailAsync(string userId, string emailToken)
        {
            var user = await _userService.GetUserById(int.Parse(userId));

            if (user == null)
            {
                return Content("User not found");
            }

            var appSettings = _config.Value;

            // Verify the email token
            var result = _userService.GetPrincipal(emailToken, appSettings.Secret);

            if (result.Identity.Name != user.Username)
            {
                _log.Information("Invalid Email Verification Token ");
                return Content("Invalid Email Verification Token ");
            }

            await _userService.ConfirmEmailAsync(user);
            _log.Information($"User {user.Username} confirmed email");
            return Content("Email Verified");
        }

        [Authorize]
        [HttpPost, Route("profile")]
        public async Task<ActionResult> GetUserProfileAsync(AuthenticateModel user)
        {
            var userDTO = await _userService.GetUserByNickAndPass(user.Nickname, user.Password);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            var viewUser = _mapper.Map<ProfileModel>(userDTO);
            return Ok(viewUser);
        }

        [Authorize]
        [HttpPost, Route("profile/email")]
        public async Task<ActionResult> ChangeEmailAsync(ProfileModel user)
        {
            var userDTO = await _userService.GetUserByNick(user.Username);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            userDTO.Email = user.Email;
            await _userService.Update(userDTO);
            _log.Information($"User {userDTO.Username} successfully changed email");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("profile/pass")]
        public async Task<ActionResult> ChangePassAsync(ProfileModel user)
        {
            var userDTO = await _userService.GetUserByNick(user.Username);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            if (!_authHelper.IsPassStrong(user.Password))
            {
                _log.Information("Invalid register request. Password isn`t enough strong");
                return BadRequest("Password isn`t enough strong");
            }

            userDTO.Password = user.Password;
            await _userService.Update(userDTO);
            _log.Information($"User {userDTO.Username} successfully changed pass");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("profile/resend/email")]
        public async Task<ActionResult> ResendEmailAsync(ProfileModel profile)
        {
            var userDTO = await _userService.GetUserByNick(profile.Username);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            await SendConfEmailAsync(userDTO);
            _log.Information($"User {userDTO.Username} successfully requested email confirmation");
            return Ok();
        }

        public async Task SendConfEmailAsync(UserDTO createdUser)
        {
            var emailVerificationCode = _authHelper.GetJwtoken(createdUser);
            var confirmationUrl = $"https://{Request.Host.Value}/api/auth/verify/email/?userId="
                + $"{HttpUtility.UrlEncode(createdUser.Id.ToString())}" +
                $"&emailToken={HttpUtility.UrlEncode(emailVerificationCode)}";

            var response = await _userService.SendUserVerificationEmailAsync(
                createdUser.Username,
                createdUser.Email,
                confirmationUrl);

            if (!response.Successful)
            {
                response.Errors.ForEach(x => _log.Error(x));
            }
        }

        [Authorize]
        [HttpPost, Route("profile/name")]
        public async Task<ActionResult> ChangeNameAsync(ProfileModel profile)
        {
            var userDTO = await _userService.GetUserByNick(profile.Username);
            if (userDTO == null)
            {
                return BadRequest("Invalid user data");
            }

            userDTO.FullName = profile.FullName;
            await _userService.Update(userDTO);
            _log.Information($"User {userDTO.Username} successfully changed name");
            return Ok();
        }
    }
}