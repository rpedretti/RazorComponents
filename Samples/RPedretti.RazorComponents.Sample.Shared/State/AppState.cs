using RPedretti.RazorComponents.I18n.Models;

namespace RPedretti.RazorComponents.Sample.Shared.State
{
    public class AppState : IAppState
    {
        public ITranslations<int> Translations { get; } = new Translations<int>();
    }
}
