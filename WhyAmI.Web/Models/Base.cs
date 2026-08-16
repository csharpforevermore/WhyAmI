using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

public abstract class Base
{
    [Key] public  Guid Id { get; set; }
}