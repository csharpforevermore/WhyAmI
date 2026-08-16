using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// A scored answer choice for a questionnaire.
/// </summary>
public class ScoreOption
{
    public int Score { get; set; }

    [StringLength(200)]
    public string? Text { get; set; }
}
