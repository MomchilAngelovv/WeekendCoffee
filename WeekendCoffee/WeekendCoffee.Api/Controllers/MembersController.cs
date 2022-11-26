namespace WeekendCoffee.Api.Controllers
{
	using System.Threading.Tasks;

	using Microsoft.AspNetCore.Mvc;

	using WeekendCoffee.Common;
	using WeekendCoffee.Services;
	using WeekendCoffee.Api.Models.Requests;
	using WeekendCoffee.Api.Models.Responses;

	[ApiController]
	[Route("[controller]")]
	public class MembersController : BaseController
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
			var existingMember = this.membersService.GetOneAsync(request.NickName);
			if (existingMember is not null) 
			{
				return ErrorResponse(GlobalErrorMessages.MemberAlreadyExists);
			}

			var newMember = await this.membersService.InsertOneAsync(request.FirstName, request.MiddleName, request.LastName, request.NickName, request.PhoneNumber);

			var responseData = new InsertMemberResponse
			{
				Id = newMember.Id,
				FirstName = newMember.FirstName,
				MiddleName = newMember.MiddleName,
				LastName = newMember.LastName,
				NickName = newMember.NickName,
				PhoneNumber = newMember.PhoneNumber
			};

			return this.SuccessResponse(responseData);
		}
	}
}
