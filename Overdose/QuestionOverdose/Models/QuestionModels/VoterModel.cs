using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionOverdose.Models.QuestionModels
{
    public class VoterModel
    {
        public int UserId { get; set; }

        public bool IsUpvote { get; set; }
    }
}
