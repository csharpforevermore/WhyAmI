using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

public class ContactDetails
{
    [Key]
    public required string ID { get; init; }
    
    [StringLength(10)]
    public required string Title { get; init; }

    [StringLength(200)]
    [DisplayName("First Name")]
    public required string FirstName { get; init; }

    [StringLength(200)]
    [DisplayName("Last Name")]
    public required string LastName { get; init; }

    [EmailAddress]
    [StringLength(200)]
    public required string Email { get; init; }

    [Phone]
    [DisplayName("Phone Number")]
    [StringLength(200)]
    public required string PhoneNumber { get; init; }

    [Phone]
    [DisplayName("Alternate Phone Number")]
    [StringLength(200)]
    public string? AlternatePhoneNumber { get; set; } 
}