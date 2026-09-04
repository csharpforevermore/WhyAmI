using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// One answered question on an archived survey response, including when it was answered.
/// </summary>
public class SurveyResponseAnswer : Base
{
    public Guid SurveyResponseId { get; set; }

    public SurveyResponse SurveyResponse { get; set; } = null!;

    public Guid? SourceQuestionId { get; set; }

    public SurveyQuestion? SourceQuestion { get; set; }

    public int QuestionNumber { get; set; }

    [StringLength(200)]
    public required string QuestionText { get; set; }

    [Range(1, 5)]
    public int Score { get; set; }

    [StringLength(200)]
    public required string AnswerText { get; set; }

    public DateTimeOffset AnsweredAt { get; set; }
}
