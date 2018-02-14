using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LeaderboardCoreWebApp.Services.Contracts;
using LeaderboardCoreWebApp.Models;
using LeaderboardCoreWebApp.Controllers;

namespace LeaderboardCoreWebApp.UnitTest
{
    [TestClass]
    public class ControllersTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfCompetitors()
        {
            // Arrange
            var mockService= new Mock<ILeaderboardService>();
            mockService.Setup(repo => repo.GetAllCompetitors()).Returns(Task.FromResult(GetTestCompetitors()));
            var controller = new CompetitorsController(mockService.Object, null);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<IEnumerable<Competitor>>(
                viewResult.ViewData.Model);
            Xunit.Assert.Equal(4, model.Count());
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithALeaderboard()
        {
            // Arrange
            var mockService = new Mock<ILeaderboardService>();
            mockService.Setup(repo => repo.FindLeaderboardByName("Default")).Returns(Task.FromResult(this.GetTestLeaderboard()));
            var controller = new LeaderboardsController(mockService.Object, null);

            // Act
            var result = await controller.Index();

            // Assert
            var viewResult = Xunit.Assert.IsType<ViewResult>(result);
            var model = Xunit.Assert.IsAssignableFrom<Leaderboard>(
                viewResult.ViewData.Model);
            Xunit.Assert.Equal("Default", model.Name);
        }

        private IEnumerable<Competitor> GetTestCompetitors()
        {
            var competitors = new Competitor[]
            {
                    new Competitor{Name="Iron Man", LeaderboardId=1},
                    new Competitor{Name="Strong Fighter", LeaderboardId=1},
                    new Competitor{Name="Nency123", LeaderboardId=1},
                    new Competitor{Name="Kunfuist", LeaderboardId=1}
            };

            return competitors.ToList();
        }

        private Leaderboard GetTestLeaderboard()
        {
            var board = new Leaderboard { Name = "Default" };

            board.Competitors = this.GetTestCompetitors().ToList();

            return board;
        }
    }
}
