using RPedretti.RazorComponents.I18n;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPedretti.RazorComponents.Sample.Shared.State
{
    public class AppState : IAppState
    {
        public ITranslations<int> Translations { get; } = new Translations<int>();
    }
}
