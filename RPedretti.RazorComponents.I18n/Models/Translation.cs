namespace RPedretti.RazorComponents.I18n.Models
{
    public sealed class Translation<TKey> : ITranslation<TKey>
    {
        public TKey Key { get; set; }

        public string Text { get; set; }
    }
}
