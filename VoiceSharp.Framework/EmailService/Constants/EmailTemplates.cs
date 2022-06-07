using VoiceSharp.Framework.EmailService.Models;

namespace VoiceSharp.Framework.EmailService.Constants;

public static class EmailTemplates
{
    public static EmailTemplate Default { get; }
    public static EmailTemplate ResetPassword { get; }
    public static EmailTemplate QuizLink { get; }
    public static EmailTemplate QuizResult { get; }

    static EmailTemplates()
    {
        Default = new EmailTemplate
        {
            Body = "{0}",
            Layout = EmailLayouts.Default,
            Subject = "Default",
        };
        ResetPassword = new EmailTemplate
        {
            Body = "<a href='{0}'> Reset</a> - click to reset password",
            Layout = EmailLayouts.Default,
            Subject = "Voice Sharp - ResetPassword",
        };
        QuizLink  = new EmailTemplate
        {
            Body = "<a href='{0}'> Link</a> - click to start quiz {1}",
            Layout = EmailLayouts.Default,
            Subject = "Voice Sharp - Please take the quiz",
        };
        QuizResult  = new EmailTemplate
        {
            Body = "Your Token: {0} <br> Your Identifier: {1} <br> For find your vote in the system, you must enter this data <br> Link for quiz result (will active when quiz ends): <a href='{2}'> Link</a>",
            Layout = EmailLayouts.Default,
            Subject = "Voice Sharp - Please take the quiz",
        };
    }
}