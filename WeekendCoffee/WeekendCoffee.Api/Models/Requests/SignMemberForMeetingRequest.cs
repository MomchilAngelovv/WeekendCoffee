namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class SignMemberForMeetingRequest
    {
        [Required]
        public int MemberId { get; set; }
        public string Comment { get; set; }
    }
}
