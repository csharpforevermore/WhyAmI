using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// Editable survey definition. Completed responses snapshot this data rather than joining it.
/// </summary>
public class Survey : Base
{
    [StringLength(200)]
    public required string Title { get; set; }

    [Display(Name = "Subtitle")]
    [StringLength(500)]
    public string SubTitle { get; set; } = string.Empty;

    [StringLength(200)]
    public string Source { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Enabled?")]
    public bool IsEnabled { get; set; } = true;

    [Display(Name = "Score options")]
    [Required]
    public IList<SurveyScoreOption> ScoreOptions { get; set; } = new List<SurveyScoreOption>();

    [Required]
    public IList<SurveyQuestion> Questions { get; set; } = new List<SurveyQuestion>();
}
