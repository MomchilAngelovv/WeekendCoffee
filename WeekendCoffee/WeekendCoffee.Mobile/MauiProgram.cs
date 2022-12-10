namespace WeekendCoffee.Mobile
{
	using WeekendCoffee.Mobile.Models.ViewModels;

	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

			builder.Services.AddSingleton(sp => new HttpClient
			{ 
				BaseAddress = new Uri("https://dd69-2a01-5a8-207-17d1-9cc2-d0c0-e8bd-6f26.eu.ngrok.io/")
			});

			builder.Services.AddTransient<MainPage>();
			builder.Services.AddTransient<MaingPageViewModel>();

			return builder.Build();
		}
	}
}