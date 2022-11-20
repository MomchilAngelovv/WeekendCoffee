namespace WeekendCoffee.Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models;
	using WeekendCoffee.Api.Models.Requests;
	using WeekendCoffee.Api.Models.Responses;

	[ApiController]
	[Route("[controller]")]
	public class MembersController : ControllerBase
	{
		private readonly IMembersService membersService;

		public MembersController(
			IMembersService membersService)
		{
			this.membersService = membersService;
		}

		[HttpPost]
		public async Task<IActionResult> InsertMember(InsertMemberRequest request)
		{
			var response = new ControllerResponse<InsertMemberResponse>();
			var newMember = await this.membersService.AddMemberAsync(request.FirstName, request.MiddleName, request.LastName, request.NickName, request.PhoneNumber);

			response.Status = GlobalConstants.Success;
			response.ErrorMessage = GlobalConstants.NotAvailable;
			response.Data = new InsertMemberResponse
			{
				Id = newMember.Id,
				FirstName = newMember.FirstName,
				MiddleName = newMember.MiddleName,
				LastName = newMember.LastName,
				NickName = newMember.NickName,
				PhoneNumber = newMember.PhoneNumber
			};

			return this.Ok(response);
		}
	}
}
