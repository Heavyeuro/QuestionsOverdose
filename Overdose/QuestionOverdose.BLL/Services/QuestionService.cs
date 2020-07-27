using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using QuestionOverdose.DAL.EF;
using QuestionOverdose.DAL.Repositories;
using QuestionOverdose.Domain.Entities;
using QuestionOverdose.DTO;

namespace QuestionOverdose.BLL.Services
{
    public class QuestionService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly TagRepository _tagRepository;
        private readonly QuestionRepository _questionRepository;
        private readonly QuestionTagRepository _questionTagRepository;
        private readonly UserRepository _userRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly UserQuestionRepository _userQuestionRepository;
        private readonly UserAnswerRepository _userAnswerRepository;
        private readonly CommentRepository _commentRepository;

        public QuestionService(
            UnitOfWork unitOfWork,
            IMapper mapper,
            TagRepository tagRepository,
            QuestionRepository questionRepository,
            QuestionTagRepository questionTagRepository,
            UserRepository userRepository,
            AnswerRepository answerRepository,
            UserQuestionRepository userQuestionRepository,
            UserAnswerRepository userAnswerRepository,
            CommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
            _userAnswerRepository = userAnswerRepository;
            _userQuestionRepository = userQuestionRepository;
            _answerRepository = answerRepository;
            _userRepository = userRepository;
            _questionTagRepository = questionTagRepository;
            _questionRepository = questionRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateQuestion(QuestionDTO questionDTO, List<string> tagNames)
        {
            var question = _mapper.Map<Question>(questionDTO);

            question.QuestionsTags = new List<QuestionTag>();
            question.Author = await _userRepository.GetByIdAsync(question.Author.Id);

            await _questionRepository.CreateAsync(question);
            var questionTags = new List<QuestionTag>();
            tagNames.ForEach(x => questionTags
                 .Add(new QuestionTag()
                 {
                     TagId = _tagRepository.GetByName(x).Id,
                     Question = question
                 }));

            questionTags.ForEach(x => _questionTagRepository.Create(x));

            await _unitOfWork.Commit();
        }

        public async Task<int> CountQuestions(string tagName = null)
        {
            return await _questionRepository.CountAsync(tagName);
        }

        public async Task<List<QuestionDTO>> GetQuestions(int page, int pageSize, string tagName = null)
        {
            var questions = await _questionRepository.GetPartlyAsync((page - 1) * pageSize, pageSize, tagName);

            return _mapper.Map<List<QuestionDTO>>(questions);
        }

        public async Task<QuestionDTO> GetQuestion(int id)
        {
            var questions = await _questionRepository.GetQuestionById(id);
            return _mapper.Map<QuestionDTO>(questions);
        }

        public async Task VoteQuestion(int questionId, bool isUpvote, int userId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            if (isUpvote)
            {
                question.Votes++;
            }
            else
            {
                question.Votes--;
            }

            await _userQuestionRepository.CreateAsync(new UserQuestion()
            {
                IsUpvote = isUpvote,
                QuestionId = questionId,
                UserId = userId
            });
            _questionRepository.Update(question);

            await _unitOfWork.Commit();
        }

        public async Task VoteAnswer(int answerId, bool isUpvote, int userId)
        {
            var answer = await _answerRepository.GetByIdAsync(answerId);
            if (isUpvote)
            {
                answer.Votes++;
            }
            else
            {
                answer.Votes--;
            }

            await _userAnswerRepository.CreateAsync(new UserAnswer()
            {
                IsUpvote = isUpvote,
                AnswerId = answerId,
                UserId = userId
            });
            _answerRepository.Update(answer);

            await _unitOfWork.Commit();
        }

        public async Task IncrementViews(int questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            question.Views++;
            _questionRepository.Update(question);

            await _unitOfWork.Commit();
        }

        public async Task<AnswerDTO> AddAnswer(int questionId, string body, int userId)
        {
            var dateTime = DateTime.Now;
            var answer = new Answer()
            {
                AuthorId = userId,
                IsAnswer = false,
                QuestionId = questionId,
                Votes = 0,
                DateOfPublication = dateTime,
                Body = body
            };
            await _answerRepository.CreateAsync(answer);
            await _unitOfWork.Commit();
            answer = await _answerRepository.GetByDate(userId, dateTime);
            return _mapper.Map<AnswerDTO>(answer);
        }

        public async Task<CommentDTO> AddComment(int answerId, string body, int? commentAncestorId, int userId)
        {
            var dateTime = DateTime.Now;
            var comment = new Comment()
            {
                AuthorId = userId,
                DateOfCreation = dateTime,
                Body = body,
                AnswerId = answerId,
                CommentAncestorId = commentAncestorId == 0 ? null : commentAncestorId,
            };
            await _commentRepository.CreateAsync(comment);
            await _unitOfWork.Commit();

            comment = await _commentRepository.GetByDate(userId, dateTime);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task MarkAsAnswer(int answerId)
        {
            var answer = await _answerRepository.GetByIdAsync(answerId);
            var question = await _questionRepository.GetByIdAsync(answer.QuestionId);

            answer.IsAnswer = true;
            question.IsAnswered = true;
            _answerRepository.Update(answer);
            await _unitOfWork.Commit();
        }

        public async Task SoftDeleteQuestion(int questionId)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            question.IsDeleted = true;
            _questionRepository.Update(question);
            await _unitOfWork.Commit();
        }

        public async Task SoftDeleteAnswer(int answerId)
        {
            var answer = await _answerRepository.GetByIdAsync(answerId);
            if (answer.IsAnswer)
            {
                var question = await _questionRepository.GetByIdAsync(answer.QuestionId);
                question.IsAnswered = false;
                _questionRepository.Update(question);
            }

            answer.IsDeleted = true;
            _answerRepository.Update(answer);
            await _unitOfWork.Commit();
        }

        public async Task DeleteComment(int commentId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            _commentRepository.Delete(comment);
            await _unitOfWork.Commit();
        }

        public async Task UpdateComment(int commentId, string body)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            comment.Body = body;
            _commentRepository.Update(comment);
            await _unitOfWork.Commit();
        }

        public async Task UpdateAnswer(int answerId, string body)
        {
            var answer = await _answerRepository.GetByIdAsync(answerId);
            answer.Body = body;
            _answerRepository.Update(answer);
            await _unitOfWork.Commit();
        }

        public async Task UpdateQuestion(int questionId, string body)
        {
            var question = await _questionRepository.GetByIdAsync(questionId);
            question.Body = body;
            _questionRepository.Update(question);
            await _unitOfWork.Commit();
        }

        public async Task<List<QuestionDTO>> GetQuestionsLiveSearch(string titleName)
        {
            var questions = await _questionRepository.GetQuestionsLiveSearchAsync(titleName);
            return _mapper.Map<List<QuestionDTO>>(questions);
        }
    }
}
