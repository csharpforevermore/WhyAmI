using WhyAmI.Web.Models;

namespace WhyAmI.Web.Services;

public interface IQuestionCatalog
{
    IReadOnlyList<QuestionExample> Questions { get; }
}
