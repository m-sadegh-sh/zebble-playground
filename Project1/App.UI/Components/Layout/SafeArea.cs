using Domain;
using System;
using System.Threading.Tasks;
using Zebble;

namespace UI.Templates
{
    public class SafeArea : Canvas
    {
        public bool WithBorder { get; set; }

        public override async Task OnPreRender()
        {
            await base.OnPreRender();

            Height.Set(Length.AutoStrategy.Container);

            this.Padding(
                left: Zebble.Device.Screen.SafeAreaInsets.Left,
                top: Zebble.Device.Screen.SafeAreaInsets.Top,
                right: Zebble.Device.Screen.SafeAreaInsets.Right,
                bottom: Zebble.Device.Screen.SafeAreaInsets.Bottom
            );

            if (WithBorder)
            {
                UIContext.Refreshed.Handle(SetBorder);
                SetBorder();
            }
        }

        void SetBorder() => this.Border(
            bottom: Zebble.Device.Screen.SafeAreaInsets.Bottom * (WithBorder && Zebble.Device.OS.Platform.IsIOS() ? 2 : 1),
            color: UIContext.IsDark() ? WordupColors.Gray : Colors.White
        );
    }
}