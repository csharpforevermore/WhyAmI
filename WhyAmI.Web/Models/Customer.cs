namespace WhyAmI.Web.Models;

/// <summary>
/// Represents a Customer in the WhyAmI application.
/// </summary>
public class Customer : Base, IUser
{
    public ContactDetails ContactDetails { get; set; }
}