using System;
using System.Collections.Generic;
using System.Linq;
using QuestionOverdose.Interfaces;

namespace QuestionOverdose.Models.QuestionModels
{
    public class AnswerModel : IVoter
    {
        public int Id { get; set; }

        public int Votes { get; set; }

        public string Body { get; set; }

        public string AuthorName { get; set; }

        public DateTime DateOfPublication { get; set; }

        public bool IsAnswer { get; set; }

        public List<CommentModel> Comments { get; set; }

        public List<VoterModel> Voters { get; set; }

        public bool? IsUpvoted { get; set; }

        public void SetCurrentVoter(int? userId)
        {
            var voter = Voters.Where(x => x.UserId == userId).FirstOrDefault();
            IsUpvoted = voter?.IsUpvote;
        }
    }
}
