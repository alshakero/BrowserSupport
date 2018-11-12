using System;
using Starcounter;

namespace BrowserSupport
{
    class Program
    {
        static void Main()
        {
            var app = Application.Current;
            app.Use(new HtmlFromJsonProvider());
            app.Use(new PartialToStandaloneHtmlProvider());

            Handle.GET("/Main", () => new MainView());
        }
    }
}