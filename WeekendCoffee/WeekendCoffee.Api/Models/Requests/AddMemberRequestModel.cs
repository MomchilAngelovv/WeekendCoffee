namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class AddMemberRequestModel
    {
        [Required]
        public string Name { get; set; }
		[Required]
		public string NickName { get; set; }
		[Required]
		public string PhoneNumber { get; set; }
    }
}
