using QuestionOverdose.DTO;

namespace QuestionOverdose.BLL.DTO
{
    public class VoterDTO
    {
        public UserDTO UserDTO { get; set; }

        public bool IsUpvote { get; set; }
    }
}
