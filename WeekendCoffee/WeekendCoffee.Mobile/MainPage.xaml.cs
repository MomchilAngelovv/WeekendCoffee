namespace WeekendCoffee.Mobile
{
	using System.Text.Json;
	using System.Net.Http.Json;

	using WeekendCoffee.Mobile.Models.Responses;
	using WeekendCoffee.Mobile.Models.ViewModels;

	public partial class MainPage : ContentPage
	{
		public MainPage(MaingPageViewModel pageViewModel)
		{
			InitializeComponent();
			BindingContext = pageViewModel;

			Task.Run(() => pageViewModel.GetCurrentMeetingDataAsync());
		}
	}
}