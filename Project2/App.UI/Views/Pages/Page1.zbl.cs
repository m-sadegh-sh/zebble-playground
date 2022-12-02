namespace UI.Pages
{
    using Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;

    partial class Page1
    {
        public bool isDark = false;
        public override async Task OnInitialized()
        {
            await base.OnInitialized();
            Body.ClipChildren = false;
        }

        async Task OnDarkTapped()
        {
            isDark = true;
            await this.SetPseudoCssState("dark", isDark);
        }

        async Task OnLightTapped()
        {
            isDark = false;
            await this.SetPseudoCssState("dark", isDark);
        }
    }
}