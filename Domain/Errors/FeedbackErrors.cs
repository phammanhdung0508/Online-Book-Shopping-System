namespace Domain.Errors;

public static class FeedbackErrors
{
    public static readonly Error NotFound
        = new("Feedback.NotFound", "Can't not found any feedbacks.");
    public static readonly Error ContentIsEmptyOrNull 
        = new("Feedback.ContentIsEmptyOrNull", "Can't leave content empty or null.");
    public static readonly Error FeedbackOnIsEmptyOrNull
        = new("Feedback.FeedbackOnIsEmptyOrNull", "Can't leave feedback on empty or null.");
}
