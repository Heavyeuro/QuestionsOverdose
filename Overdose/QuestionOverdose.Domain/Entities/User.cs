using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestionOverdose.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsEmailVerified { get; set; }

        [Required]
        public int UserRoleId { get; set; }

        public virtual Role UserRole { get; set; }

        public virtual List<UserTag> SubscribedTags { get; set; }

        public virtual List<UserAnswer> VotedAnswers { get; set; }

        public virtual List<UserQuestion> VotedQuestions { get; set; }
    }
}