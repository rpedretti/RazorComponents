using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.I18n
{
    public sealed class Translation<TKey> : ITranslation<TKey>
    {
        public TKey Key { get; set; }

        public string Text { get; set; }
    }
}
