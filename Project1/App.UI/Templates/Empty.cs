namespace UI.Templates
{
    using System.Threading.Tasks;
    using Zebble;

    class Empty : Page
    {
        public SafeArea SafeAreaContainer = new SafeArea();
        public Stack Body;

        protected override async Task InitializeFromMarkup()
        {
            await SetPseudoCssState("dark", UIContext.IsDark());
            await base.InitializeFromMarkup();

            await Add(SafeAreaContainer);
            await SafeAreaContainer.Add(Body = new Stack().Id("Body"));
            BackgroundColor = Root.BackgroundColor;

        }
    }
}
