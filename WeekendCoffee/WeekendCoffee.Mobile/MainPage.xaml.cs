namespace WeekendCoffee.Mobile
{
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