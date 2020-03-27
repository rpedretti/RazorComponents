using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.I18n
{
    public class Translations<TKey> : ITranslations<TKey>
    {
        private readonly IDictionary<TKey, string> _translations = new Dictionary<TKey, string>();

        public Translations() { }

        public Translations(Dictionary<TKey, string> values)
        {
            _translations = values;
        }

        public string this[TKey key] => _translations[key];

        public int Count => _translations.Count;

        public IEnumerable<TKey> Keys => _translations.Keys;

        public IEnumerable<string> Values => _translations.Values;

        public bool ContainsKey(TKey key) => _translations.ContainsKey(key);

        public IEnumerator<KeyValuePair<TKey, string>> GetEnumerator() => _translations.GetEnumerator();

        public bool TryGetValue(TKey key, out string value) => _translations.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => _translations.GetEnumerator();

        public void Add(TKey key, string value)
        {
            _translations[key] = value;
        }

        public void Remove(TKey key)
        {
            _translations.Remove(key);
        }

        public void Clear()
        {
            _translations.Clear();
        }
    }
}
