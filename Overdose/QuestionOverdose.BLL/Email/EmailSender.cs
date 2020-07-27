using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using QuestionOverdose.BLL.Email;
using QuestionOverdose.Domain.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace QuestionOverdose.BLL.Services
{
    public class EmailSender
    {
        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        // set only via Secret Manager
        public AuthMessageSenderOptions Options { get; }

        public async Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details,  string confirmationUrl)
        {
            // Read the general template from file
            var stream = new FileStream(@"Statics\GeneralTemplate.txt", FileMode.Open);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            string templateText = await reader.ReadToEndAsync();

            // Replace special values with those inside the template
            templateText = templateText.Replace("--Title--", details.Subject)
                                        .Replace("--UserName--", details.ToName)
                                        .Replace("--Content--", details.Content)
                                        .Replace("--confirmationUrl--", confirmationUrl);

            // Set the details content to this template content
            details.Content = templateText;

            return await SendEmailAsync(details.Subject, details.Content, details.ToEmail);
        }

        private async Task<SendEmailResponse> SendEmailAsync(string subject, string message, string email)
        {
            string apiKey = Options.SendGridKey;
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            var bodyResult = await response.Body.ReadAsStringAsync();

            // Deserialize the response
            var sendGridResponse = JsonConvert.DeserializeObject<SendGridResponse>(bodyResult);

            var errorResponse = new SendEmailResponse
            {
                Errors = sendGridResponse?.Errors.Select(f => f.Message).ToList()
            };
            return errorResponse;
        }
    }
}