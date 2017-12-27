namespace Funding.Services.Interfaces
{
    using Funding.Services.Models.MailViewModels;
    using System.Threading.Tasks;

    public interface IInboxService
    {
        Task<MessagesListViewModel> GetAllMessages(string name, int page);

        Task<bool> SendMessage(string senderName, string receiverName, string title, string content);

        Task<AllMessagesViewModel> GetSingleMessage(string currentUser, int messageId);

        Task<bool> DeleteMessage(string currentUser, int messageId);

        Task<int> GetUnreadMessages(string userName);
    }
}