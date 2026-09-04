using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// A Likert choice (score 1–5 plus label) shared by all questions on a survey.
/// </summary>
public class SurveyScoreOption
{
    [Range(1, 5)]
    public int Score { get; set; }

    [StringLength(200)]
    public required string Text { get; set; }
}
