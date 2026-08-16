using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// Represents a questionnaire in the WhyAmI application.
/// </summary>
public class Questionnaire : Base
{
    /// <summary>
    /// Title of the questionnaire, limited to 200 characters.
    /// </summary>
    [StringLength(200)] public required string Title { get; set; }

    /// <summary>
    /// Subtitle text of the questionnaire, limited to 500 characters.
    /// </summary>
    [Display(Name = "Subtitle")]
    [StringLength(500)] public string SubTitle { get; init; } = string.Empty;

    /// <summary>
    /// URL or source of the questionnaire, limited to 200 characters.
    /// </summary>
    [StringLength(200)] public string Source { get; init; } = string.Empty;

    /// <summary>
    /// Prefix questions with True or False checkboxes. This is a convenience property to indicate if any questions in the questionnaire are prefixed / enabled (True/False).
    /// </summary>
    [Display(Name = "Questions have True False conditions?")]
    public bool HasQuestionsWithTrueFalse { get; set; }
    
    /// <summary>
    /// Shared score options for all questions in this questionnaire.
    /// </summary>
    [Display(Name = "Score options")]
    [Required]
    public IList<ScoreOption> ScoreOptions { get; set; } = new List<ScoreOption>();

    /// <summary>
    /// List of questions in the questionnaire.
    /// </summary>
    [Required] public IList<Question> Questions { get; set; } = new List<Question>();

    /// <summary>
    /// Summary description of the questionnaire, limited to 500 characters.
    /// </summary>
    [StringLength(500)]
    public string Description { get; init; } = "";

    /// <summary>
    /// Indicates whether the questionnaire is enabled or disabled. When disabled, it will not be available for users to take.
    /// </summary>
    [Display(Name = "Enabled?")]
    public bool IsEnabled { get; set; } = true;
}
