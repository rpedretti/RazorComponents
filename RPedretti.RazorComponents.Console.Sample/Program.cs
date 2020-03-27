using System;
using System.IO;
using WebWindows.Blazor;

namespace RPedretti.RazorComponents.Console.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            ComponentsDesktop.Run<Startup>
        }
    }
}
