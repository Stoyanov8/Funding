using FluentAssertions;
using Funding.Services.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Funding.Test.Services
{
    public class InboxServiceTest
    {
        [Fact]
        public async Task GetAllMessagesWorksCorrect()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForInboxService();
            var service = new InboxService(db);

            // Act

            var result = await service.GetAllMessages("georgi", 1);

            //Assert
            result.Messages.Should().NotBeNull();
        }


        [Fact]
        public async Task NumberOfPagesGivesCorrectNumber()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForInboxService();
            var service = new InboxService(db);

            // Act

            var result = await service.GetAllMessages("kolio", 1);

            //Assert
            result.NumberOfPages.Should().Be(1);
        }

        [Fact]
        public async Task GetsTheWriteMessageIfDataIsCorrect()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForInboxService();
            var service = new InboxService(db);

            // Act

            var result = await service.GetSingleMessage("georgi", 1);

            //Assert
            result.Should().NotBeNull(result.Title);
        }
        [Fact]
        public async Task DeleteMessageReturnFalseNotExceptionIfDataIsWrong()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForInboxService();
            var service = new InboxService(db);

            // Act

            var result = await service.DeleteMessage("georgi", 10);

            //Assert
            result.Should().Be(false);
        }


        [Fact]
        public async Task DeleteMessageWorksCorrectlyIfDataIsRight()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForInboxService();
            var service = new InboxService(db);

            // Act

            var result = await service.DeleteMessage("georgi", 1);

            //Assert
            result.Should().Be(true);
        }
    }
}
