using System.ComponentModel.DataAnnotations;

namespace WhyAmI.Web.Models;

/// <summary>
/// A single individual question in a questionnaire in the WhyAmI application.
/// </summary>
public class Question : Base
{
    /// <summary>
    /// For sorting, this is an integer value to determine order
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Question text, limited to 200 characters.
    /// </summary>
    [StringLength(200)] public required string Text { get; set; }

    /// <summary>
    /// When true, the question is answered as true/false rather than scored options.
    /// </summary>
    public bool HasTrueFalse { get; set; }
}

public sealed record QuestionExample(int Id, string Text, string Hint);
