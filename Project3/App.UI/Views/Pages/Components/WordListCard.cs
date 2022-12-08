namespace UI.Modules
{
    using Domain;
    using Olive;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;

    internal class WordListCard : Stack
    {
        public TextView SubjectTitle = new TextView();
        public ImageView SubjectImage = new ImageView();
        public bool isDark {get; set; }
        public Bindable<Subject> Subject { get; set;} = new();
        public override async Task OnInitializing()
        {
            SubjectTitle.Bind("Text",() => Subject, x=>x?.Name);
            SubjectTitle.CenterAlign();
            SubjectTitle.CssClass("SubjectText");
            SubjectImage.Alignment(Alignment.Left);
            SubjectTitle.Height(30);

            SubjectImage.Bind("Path", () => Subject, x => x?.ImageURL);
            SubjectImage.Size(80);
            SubjectImage.Alignment(Alignment.Middle);
            SubjectImage.CssClass("PhotoCenter");
            await Add(SubjectImage);
            await Add(SubjectTitle);
            this.On(x=>x.Tapped, OnDarkTapped);

            await base.OnInitializing();
        }

        async Task OnDarkTapped()
        {
            await Nav.Go<Page2>(new {Theme = isDark , Detail = Subject.Value});
        }
    }
}
