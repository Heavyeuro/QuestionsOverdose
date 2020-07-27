using System.Collections.Generic;

namespace QuestionOverdose.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public bool IsEmailVerified { get; set; }

        public string Password { get; set; }

        public RoleDTO UserRole { get; set; }

        public List<TagDTO> SubscribedTags { get; set; }
    }
}