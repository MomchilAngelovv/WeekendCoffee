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

			builder.Services.AddTransient<MainPage>();
			builder.Services.AddTransient<MaingPageViewModel>();

			return builder.Build();
		}
	}
}