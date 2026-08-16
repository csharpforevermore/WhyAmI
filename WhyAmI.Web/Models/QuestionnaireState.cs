namespace WhyAmI.Web.Models;

public sealed class QuestionnaireState
{
    public int CurrentIndex { get; set; }
    public bool IsComplete { get; set; }
    public Dictionary<int, string> Answers { get; set; } = [];
}
