using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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
				if (string.IsNullOrWhiteSpace(Number.Text))
				{
					return;
				}

				var contentObject = new
				{
					MemberId = Number.Text,	
				};

				//var content = new StringContent(contentObject.ToString(), Encoding.UTF8, "application/json");
				//content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
				var addPlayerResponse = await _httpClient.PostAsJsonAsync($"http://10.0.2.2:5281/Attendances", contentObject);
				if (!addPlayerResponse.IsSuccessStatusCode)
				{
					//SOMETHING
					return;
				}

				var currentMeetingResponse = await _httpClient.GetAsync($"http://10.0.2.2:5281/meetings/current");
				if (!currentMeetingResponse.IsSuccessStatusCode)
				{
					//SOMETHING
				}

				var responseBody = await currentMeetingResponse.Content.ReadAsStringAsync();
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