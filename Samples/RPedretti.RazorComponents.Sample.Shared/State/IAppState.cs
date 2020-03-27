using RPedretti.RazorComponents.I18n;

namespace RPedretti.RazorComponents.Sample.Shared.State
{
    public interface IAppState
    {
        ITranslations<int> Translations { get; }
    }
}