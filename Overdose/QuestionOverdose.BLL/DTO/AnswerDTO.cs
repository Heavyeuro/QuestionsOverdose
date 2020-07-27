using System;
using System.Collections.Generic;
using QuestionOverdose.BLL.DTO;

namespace QuestionOverdose.DTO
{
    public class AnswerDTO
    {
        public int Id { get; set; }

        public int Votes { get; set; }

        public string Body { get; set; }

        public virtual UserDTO Author { get; set; }

        public virtual QuestionDTO Question { get; set; }

        public DateTime DateOfPublication { get; set; }

        public bool IsAnswer { get; set; }

        public List<CommentDTO> Comments { get; set; }

        public List<VoterDTO> Voters { get; set; }
    }
}