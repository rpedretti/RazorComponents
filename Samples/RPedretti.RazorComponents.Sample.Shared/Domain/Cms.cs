using System;
using System.Collections.Generic;
using System.Text;

namespace RPedretti.RazorComponents.Sample.Shared.Domain
{
    public class Cms
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Cms(int id, string text)
        {
            Id = id;
            Text = text;
        }

    }
}
