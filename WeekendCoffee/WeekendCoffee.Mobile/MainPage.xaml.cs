using System.Text.Json;
using WeekendCoffee.Mobile.Models.Responses;

namespace WeekendCoffee.Mobile
{
	public partial class MainPage : ContentPage
	{
		int count = 0;
		private readonly HttpClient _httpClient;

		public MainPage()
		{

			var handler = new Xamarin.Android.Net.AndroidMessageHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
			{
				if (cert != null && cert.Issuer.Equals("CN=localhost"))
					return true;
				return errors == System.Net.Security.SslPolicyErrors.None;
			};

			_httpClient = new HttpClient(handler);

			InitializeComponent();

		}

		private async void OnAddButtonClicked(object sender, EventArgs e)
		{
			try
			{

				var response = await _httpClient.GetAsync($"https://10.0.2.2:7281/meetings/current");
				if (!response.IsSuccessStatusCode)
				{
					//SOMETHING
				}

				var data = JsonSerializer.Deserialize<MeetingResponseModel>(await response.Content.ReadAsStringAsync());
				AddButton.Text = data.Label;
			}
			catch (Exception ex)
			{

			}
			
			//count++;

			//if (count == 1)
			//	CounterBtn.Text = $"Clicked {count} time";
			//else
			//	CounterBtn.Text = $"Clicked {count} times";

			//SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}
}