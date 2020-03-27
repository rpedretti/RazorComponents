using RPedretti.RazorComponents.I18n;
using RPedretti.RazorComponents.Sample.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.Sample.Shared.Pages.Translation
{
    public partial class TranslationPage
    {
        public ITranslations<int> Source = new Translations<int> {
            { 1, "Some translation" }
        };

        public ITranslation<int>[] Cms = new ITranslation<int>[] {
            new Translation<int> { Key = 1, Text = "Some Default Text" },
            new Translation<int> { Key = 2, Text = "Some Default Other Text" }
        };

        public void AddTranslation()
        {
            Source.Add(2, "Hello");
        }
    }
}
