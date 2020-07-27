using System;
using System.Collections.Generic;
using QuestionOverdose.BLL.DTO;

namespace QuestionOverdose.DTO
{
    public class QuestionDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int Votes { get; set; }

        public int Views { get; set; }

        public bool IsAnswered { get; set; }

        public UserDTO Author { get; set; }

        public List<TagDTO> Tags { get; set; }

        public List<AnswerDTO> Answers { get; set; }

        public List<VoterDTO> Voters { get; set; }

        public DateTime DateOfPublication { get; set; }
    }
}