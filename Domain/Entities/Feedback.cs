using Domain.Primitives;

namespace Domain.Entities;

public sealed class Feedback : Entity
{
    public string Content { get; private set; } = string.Empty;
    public string FeedbackOn { get; private set; } = string.Empty;
    public DateTime FeedbackAt { get; private set; } = DateTime.MinValue;
    public string Status { get; private set; } = string.Empty;
    public bool IsRemoved { get; private set; } = false;
    public DateTime RemovedAt { get; private set; } = DateTime.MinValue;

    /*One -------------------------------------------------*/
    public Guid UserId { get; private set; }
    public User? User { get; private set; }
    public Guid BookId { get; private set; }
    public Book? Book { get; private set; }

    public Feedback() { }
    public class Builder
    {
        private Feedback feedback = new Feedback();

        public Builder SetID(Guid id) { feedback.Id = id; return this; }
        public Builder SetContent(string content) { feedback.Content = content; return this; }
        public Builder SetFeedbackOn(string feedbackOn) { feedback.FeedbackOn = feedbackOn; return this; }
        public Builder SetFeedbackAt(DateTime feedbackAt) { feedback.FeedbackAt = feedbackAt; return this; }
        public Builder SetStatus(string status) { feedback.Status = status; return this; }
        public Builder SetUserId(Guid userId) { feedback.UserId = userId; return this; }
        public Builder SetBookId(Guid bookId) { feedback.BookId = bookId; return this; }
        public Builder SetIsRemoved(bool isRemoved) { feedback.IsRemoved = isRemoved; return this; }
        public Builder SetRemovedAt(DateTime removeAt) { feedback.RemovedAt = removeAt; return this; }

        public Feedback Build() => feedback;
    }
}