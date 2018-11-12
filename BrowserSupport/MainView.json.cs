using Starcounter;

namespace BrowserSupport
{
    partial class MainView : Json
    {
        public MainView()
        {
            this.Count = 0;
        }
        public long CountMirror => this.Count;
    }
}
