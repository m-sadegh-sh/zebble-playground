namespace ViewModel
{
	using System.Threading.Tasks;
	using Zebble;
	using Zebble.Mvvm;
	using Olive;
    using Domain;
	using UI.Pages;

	class StartUp
	{
		public static Task Run()
		{
			// Comment this line out to use the Live API
			Api.Fake();

			Nav.Go<Page1>(PageTransition.Fade);

			return Task.CompletedTask;
		}
	}
}