namespace QuestionOverdose.Models
{
    public class AuthenticateModel
    {
        public string Nickname { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}