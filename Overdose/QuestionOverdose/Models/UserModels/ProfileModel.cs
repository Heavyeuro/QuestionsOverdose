using System.Collections.Generic;

namespace QuestionOverdose.Models
{
    public class ProfileModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool IsEmailVerified { get; set; }

        public List<string> SubscribedTags { get; } = new List<string>();
    }
}
