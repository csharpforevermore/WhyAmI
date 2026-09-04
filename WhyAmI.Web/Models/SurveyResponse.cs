using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// Archived taking of a survey. Snapshots definition data so later survey edits do not rewrite history.
/// </summary>
public class SurveyResponse : Base
{
    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public Guid? SurveyId { get; set; }

    public Survey? Survey { get; set; }

    [StringLength(200)]
    public required string SurveyTitle { get; set; }

    [StringLength(500)]
    public string SurveyDescription { get; set; } = string.Empty;

    /// <summary>
    /// The 1–5 labels as they were when this response was recorded.
    /// </summary>
    public IList<SurveyScoreOption> ScoreOptionsSnapshot { get; set; } = new List<SurveyScoreOption>();

    public DateTimeOffset StartedAt { get; set; }

    public DateTimeOffset? CompletedAt { get; set; }

    public int TotalScore { get; set; }

    public IList<SurveyResponseAnswer> Answers { get; set; } = new List<SurveyResponseAnswer>();
}
