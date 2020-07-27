using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionOverdose.Domain.Email
{
    public class SendEmailDetails
    {
        // The name of the sender
        public string FromName { get; set; }

        // The email of the sender
        public string FromEmail { get; set; }

        // The name of the receiver
        public string ToName { get; set; }

        // The email of the receiver
        public string ToEmail { get; set; }

        // The email subject
        public string Subject { get; set; }

        // The email body content
        public string Content { get; set; }
    }
}
