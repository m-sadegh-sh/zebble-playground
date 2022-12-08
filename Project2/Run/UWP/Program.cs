namespace UWP
{
    class Program
    {
        public static void Main()
        {
            Zebble.UIRuntime.Initialize<Program>("Project2");
            Windows.UI.Xaml.Application.Start((p) => new App());
        }
    }
}