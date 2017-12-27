namespace Funding.Test.Services
{
    using FluentAssertions;
    using Funding.Common.Constants;
    using Funding.Data;
    using Funding.Data.Models;
    using Funding.Services.Models.ProjectViewModels;
    using Funding.Services.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ProjectServiceTest
    {
        [Fact]
        public async Task ListingAllProjectsShouldBeOrederById()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            // Act
            var result = await service.GetAllProjects(1);

            // Assert
            result.Projects.Should()
                .Match(x => x.ElementAt(0).Id == 3 && x.ElementAt(1).Id == 2);
        }

        [Fact]
        public async Task AddingStartDateBeforeCurrentDateShouldReturnCertainMessage()
        {
            // Arrange

            var db = DatabaseInitializer.InitializeForProjectService();
            var startDate = new DateTime(2016, 10, 2);
            var endDate = new DateTime(2015, 10, 2);

            var service = new ProjectService(db);

            // Act
            var result = await service.AddProject("Hello", "Description", "wwww.imageurl.com", 10, startDate, endDate, "spas", "1");

            // Assert
            result.Should().Be(ProjectConst.StartDateMessage);
        }

        [Fact]
        public async Task AddingEndDateBeforeStartDateShouldReturnCertainMessage()
        {

            var db = DatabaseInitializer.InitializeForProjectService();
            var startDate = new DateTime(2018, 10, 2);
            var endDate = new DateTime(2015, 10, 2);

            var service = new ProjectService(db);

            // Act
            var result = await service.AddProject("Hello", "Description", "wwww.imageurl.com", 10, startDate, endDate, "spas", "1");

            // Assert
            result.Should().Be(ProjectConst.EndDateMessage);
        }

        [Fact]
        public async Task EnsureReturningErrorMessageIfUserIsNull()
        {
            //Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var startDate = new DateTime(2018, 10, 2);
            var endDate = new DateTime(2019, 10, 2);

            var service = new ProjectService(db);

            // Act
            var result = await service.AddProject("Hello", "Description", "wwww.imageurl.com", 10, startDate, endDate, null, "1");

            // Assert
            result.Should().Be(ProjectConst.Failed);
        }


        [Fact]
        public void GetProjectByIdReturnsCorrectValue()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            // Act

            var actual = service.GetProjectById(1);
            var expected = typeof(DetailsProjectModel);

            // Assert
            Assert.IsType(expected, actual);
        }


        [Fact]
        public async Task MakeDonationWorksOkayIfEverythingIsCorrect()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.MakeDonation(1, "georgi", "nice project", 10);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task EnsureMethodReturnsFalseRatherThanException()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.MakeDonation(110, "georgi", "nice project", 10);
            //Assert
            Assert.False(result);
        }


        [Fact]
        public async Task GetMyProjectsReturnMyRealProjects()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.GetMyProjects(1, "georgi");
            //Assert
            Assert.Single(result.Projects);
        }

        [Fact]
        public async Task UserAlreadyDonatedReturnsTrue()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.UserAlreadyDonated(1, "georgi");
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProjectReturnsFalseIfDataMismatch()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.DeleteProject("georgi", 10);
            //Assert
            Assert.False(result);
        }


        [Fact]
        public async Task DeleteProjectWorksFineIfDataIsCorrect()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = await service.DeleteProject("georgi", 1);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void GetEditModelReturnsFalseIfUserIsNotTheCreator()
        {

            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            //Act
            var result = service.GetEditModel("georgi", 2);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task EditWorksOkayIfDataIsCorrect()
        {
            //Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var startDate = new DateTime(2018, 10, 2);
            var endDate = new DateTime(2019, 10, 2);

            var service = new ProjectService(db);

            // Act
            var result = await service.EditProject(1, "Hello", startDate, endDate, "Description", 10, "wwww.iamimagetrustme.com/realimage.jpg");

            var testedProject = db.Projects.FirstOrDefault(x => x.Name == "Hello");

            // Assert
            testedProject.Should().NotBeNull();
        }


        [Fact]
        public async Task GetFundedProjectsReturnsCorrectProjects()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);

            // Act
            var result = await service.GetFundedProjects(1, "georgi");

            //Assert
            Assert.Single(result.Projects);
        }

        [Fact]
        public async Task IsImageWorksCorrectly()
        {
            // Arrange
            var db = DatabaseInitializer.InitializeForProjectService();
            var service = new ProjectService(db);
            var firstProject = await db.Projects.FirstOrDefaultAsync(x=> x.Id==1);
            // Act
            var result = service.IsImageUrl(firstProject.ImageUrl);

            //Assert
            Assert.False(result);
        }
    }
}
