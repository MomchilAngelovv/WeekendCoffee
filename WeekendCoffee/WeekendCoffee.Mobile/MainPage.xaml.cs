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

				var responseBody = await response.Content.ReadAsStringAsync();
				if (string.IsNullOrWhiteSpace(responseBody))
				{
					//SOMETHING
				}

				var data = JsonSerializer.Deserialize<MeetingResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				CurrentMeetingLabel.Text = data.Label;
			}
			catch (Exception ex)
			{

			}
		}
	}
}