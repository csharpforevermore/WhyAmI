using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// A question on an editable survey definition.
/// </summary>
public class SurveyQuestion : Base
{
    public Guid SurveyId { get; set; }

    public Survey Survey { get; set; } = null!;

    public int Number { get; set; }

    [StringLength(200)]
    public required string Text { get; set; }
}
