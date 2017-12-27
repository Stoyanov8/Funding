namespace Funding.Common.Constants
{
    public class ProjectConst
    {
        public const string ProjectNameRequired = "Your project should have a name";
        public const string ProjectDescriptionRequired = "Your project should have a clear description";
        public const string ProjectImageUrlRequired = "Please verify that you are sending image link";
        public const string StartDateMessage = "Start date should be greater than ";
        public const string EndDateMessage = "End date should be greater than Start Date";
        public const string GoalRequired = "You need to have goal money";
        public const string TagRequired = "You should have atleast on tag";
        public const string GoalNegativeMessage = "Goal money cannot be negative";
        public const string Success = "Succesfully added project";
        public const string Failed = "Something went wrong try again";
        public const string DoesntExist = "This project doesn't exist";
        public const string ProjectAdded = "Your project was succesfully added.The admin should approve it before people can see it.";
        public const string Comment = "Comment cannot be empty";
        public const string ProjectDeleted = "Project deleted";
        public const string ProjectFunded = "Project {0} funded by {1}";
        public const string DonateMessageRequired = "Please tell the creator why you liked his project";
        public const string DonateAmountRequired = "Please enter valid number greater than 0.1";
        public const string StartDateRequired = "Start Date need to be selected";
        public const string EndDateRequired = "End Date need to be selected";
        public const string ProjectApproved = "Your project was approved";
        public const string ProjectNotApproved = "Your project was not approved";
        public const string TempDataDeletedComment = "commentDelete";
        public const string DeletedCommentSuccess = "Comment successfully deleted";
        public const string DeletedCommentFailed = "You do not have permission to do that!";

        public const string SuccesfullyEdited = "Project succesfully edited";
        public const string AlreadyDonated = "You've already donated for this project !";
        public const string SuccessfullyDonated = "{0} euro succesfully donated!";

        public const string ImageUrlDisplay = "Image URL";
        public const string StartDateDisplay = "Project Start Date";
        public const string EndDateDisplay = "Project End Date";


        public const string Approved = "Approved";
        public const string Deleted = "Deleted";
        public const string Disabled = "disabled";

        public const string TempDataUnauthorized = "unauthorized";
        public const string TempDataDeleted = "deletedSuccess";
        public const string TempDataSorry = "sorry";
        public const string TempDataDonated = "message1";
        public const string TempDataMessage = "message";
        public const string TempDataAddedProject = "addedProject";

        public const int MaxLenght = 2400;
        public const int ImageUrlMinLenght = 5;
        public const int MinimumGoal = 1;
        public const int NameMinLength = 2;
        public const int NameМaxLength = 100;
        public const int DescriptionMinLenght = 10;
        public const int DescriptionMaxLenght = 600;
        public const int CommentMinLength = 2;
        public const int CommentMaxLength = 250;
        public const double MinimumAmountToDonate = 0.01;


        public const string Jpeg = "jpeg";
        public const string Jpg = "jpg";
        public const string Png = "png";
        public const string Gif = "gif";
    }
}