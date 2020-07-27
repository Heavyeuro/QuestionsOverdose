using System;
using System.Collections.Generic;
using System.Linq;
using QuestionOverdose.Interfaces;

namespace QuestionOverdose.Models.QuestionModels
{
    public class QuestionModel : IVoter
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string AuthorName { get; set; }

        public int Votes { get; set; }

        public int Views { get; set; }

        public bool IsAnswered { get; set; }

        public List<string> TagNames { get; set; }

        public DateTime DateOfPublication { get; set; }

        public List<VoterModel> Voters { get; set; }

        public bool? IsUpvoted { get; set; }

        public void SetCurrentVoter(int? userId)
        {
            var voter = Voters.Where(x => x.UserId == userId).FirstOrDefault();
            IsUpvoted = voter?.IsUpvote;
        }
    }
}
