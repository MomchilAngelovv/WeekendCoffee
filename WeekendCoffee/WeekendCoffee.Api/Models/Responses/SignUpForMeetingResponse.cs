namespace WeekendCoffee.Api.Models.Responses
{
	public class SignUpForMeetingResponse
	{
		public string Status { get; set; }
		public string Message { get; set; }
		public int MemberId { get; set; }
		public string MeetingLabel { get; set; }
	}
}
