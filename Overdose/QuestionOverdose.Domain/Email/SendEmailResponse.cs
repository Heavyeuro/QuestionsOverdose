using System.Collections.Generic;

namespace QuestionOverdose.Domain.Email
{
    public class SendEmailResponse
    {
        // Gets a value indicating whether true if the email was sent successfully.
        public bool Successful => !(Errors?.Count > 0);

        // Gets or sets the error message if the sending failed.
        public List<string> Errors { get; set; }
    }
}
