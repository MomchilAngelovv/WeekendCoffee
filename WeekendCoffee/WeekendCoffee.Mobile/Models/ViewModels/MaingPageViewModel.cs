namespace WeekendCoffee.Mobile.Models.ViewModels
{
	using System.Text.Json;
	using System.Net.Http.Json;
	using System.Collections.ObjectModel;

	using CommunityToolkit.Mvvm.Input;
	using CommunityToolkit.Mvvm.ComponentModel;

	using WeekendCoffee.Mobile.Models.Responses;

	public partial class MaingPageViewModel : ObservableObject
	{
		private readonly HttpClient _httpClient;

		public MaingPageViewModel(HttpClient httpClient)
		{
			_httpClient = httpClient;
			members = new ObservableCollection<string>();
		}

		[ObservableProperty]
		string memberId;

		[ObservableProperty]
		string label;

		[ObservableProperty]
		ObservableCollection<string> members;

		[RelayCommand]
		public async Task AddMember()
		{
			if (string.IsNullOrWhiteSpace(MemberId))
			{
				return;
			}

			var contentObject = new
			{
				MemberId,
			};

			var response = default(HttpResponseMessage);
			try
			{
				response = await _httpClient.PostAsJsonAsync($"/Attendances", contentObject);
			}
			catch (Exception ex)
			{
				//Think logic if error;
			}

			if (!response.IsSuccessStatusCode)
			{
				var body = await response.Content.ReadAsStringAsync();
				return;
			}

			MemberId = string.Empty;
			await GetCurrentMeetingDataAsync();
		}

		public async Task GetCurrentMeetingDataAsync()
		{
			var response = default(HttpResponseMessage);
			try
			{
				response = await _httpClient.GetAsync($"/meetings/current");
			}
			catch (Exception ex)
			{
				//Think logic if error;
			}

			if (!response.IsSuccessStatusCode)
			{
				//Think logic if not successfull response;
				return;
			}

			var responseBody = await response.Content.ReadAsStringAsync();
			if (string.IsNullOrWhiteSpace(responseBody))
			{
				//Think logic if empty body;
				return;
			}

			var controllerResponse = JsonSerializer.Deserialize<ControllerResponse<MeetingInformationResponse>>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			Label = $"{controllerResponse.Data.Label}"; 
			Members.Clear();
			foreach (var member in controllerResponse.Data.Members)
			{
				Members.Add(member);
			}
		}
	}
}
