using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuestionOverdose.Domain.Entities
{
    public class Answer
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int Votes { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public virtual User Author { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public virtual Question Question { get; set; }

        [Required]
        public DateTime DateOfPublication { get; set; }

        [Required]
        public bool IsAnswer { get; set; }

        public List<Comment> Comments { get; set; }

        public virtual List<UserAnswer> Voters { get; set; }

        public DateTime? DateOfRedacting { get; set; }

        public bool IsDeleted { get; set; }
    }
}