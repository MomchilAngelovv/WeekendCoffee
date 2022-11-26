namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class InsertMemberRequest
    {
        [Required]
        public string FirstName { get; set; }
		public string? MiddleName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Required]
		public string NickName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
    }
}
