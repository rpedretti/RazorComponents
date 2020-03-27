using RPedretti.RazorComponents.I18n.Models;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Translation
{
    public static class TranslationPageCms
    {
        public static readonly ITranslation<int> P1 = new Translation<int> { Key = 1, Text = "Some Default Text" };
        public static readonly ITranslation<int> P2 = new Translation<int> { Key = 2, Text = "Some Default Other Text" };
    }
}
