using System.Collections.Generic;
using QuestionOverdose.Models.QuestionModels;

namespace QuestionOverdose.Interfaces
{
    internal interface IVoter
    {
        public List<VoterModel> Voters { get; set; }

        public bool? IsUpvoted { get; set; }

        public void SetCurrentVoter(int? userId);
    }
}
