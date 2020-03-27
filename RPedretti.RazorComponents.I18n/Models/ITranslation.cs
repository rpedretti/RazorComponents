namespace RPedretti.RazorComponents.I18n.Models
{
    public interface ITranslation<TKey>
    {
        TKey Key { get; }
        string Text { get; }
    }
}
