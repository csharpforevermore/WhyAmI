using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;


/// <summary>
/// Represents a CustomerQuestionnaire session in the application.
/// </summary>
public class CustomerQuestionnaireResults : Base
{
    [Required] public IList<Question> Questions { get; init; } = new List<Question>();
    [Required] public IList<Answer> Answers { get; init; } = new List<Answer>();

    public int TotalScore { get; init; } 

    public required Customer Customer { get; init; }

    public IList<Vendor> Vendors { get; init; } = new List<Vendor>();
}