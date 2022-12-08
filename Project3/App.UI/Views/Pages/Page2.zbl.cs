namespace UI.Pages
{
    using Domain;
    using Olive;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Net.Cache;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Text;
    using System.Threading.Tasks;
    using Zebble;
    using Zebble.Billing;
    using Zebble.UWP;
    using static Olive.OliveExtensions;

    partial class Page2
    {
        bool _theme;
        public bool Theme
        {
            get => _theme;
            set
            {
                _theme = value;
            }
        }
        Subject _detail;
        public Subject Detail
        {
            get => _detail;
            set
            {
                _detail = value;
            }
        }

        public override async Task OnInitialized()
        {
            Theme = Nav.Param<bool>("Theme");
            Detail = Nav.Param<Subject>("Detail");
            await DetailsList.UpdateSource(Detail.Details);
            await this.SetPseudoCssState("dark", Theme);
            await base.OnInitialized();
        }

        async Task BackTapped()
        {
            await Nav.Go<Page1>();
        }

        partial class DetailRow
        {
            public WordInfo Detail { get; set; }

            public override Task OnInitializing()
            {
                Detail = Item;
                Text1.Text(Detail.Text);
                Text1.CssClass("Text1");
                Text2.CssClass("Text2");
                Text3.CssClass("Text2");
                Text2.Text(Detail.OtherForms);
                Text3.Text(Detail.Meaning + "\n\n\n");
                return base.OnInitializing();
            }
        }
    }
}