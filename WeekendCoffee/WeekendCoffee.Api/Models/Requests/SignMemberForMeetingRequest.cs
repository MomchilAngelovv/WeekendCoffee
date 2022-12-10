namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class SignMemberForMeetingRequest
    {
        [Required]
        public string MemberId { get; set; }
		public string? Comment { get; set; }
    }
}
