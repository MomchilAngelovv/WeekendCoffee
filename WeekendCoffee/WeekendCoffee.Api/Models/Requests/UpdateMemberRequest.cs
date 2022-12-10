namespace WeekendCoffee.Api.Models.Requests
{
	public class UpdateMemberRequest
	{
		public string? NewFirstName { get; set; }
		public string? NewMiddleName { get; set; }
		public string? NewLastName { get; set; }
		public string? NewNickName { get; set; }
		public string? NewPhoneNumber { get; set; }
		public string? NewPassword { get; set; }
	}
}
