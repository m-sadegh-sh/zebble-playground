namespace UI.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Domain;
    using Newtonsoft.Json.Linq;
    using Olive;
    using UI.Pages;
    using Zebble;
    using Zebble.Billing;


    partial class SubjectsPreview : Stack
    {
        bool _isDark;
        public bool isDark
        {
            get => _isDark;
            set
            {
                _isDark = value;
            }
        }
        Category _category;
        public Category Category
        {
            get => _category;
            set
            {
                _category = value;
            }
        }

        string _headerText;
        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                HeaderChanged(value);
            }
        }

        bool _rendered = false;

        public override async Task OnInitializing()
        {
            Carousel.SlideWidth = 85;
            Carousel.Height(145);
            await base.OnInitializing();
            
        }

        public override async Task OnRendered()
        {
            _rendered = true;
            await base.OnRendered();
            if (Category != null) await Carousel.UpdateDataSource(GetSubjects());

        }

        private void HeaderChanged(string value) => RowHeader.Text = value;

        List<Subject> GetSubjects()
        {
            return Category.Subjects.ToList();
        }
        async Task OnDarkTapped()
        {
            isDark = !isDark;
            await this.SetPseudoCssState("dark", isDark);
        }
        partial class SlideTemplate
        {
            public override async Task OnInitializing()
            {
                await base.OnInitializing();
                Item.Changed += Item_Changed;
                Item_Changed();
            }

            private void Item_Changed()
            {
                WordListCard.Subject.Set(Item);
            }

            public override void Dispose()
            {
                Item.Changed -= Item_Changed;
                base.Dispose();
            }
        }

    }

    
}