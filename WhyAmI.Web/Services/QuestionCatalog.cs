using WhyAmI.Web.Models;

namespace WhyAmI.Web.Services;

public sealed class QuestionCatalog : IQuestionCatalog
{
    static string defaultHint = "0 - 4";
    
    public IReadOnlyList<QuestionExample> Questions { get; } =
    [
        new(0, "What is your name?", "This is used only to personalize the questionnaire."),
        new(1, "1. I feel uninterested in the things I used to enjoy. ", defaultHint),
        new(2, "2. I often seem to live through events exactly as they happened before (déjà vu). ", defaultHint),
        new(3, "3. I sometimes smell or taste things that other people can’t smell or taste.", defaultHint),
        new(4, "4. I often hear unusual sounds like banging, clicking, hissing, clapping or ringing in my ears. ", defaultHint),
        new(5, "5. I have been confused at times whether something I experienced was real or imaginary. ", defaultHint),
        new(6, "6. When I look at a person, or look at myself in a mirror, I have seen the face change right before my eyes. ", defaultHint),
        new(7, "7. I get extremely anxious when meeting people for the first time. ", defaultHint),
        new(8, "8. I have seen things that other people apparently can't see.", defaultHint),
        new(9, "9. My thoughts are sometimes so strong that I can almost hear them. ", defaultHint),
        new(10, "10. I sometimes see special meanings in advertisements, shop windows, or in the way things are arranged around me. ", defaultHint),
        new(11, "11. Sometimes I have felt that I’m not in control of my own ideas or thoughts. ", defaultHint),
        new(12, "12. Sometimes I feel suddenly distracted by distant sounds that I am not normally aware of. ", defaultHint),
        new(13, "13. I have heard things other people can't hear like voices of people whispering or talking.", defaultHint),
        new(14, "14. I often feel that others have it in for me. ", defaultHint),
        new(15, "15. I have had the sense that some person or force is around me, even though I could not see anyone. ", defaultHint),
        new(16, "16. I feel that parts of my body have changed in some way, or that parts of my body are working differently than before.", defaultHint)
    ];
}
