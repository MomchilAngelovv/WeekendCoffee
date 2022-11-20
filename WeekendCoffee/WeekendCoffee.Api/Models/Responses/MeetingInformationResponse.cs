namespace WeekendCoffee.Api.Models.Responses
{
	public class MeetingInformationResponse
	{
		public MeetingInformationResponse()
		{
			this.Members = new List<string>();
		}

		public string Label { get; set; }
		public List<string> Members { get; set; }
	}
}
