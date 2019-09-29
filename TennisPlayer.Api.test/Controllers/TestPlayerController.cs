using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using TennisPlayer.Api.Controllers;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;
using Xunit;

namespace TennisPlayer.Api.test
{
    public class TestPlayerController
    {
        Player player1 = new Player()
        {
            Id = 1,
            Firstname = "Novak",
            Lastname = "Djokovic",
            Shortname = "N.DJO",
            Sex = "M",
            Country = new Country()
            {
                Picture = new Uri("https://i.eurosport.com/_iss_/geo/country/flag/medium/6944.png"),
                Code = "SRB"
            },
            Picture = new Uri("https://i.eurosport.com/_iss_/person/pp_clubteam/large/565920.jpg"),
            Data = new Data()
            {
                Rank = 2,
                Points = 2542,
                Weight = 80000,
                Height = 188,
                Age = 31,
                Last = new int[] { 1, 1, 1, 1, 1 }
            }
        };

        Player player2 = new Player()
        {
            Id = 2,
            Firstname = "Venus",
            Lastname = "Williams",
            Shortname = "V.WIL",
            Sex = "F",
            Country = new Country()
            {
                Picture = new Uri("https://i.eurosport.com/_iss_/person/pp_clubteam/large/136449.jpg"),
                Code = "USA"
            },
            Picture = new Uri("https://i.eurosport.com/_iss_/person/pp_clubteam/large/136450.jpg"),
            Data = new Data()
            {
                Rank = 52,
                Points = 1105,
                Weight = 74000,
                Height = 185,
                Age = 38,
                Last = new int[] { 0, 1, 0, 0, 1 }
            }
        };

        [Fact]
        public void TestGetPlayers_CallsGetPlayersServiceMethod_Once()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayers())
                .Returns(GetPlayersPayload());

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayers();

            // Assert
            playerServiceMock.Verify(m => m.GetPlayers(), Times.Once);
        }

        [Fact]
        public void TestGetPlayers_ExpectStatusOk()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayers())
                .Returns(GetPlayersPayload());
            var expectedPlayers = GetPlayersPayload();

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayers();

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);

            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestGetPlayers_ReturnsExpectedPlayerList()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayers())
                .Returns(GetPlayersPayload());
            var expectedPlayers = new List<Player>
            {
                player1,
                player2
            };

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayers();

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var players = (List<Player>)result.Value;
            Assert.Equal(expectedPlayers.Count, players.Count);
            Assert.Equal(expectedPlayers[0].Id, players[0].Id);
            Assert.Equal(expectedPlayers[1].Id, players[1].Id);
        }

        [Fact]
        public void TestGetPlayer_CallsGetPlayerServiceMethod_Once()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayer(expectedPlayer.Id);

            // Assert
            playerServiceMock.Verify(m => m.GetPlayer(expectedPlayer.Id), Times.Once);
        }

        [Fact]
        public void TestGetPlayer_ExpectStatusOk()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayer(expectedPlayer.Id);

            // Assert
            Assert.IsAssignableFrom<OkObjectResult>(actionResult.Result);

            var result = actionResult.Result as OkObjectResult;
            Assert.NotNull(result);

            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestGetPlayer_ExpectStatusNotFound()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayer(It.IsAny<int>()))
                .Returns((Player)null);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayer(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(actionResult.Result);

            var result = actionResult.Result as NotFoundResult;
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void TestGetPlayer_ReturnsExpectedPlayer()
        {
            // Arrange
            var expectedPlayer = player1;
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.GetPlayer(expectedPlayer.Id);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var player = (Player)result.Value;
            Assert.Equal(player.Id, expectedPlayer.Id);
            Assert.Equal(player.Lastname, expectedPlayer.Lastname);
        }

        [Fact]
        public void TestDeletePlayer_CallsDeletePlayerServiceMethod_Once()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.DeletePlayer(It.IsAny<int>())).Returns(true);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            controller.DeletePlayer(player1.Id);

            // Assert
            playerServiceMock.Verify(m => m.DeletePlayer(player1.Id), Times.Once);
        }

        [Fact]
        public void TestDeletePlayer_ExpectStatusOk()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.DeletePlayer(It.IsAny<int>())).Returns(true);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.DeletePlayer(1);

            // Assert
            Assert.IsAssignableFrom<OkResult>(actionResult);

            var result = actionResult as OkResult;
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void TestDeletePlayer_ExpectStatusNotFound()
        {
            // Arrange
            var playerServiceMock = new Mock<IPlayerService>();
            playerServiceMock.Setup(playerService => playerService.DeletePlayer(It.IsAny<int>())).Returns(false);

            var controller = new PlayerController(playerServiceMock.Object);

            // Act
            var actionResult = controller.DeletePlayer(1);

            // Assert
            Assert.IsAssignableFrom<NotFoundResult>(actionResult);

            var result = actionResult as NotFoundResult;
            Assert.Equal(404, result.StatusCode);
        }

        private List<Player> GetPlayersPayload()
        {
            var players = new List<Player>
            {
                player1,
                player2
            };
            return players;
        }
    }
}
