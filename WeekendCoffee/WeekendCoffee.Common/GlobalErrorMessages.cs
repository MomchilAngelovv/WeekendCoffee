namespace WeekendCoffee.Common
{
	public static class GlobalErrorMessages
	{
		//Meetings
		public const string CannotCreateMeeting = "There was a problem when trying to create meeting!";
		public const string CannotFindMeeting = "There was a problem when trying to find meeting!";

		//Members
		public const string CannotFindMember = "There was a problem when trying to find member!";
		public const string MemberAlreadyExists = "Member with this nickname already exists.";
		public const string MemberAlreadySignedForMeeting = "There was a problem when trying to sign up member for meeting! Already signed.";

		//Settings
		//TODO FIX Constants naming conventions
		public const string SettingWithKeyAlreadyExists = "There is already a setting with the provided key!";
		public const string SettingDoesNotExists = "Setting does now exists!";
	}
}
