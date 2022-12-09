namespace WeekendCoffee.Api.Models.Requests
{
	using System.ComponentModel.DataAnnotations;

	public class UpdateSettingRequest
	{
		[Required]
		public string NewValue { get; set; }
	}
}
