namespace WeekendCoffee.Api.Models.Responses
{
	public class GetCurrentMeetingInformationResponse
	{
		public string Label { get; set; }
		public List<string> Members { get; set; } = new List<string>();
	}
}
