namespace TinyCommerce.BuildingBlocks.Application.Emails
{
    public class EmailMessage
    {
        public EmailMessage(string email, string subject, string content)
        {
            Email = email;
            Subject = subject;
            Content = content;
        }

        public string Email { get; }
        public string Subject { get; }
        public string Content { get; }
    }
}