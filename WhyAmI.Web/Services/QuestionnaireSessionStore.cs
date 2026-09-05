using WhyAmI.Web.Infrastructure;
using WhyAmI.Web.Models;

namespace WhyAmI.Web.Services;

public sealed class QuestionnaireSessionStore(IHttpContextAccessor httpContextAccessor)
{
    const string SessionKey = "questionnaire";

    private ISession Session =>
        httpContextAccessor.HttpContext?.Session
        ?? throw new InvalidOperationException("No active HTTP context.");

    public QuestionnaireState GetOrCreate() =>
        Session.GetObject<QuestionnaireState>(SessionKey) ?? new QuestionnaireState();

    public void Save(QuestionnaireState state) =>
        Session.SetObject(SessionKey, state);

    public void Clear() => Session.Remove(SessionKey);
}
