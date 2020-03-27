using System.Collections.Generic;

namespace RPedretti.RazorComponents.I18n
{
    public interface ITranslations<TKey> : IReadOnlyDictionary<TKey, string>
    {
        void Add(TKey key, string value);
        void Add(params (TKey key, string value)[] values);
        void Remove(TKey key);
        void Clear();
    }
}