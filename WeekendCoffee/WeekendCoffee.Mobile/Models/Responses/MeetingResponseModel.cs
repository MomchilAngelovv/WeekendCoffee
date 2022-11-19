namespace WeekendCoffee.Mobile.Models.Responses
{
	public class MeetingResponseModel
	{
		public string Label { get; set; }
		public DateTime OccursOn { get; set; }
		public List<string> Members { get; set; }
	}
}
