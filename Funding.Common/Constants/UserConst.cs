namespace Funding.Common.Constants
{
    public class UserConst
    {
        public const int UserMinLength = 1;
        public const int UserMaxLenght = 100;
        public const int MinAge = 18;
        public const int MaxAge = 140;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;

        public const string FirstName = "First Name cannot be empty";
        public const string LastName = "Last Name cannot be empty";
        public const string ConfirmPassword = "The password and confirmation password do not match.";
        public const string Password = "The password must be at least 6 and at max 100 characters long.";
        public const string Age = "You must be 18 or above to register.Max Age: 140";
        public const string UserName = "UserName cannot be empty";

        public const string FirstNameDisplay = "First Name";
        public const string LastNameDisplay = "Last Name";
        public const string PasswordDisplay = "Password";
        public const string ConfirmPasswordDisplay = "Confirm Password";
        public const string EmailDisplay = "Email";
        public const string UserNameDisplay = "UserName";

        public const string UserDoesntExist = "This user doesn't exist";
        public const string EmailTaken = "This email address - {0} is taken";
        public const string UserUpdated = "Successfully updated user {0}";
        public const string InvalidIdentity = "Invalid identity details.";
        public const string AddedInRole = "{0} is now {1}";

        public const string TempDataResult = "result";
        public const string TempDataError = "error";
        public const string Success = "Successfully";
        public const string GetUnreadMessages = "GetMessages";

    }
}
