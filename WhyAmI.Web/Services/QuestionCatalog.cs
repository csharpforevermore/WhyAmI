using WhyAmI.Web.Models;

namespace WhyAmI.Web.Services;

public sealed class QuestionCatalog : IQuestionCatalog
{
    public IReadOnlyList<QuestionExample> Questions { get; } =
    [
        new(1, "What is your name?", "This is used only to personalize the summary."),
        new(2, "Which team do you work on?", "For example: Platform, Payments, or Support."),
        new(3, "What is the main problem you want this process to solve?", "One or two sentences is enough."),
        new(4, "How would you rate this wizard so far?", "Use a number from 1 (poor) to 5 (excellent).")
    ];
}
