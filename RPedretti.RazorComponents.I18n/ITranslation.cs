using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.I18n
{
    public interface ITranslation<TKey>
    {
        TKey Key { get; }
        string Text { get; }
    }
}
