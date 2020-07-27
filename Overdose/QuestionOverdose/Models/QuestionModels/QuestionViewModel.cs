using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionOverdose.Models.QuestionModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public int Votes { get; set; }

        public int Views { get; set; }

        public bool IsAnswered { get; set; }

        public string AuthorName { get; set; }

        public List<string> TagNames { get; set; }

        public DateTime DateOfPublication { get; set; }

        public List<AnswerModel> AnswerModels { get; set; }
    }
}
