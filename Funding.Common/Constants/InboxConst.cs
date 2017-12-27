namespace Funding.Common.Constants
{
    public class InboxConst
    {
        public const string TitleRequired = "The title should be atleast 2 symbols and less than 100";
        public const string ContentRequired = "The message should be atleast 10 symbols and not greater than 2400";
        public const string ReceiverNameRequired = "The receiver should not be empty";

        public const int TitleMinLength = 2;
        public const int TitleMaxLength = 100;

        public const int ContentMinLength = 10;
        public const int ContentMaxLength = 2400;


        public const int ReceiverNameMinLength = 2;
        public const int ReceiverNameMaxLength = 100;

        public const string ReceiverDisplay = "To";
    }
}
