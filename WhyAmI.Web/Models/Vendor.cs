using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// Application user model for the WhyAmI application.
/// </summary>
public class Vendor : BaseUser
{
    [Required] [StringLength(200)] public string? About { get; init; }
    [StringLength(200)] public required string Name { get; init; }
    [StringLength(200)] public string Website { get; init; } = "";

}
