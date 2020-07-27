using System;
using System.Collections.Generic;
using System.Text;

namespace QuestionOverdose.BLL.Email
{
    public class SendGridResponse
    {
        // Any errors from a response
        public List<SendGridResponseError> Errors { get; set; }
    }

    public class SendGridResponseError
    {
        public string Message { get; set; }

        public string Field { get; set; }

        public string Help { get; set; }
    }
}
