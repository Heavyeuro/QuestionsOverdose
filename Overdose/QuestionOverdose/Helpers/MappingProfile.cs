using System.Linq;
using AutoMapper;
using QuestionOverdose.BLL.DTO;
using QuestionOverdose.Domain.Entities;
using QuestionOverdose.DTO;
using QuestionOverdose.Models;
using QuestionOverdose.Models.QuestionModels;
using QuestionOverdose.ViewModels;

namespace QuestionOverdose.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<TagDTO, TagModel>().ReverseMap();
            CreateMap<AnswerDTO, Answer>();
            CreateMap<Answer, AnswerDTO>()
                .ForMember(x => x.Voters, opt => opt.MapFrom(y => y.Voters));
            CreateMap<QuestionDTO, Question>();
            CreateMap<Question, QuestionDTO>()
                .ForMember(x => x.Tags, opt => opt.MapFrom(y => y.QuestionsTags.Select(z => z.Tag)))
                .ForMember(x => x.Voters, opt => opt.MapFrom(y => y.Voters));

            CreateMap<UserAnswer, VoterDTO>()
                .ForMember(x => x.UserDTO, opt => opt.MapFrom(y => y.User));
            CreateMap<UserQuestion, VoterDTO>()
                .ForMember(x => x.UserDTO, opt => opt.MapFrom(y => y.User));
            CreateMap<VoterDTO, VoterModel>()
                .ForMember(x => x.UserId, opt => opt.MapFrom(y => y.UserDTO.Id));

            CreateMap<UserDTO, RegisterModel>()
                .ForMember(x => x.Nickname, opt => opt.MapFrom(y => y.Username))
                .ReverseMap();

            CreateMap<UserDTO, AuthenticateModel>()
                .ForMember(x => x.Role, opt => opt.MapFrom(y => y.UserRole.RoleName))
                .ForMember(x => x.Nickname, opt => opt.MapFrom(y => y.Username))
                .ReverseMap();

            CreateMap<UserDTO, ProfileModel>()
                .ForMember(x => x.SubscribedTags, opt => opt
                .MapFrom(y => y.SubscribedTags.Select(z => z.TagName)
                .ToList()));

            CreateMap<ProfileModel, UserDTO>()
                .ForMember(x => x.SubscribedTags, opt => opt.Ignore());

            CreateMap<QuestionDTO, QuestionModel>()
                .ForMember(x => x.TagNames, opt => opt.MapFrom(y => y.Tags.Select(z => z.TagName)))
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(y => y.Author.Username))
                .ForMember(x => x.Voters, opt => opt.MapFrom(y => y.Voters));

            CreateMap<QuestionModel, QuestionDTO>();

            CreateMap<AnswerDTO, AnswerModel>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(y => y.Author.Username))
                .ForMember(x => x.Comments, opt => opt.MapFrom(y => y.Comments))
                .ForMember(x => x.Voters, opt => opt.MapFrom(y => y.Voters));

            CreateMap<CommentDTO, CommentModel>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(y => y.Author.Username));

            CreateMap<QuestionDTO, QuestionViewModel>()
                .ForMember(x => x.AuthorName, opt => opt.MapFrom(y => y.Author.Username))
                .ForMember(x => x.AnswerModels, opt => opt.MapFrom(y => y.Answers));
        }
    }
}
