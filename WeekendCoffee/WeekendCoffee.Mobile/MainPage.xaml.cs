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

			//var handler = new Анд();
			//handler.servercertificatecustomvalidationcallback = (message, cert, chain, errors) =>
			//{
			//	if (cert != null && cert.issuer.equals("cn=localhost"))
			//		return true;
			//	return errors == system.net.security.sslpolicyerrors.none;
			//};

			_httpClient = new HttpClient();

			InitializeComponent();

		}

		private async void OnAddButtonClicked(object sender, EventArgs e)
		{
			try
			{
				//TODO Fix url and permision stuff
				var response = await _httpClient.GetAsync($"http://10.0.2.2:5281/meetings/current");
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