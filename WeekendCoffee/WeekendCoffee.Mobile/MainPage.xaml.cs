namespace WeekendCoffee.Mobile
{
	using System.Text.Json;
	using System.Net.Http.Json;

	using WeekendCoffee.Mobile.Models.Responses;

	public partial class MainPage : ContentPage
	{
		private readonly HttpClient _httpClient;

		public MainPage()
		{
			InitializeComponent();
			_httpClient = new HttpClient();

			Task.Run(async () =>
			{
				var currentMeetingResponse = await _httpClient.GetAsync($"http://10.0.2.2:5281/meetings/current");
				if (!currentMeetingResponse.IsSuccessStatusCode)
				{
					return;
				}

				var responseBody = await currentMeetingResponse.Content.ReadAsStringAsync();
				if (string.IsNullOrWhiteSpace(responseBody))
				{
					return;
				}

				var data = JsonSerializer.Deserialize<MeetingResponseModel>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				CurrentMeetingLabel.Text = data.Label;
			});
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
					return;
				}

				var responseBody = await currentMeetingResponse.Content.ReadAsStringAsync();
				if (string.IsNullOrWhiteSpace(responseBody))
				{
					//SOMETHING
					return;
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