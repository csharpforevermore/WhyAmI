using System.ComponentModel;

namespace WhyAmI.Web.Models;

public abstract class BaseUser : Base
{
    [DisplayName("Contact Details")]
    public ContactDetails? ContactDetails { get; set; } 
}