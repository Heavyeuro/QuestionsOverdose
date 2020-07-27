using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuestionOverdose.Domain.Entities
{
    public class UserAnswer
    {
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public int AnswerId { get; set; }

        [Required]
        public Answer Answer { get; set; }

        [Required]
        public bool IsUpvote { get; set; }
    }
}
