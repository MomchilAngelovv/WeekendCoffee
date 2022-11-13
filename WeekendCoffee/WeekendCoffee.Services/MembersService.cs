namespace WeekendCoffee.Services
{
	using Microsoft.EntityFrameworkCore;

	using WeekendCoffee.Data;

	public interface IMembersService
	{
		Task<Member> AddMemberAsync(string name, string nickName, string phoneNumber);
		Task<Member> GetOneAsync(int memberId);
	}

	public class MembersService : IMembersService
	{
		private readonly WeekendCoffeeDbContext db;

		public MembersService(
			WeekendCoffeeDbContext db)
		{
			this.db = db;
		}

		public async Task<Member> AddMemberAsync(string name, string nickName, string phoneNumber)
		{
			var newMember = new Member
			{
				Name = name,
				NickName = nickName,
				PhoneNumber = phoneNumber
			};

			await this.db.Members.AddAsync(newMember);
			await this.db.SaveChangesAsync();

			return newMember;
		}

		public async Task<Member> GetOneAsync(int memberId)
		{
			return await this.db.Members.SingleOrDefaultAsync(m => m.Id == memberId);
		}
	}
}
