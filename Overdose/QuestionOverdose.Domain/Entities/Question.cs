using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionOverdose.Domain.Entities
{
    public class Question
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public int Votes { get; set; }

        public int Views { get; set; }

        [Required]
        public bool IsAnswered { get; set; }

        [Required]
        public int AuthorId { get; set; }

        public virtual User Author { get; set; }

        public virtual List<Answer> Answers { get; set; }

        public DateTime DateOfPublication { get; set; }

        public virtual List<QuestionTag> QuestionsTags { get; set; }

        public virtual List<UserQuestion> Voters { get; set; }

        public DateTime? DateOfRedacting { get; set; }

        public bool IsDeleted { get; set; }
    }
}