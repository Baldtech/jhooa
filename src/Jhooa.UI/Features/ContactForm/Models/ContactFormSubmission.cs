namespace Jhooa.UI.Features.ContactForm.Models;

public class ContactFormSubmission
{
    public const int SenderMaxLength = 100;
    public const int SubjectMinLength = 3;
    public const int SubjectMaxLength = 100;
    public const int ContentMinLength = 3;
    public const int ContentMaxLength = 100;
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset SubmissionDate { get; set; } = DateTimeOffset.Now;
    public required string Sender { get; set; }
    public required string Subject { get; set; }
    public required string Content { get; set; }

}