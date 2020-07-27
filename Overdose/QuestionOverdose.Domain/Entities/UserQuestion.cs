using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuestionOverdose.Domain.Entities
{
    public class UserQuestion
    {
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        public int QuestionId { get; set; }

        [Required]
        public Question Question { get; set; }

        [Required]
        public bool IsUpvote { get; set; }
    }
}
