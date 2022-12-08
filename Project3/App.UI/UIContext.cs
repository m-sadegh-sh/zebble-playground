using Domain;
using Olive;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Pages;
using Zebble;
using static Zebble.Device.Screen;

namespace UI
{
    partial class UIContext
    {
        static DateTime LastActiveUtc = LocalTime.UtcNow;

        static bool IsBusy;
        public static readonly AsyncEvent Refreshed = new AsyncEvent();
        static DateTime LastOnline;

        public readonly static Bindable<bool> Online = new Bindable<bool>();

        public static async Task ClearCache()
        {
            foreach (var item in View.Root.AllChildren.OfType<Page>().Except(Nav.ActivePages).ToArray())
                await item.RemoveSelf();

            Nav.DisposeCache();
        }

        public static bool IsSmallScreen() => View.Root.ActualHeight < 600;

        public static Color Background => IsDark() ? WordupColors.Black : WordupColors.DarkJ;
        public static Color Foreground => IsDark() ? WordupColors.BackgroundBottom : WordupColors.Black;

        public static async Task ApplyStyleChoice()
        {
            try
            {
                View.Root.Background(Background);
#if UWP
                // Android has a bug in light theme and doesn't apply foreground at all. It's a zebble issue. 
                // And I haven't tested iOS since it looks good enough
                StatusBar.BackgroundColor = Background;
                StatusBar.ForegroundColor = Foreground;
#endif

                var css = View.Root.CssClass.OrEmpty().Split(' ').Trim()
                    .Except("dark-mode").Concat("dark-mode".OnlyWhen(IsDark()))
                    .Except("small-screen").Concat("small-screen".OnlyWhen(IsSmallScreen()))
                    .Trim().ToString(" ");

                if (View.Root.CssClass != css)
                    await View.Root.SetCssClass(css);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to apply the chosen style.", ex);
            }
        }

        internal static async Task HandleBackButton(HardwareBackEventArgs args)
        {
            args.Cancel = true;

            Task GoBack()
            {
                if (Nav.CanGoBack()) return Nav.Back();
                return Task.CompletedTask;
            }

        }

        static partial void HandleBackButtonPressedOnHome();

        public static async Task RefreshUI(bool reload = true)
        {
            while (IsBusy) await Task.Delay(20);
            IsBusy = true;

            await ClearCache();

            try
            {
                await UIWorkBatch.Run(() => ApplyStyleChoice(), awaitNative: true);
                await Refreshed.Raise();
            }
            finally { IsBusy = false; }

            if (reload) await Nav.Reload();
        }

        public static async Task<bool> IsOffline() => !await IsOnline();

        /// <summary>
        /// Determines whether the user is offline now, and has not been online since 10 seconds ago.
        /// </summary>
        public static async Task<bool> IsDefinitelyOffline()
        {
            if (LastOnline > DateTime.UtcNow.Subtract(10.Seconds())) return false;
            return await IsOffline();
        }

        public static async Task<bool> IsOnline()
        {
            var result = await Zebble.Device.Network.IsAvailable();
            if (Online.Value != result) Online.Value = result;
            if (result) LastOnline = DateTime.UtcNow;
            return result;
        }


        public static bool IsDark()
        {
            var theme =  "Automatic";

            if (theme == "Automatic") return Zebble.Device.Screen.DarkMode;
            else return theme == "Dark";
        }

        /// <returns>"white" if in dark mode otherwise "black"</returns>
        public static string WhetherThemeIcon() => WhetherTheme("white", "black");

        /// <returns>darkValue if in dark mode otherwise lightValue.</returns>
        public static T WhetherTheme<T>(T darkValue, T lightValue) => IsDark() ? darkValue : lightValue;

        internal static void KeepFresh()
        {
            Zebble.Device.App.WentIntoBackground += () => LastActiveUtc = LocalTime.UtcNow;

            Zebble.Device.App.CameToForeground += () =>
            {
                if (LocalTime.UtcNow.Subtract(LastActiveUtc) > 10.Minutes())
                {
                    ClearCache().RunInParallel();
                }
            };
        }

        internal static async Task AwaitConnection(int checkIntervals = 2)
        {
            while (await IsOffline())
                await Task.Delay(checkIntervals.Seconds());
        }
    }
}