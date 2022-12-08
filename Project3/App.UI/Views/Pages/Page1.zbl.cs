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

    partial class Page1
    {
        public Bindable<bool> isDark = false;
        public override async Task OnInitialized()
        {
            await base.OnInitialized();
            Body.ClipChildren = false;
            LoadCategories().RunInParallel();
        }

        async Task OnDarkTapped()
        {
            isDark = !isDark;
            Root.CssClass("dark".OnlyWhen(isDark));
        }

        async Task LoadCategories()
        {
            try
            {
                var http = new HttpClient();
                var categories = await HttpClientJsonExtensions.GetFromJsonAsync<Category[]>(http, "https://gist.githubusercontent.com/Mohsens22/d86950de0661b2b9f5dc6cdabcc9e71f/raw/50a989ae82b4b09f091ee8dc31a7f1d3f0304ab5/categories.json");
                if (categories != null) await CategoriesList.UpdateSource(categories);
            }
            catch (Exception ex)
            {
                Log.For(this).Error(ex, "Failed to load categories.");
            }
        }

        partial class CategoryRow
        {
            public Bindable<Category> Category { get; set; }
            public bool isDark { get; set; }

            public override Task OnInitializing()
            {
                Category = new Bindable<Category>(Item);
                return base.OnInitializing();
            }
        }
    }
}