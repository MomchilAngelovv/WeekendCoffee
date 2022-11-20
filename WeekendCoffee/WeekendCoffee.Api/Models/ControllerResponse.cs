namespace WeekendCoffee.Api.Models
{
	public class ControllerResponse<T>
	{
		public string Status { get; set; }
		public string ErrorMessage { get; set; }
		public T Data { get; set; }
	}
}
