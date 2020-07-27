using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestionOverdose.BLL.Services;
using QuestionOverdose.DTO;
using QuestionOverdose.Models;
using QuestionOverdose.Models.QuestionModels;
using Serilog;

namespace QuestionOverdose.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger _log;
        private readonly QuestionService _questionService;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public QuestionController(
            ILogger logger,
            QuestionService questionService,
            IMapper mapper,
            UserService userService)
        {
            _userService = userService;
            _log = logger;
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpPost, Route("add")]
        public async Task<ActionResult> AddQuestionAsync(QuestionModel questionModel)
        {
            if (questionModel == null)
            {
                _log.Information("Invalid question creating attempt");
                return BadRequest("Invalid question creating attempt");
            }

            var questionDTO = _mapper.Map<QuestionDTO>(questionModel);
            questionDTO.Author = await _userService.GetUserByNick(questionModel.AuthorName);
            await _questionService.CreateQuestion(questionDTO, questionModel.TagNames);
            return Ok();
        }

        [Authorize]
        [HttpPost, Route(template: "subscribe/tag")]
        public async Task<ActionResult> SubscribeOnTagAsync(string tag)
        {
            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var userId = int.Parse(claimValueId!);

            await _userService.SubscribeOnTag(userId, tag);
            _log.Information(messageTemplate: $"User {userId} successfully changed pass");
            return Ok();
        }

        [Authorize]
        [HttpGet, Route("tags")]
        public async Task<ActionResult> GetAllTagsAsync()
        {
            var tagsDTO = await _userService.GetAllTags();
            var tags = _mapper.Map<List<TagModel>>(tagsDTO);
            return Ok(tags);
        }

        [HttpGet, Route("get")]
        public async Task<ActionResult> GetQuestionsAsync(int pageSize, int pageNumber, string tagName)
        {
            var items = await _questionService.GetQuestions(pageNumber, pageSize, tagName);

            var countQuestions = await _questionService.CountQuestions(tagName);
            var questionModel = new QuestionIndexModel()
            {
                PageModel = new PageModel(countQuestions, pageNumber, pageSize),
                QuestionModels = _mapper.Map<List<QuestionModel>>(items)
            };

            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var userId = int.TryParse(claimValueId, out var f) ? f : default(int?);

            questionModel.QuestionModels.ForEach(x => x.SetCurrentVoter(userId));
            return Ok(questionModel);
        }

        [HttpGet, Route("getQuestions/live")]
        public async Task<ActionResult> GetQuestionsLiveAsync(string titleName)
        {
            var questions = await _questionService.GetQuestionsLiveSearch(titleName);
            return Ok(questions);
        }

        [HttpGet, Route("getQuestion")]
        public async Task<ActionResult> GetQuestionAsync(int id)
        {
            var question = await _questionService.GetQuestion(id);
            await _questionService.IncrementViews(id);
            var questionModel = _mapper.Map<QuestionViewModel>(question);
            questionModel.AnswerModels.ForEach(x =>
            {
                x.Comments.ForEach(y => y.CommentChilds = x.Comments
                    .Where(z => z.CommentAncestorId == y.Id)
                    .OrderBy(d => d.DateOfCreation).ToList());

                x.Comments.RemoveAll(d => d.CommentAncestorId != null);
            });

            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var userId = int.TryParse(claimValueId, out var f) ? f : default(int?);
            questionModel.AnswerModels.ForEach(x => x.SetCurrentVoter(userId));

            return Ok(questionModel);
        }

        [Authorize]
        [HttpGet, Route("voteQuestion")]
        public async Task<ActionResult> VoteQuestionAsync(int id, bool isUpvote)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            await _questionService.VoteQuestion(id, isUpvote, int.Parse(userId));
            return Ok();
        }

        [Authorize]
        [HttpGet, Route("voteAnswer")]
        public async Task<ActionResult> VoteAnswerAsync(int id, bool isUpvote)
        {
            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;

            await _questionService.VoteAnswer(id, isUpvote, int.Parse(claimValueId!));
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("answer")]
        public async Task<ActionResult> AddAnswer(int questionId, string answerBody)
        {
            if (string.IsNullOrEmpty(answerBody) && questionId < 1)
            {
                _log.Information("Invalid answer creating attempt");
                return BadRequest("Invalid answer creating attempt");
            }

            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var answerDTO = await _questionService.AddAnswer(questionId, answerBody, int.Parse(claimValueId!));

            return Ok(_mapper.Map<AnswerModel>(answerDTO));
        }

        [Authorize]
        [HttpPost, Route("comment")]
        public async Task<ActionResult> AddComment(int answerId, string body, int commentAncestorId)
        {
            if (string.IsNullOrEmpty(body) && answerId < 1)
            {
                _log.Information("Invalid comment creating attempt");
                return BadRequest("Invalid comment creating attempt");
            }

            var claimValueId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var commentDTO = await _questionService.AddComment(answerId, body, commentAncestorId, int.Parse(claimValueId!));

            return Ok(_mapper.Map<CommentModel>(commentDTO));
        }

        [Authorize]
        [HttpPost, Route("mark/answer")]
        public async Task<ActionResult> MarkAsAnswer(int answerId)
        {
            if (answerId < 1)
            {
                _log.Information("Invalid request of marking as answer");
                return BadRequest("Invalid request of marking as answer");
            }

            await _questionService.MarkAsAnswer(answerId);
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("delete")]
        public async Task<ActionResult> DeleteQuestion(int questionId)
        {
            if (questionId < 1)
            {
                _log.Information("Invalid request of deleting question");
                return BadRequest("Invalid request of deleting question");
            }

            await _questionService.SoftDeleteQuestion(questionId);
            _log.Information($"Successful attempt of soft deleting question {questionId}");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("delete/answer")]
        public async Task<ActionResult> DeleteAnswer(int answerId)
        {
            if (answerId < 1)
            {
                _log.Information("Invalid request of deleting answer");
                return BadRequest("Invalid request of deleting answer");
            }

            await _questionService.SoftDeleteAnswer(answerId);
            _log.Information($"Successful attempt of soft deleting answer {answerId}");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("delete/comment")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {
            if (commentId < 1)
            {
                _log.Information("Invalid request of deleting comment");
                return BadRequest("Invalid request of deleting comment");
            }

            await _questionService.DeleteComment(commentId);
            _log.Information($"Successful attempt of deleting comment {commentId}");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("update")]
        public async Task<ActionResult> UpdateQuestion(int questionId, string body)
        {
            if (questionId < 1)
            {
                _log.Information("Invalid request of updating question");
                return BadRequest("Invalid request of updating question");
            }

            await _questionService.UpdateQuestion(questionId, body);
            _log.Information($"Successful attempt of updating comment {questionId}");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("update/answer")]
        public async Task<ActionResult> UpdateAnswer(int answerId, string body)
        {
            if (answerId < 1)
            {
                _log.Information("Invalid request of updating question");
                return BadRequest("Invalid request of updating question");
            }

            await _questionService.UpdateAnswer(answerId, body);
            _log.Information($"Successful attempt of updating comment {answerId}");
            return Ok();
        }

        [Authorize]
        [HttpPost, Route("update/comment")]
        public async Task<ActionResult> UpdateComment(int commentId, string body)
        {
            if (commentId < 1)
            {
                _log.Information("Invalid request of updating question");
                return BadRequest("Invalid request of updating question");
            }

            await _questionService.UpdateComment(commentId, body);
            _log.Information($"Successful attempt of updating comment {commentId}");
            return Ok();
        }
    }
}