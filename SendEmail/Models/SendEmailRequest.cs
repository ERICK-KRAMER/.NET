namespace SendEmail.Models
{
    public record SendEmailRequest(string Recipient, string Subject, string Body);
}