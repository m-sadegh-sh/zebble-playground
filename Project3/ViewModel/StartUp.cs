namespace ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using UI.Pages;
    using Zebble;

    class StartUp
    {
        public static Task Run()
        {

            Nav.Go<Page1>(PageTransition.Fade);

            return Task.CompletedTask;
        }
    }
}
