using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

public class ContactDetails
{
    [Key]
    public required string ID { get; init; }
    
    [StringLength(10)]
    public required string Title { get; init; }

    [StringLength(200)]
    public required string FirstName { get; init; }

    [StringLength(200)]
    public required string LastName { get; init; }

    [EmailAddress]
    [StringLength(200)]
    public required string Email { get; init; }

    [Phone]
    [StringLength(200)]
    public required string PhoneNumber { get; init; }

    [Phone]
    [StringLength(200)]
    public string? AlternatePhoneNumber { get; set; } 
}