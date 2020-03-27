using RPedretti.RazorComponents.I18n.Models;

namespace RPedretti.RazorComponents.Sample.Shared.State
{
    public interface IAppState
    {
        ITranslations<int> Translations { get; }
    }
}