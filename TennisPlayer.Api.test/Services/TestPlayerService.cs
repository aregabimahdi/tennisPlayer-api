using Moq;
using System;
using System.Collections.Generic;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;
using TennisPlayer.Api.Services;
using Xunit;

namespace TennisPlayer.Api.test.Services
{
    public class TestPlayerService
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
        public void TestGetPlayers_CallsGetPlayersProviderMethod_Once()
        {
            // Arrange
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayers())
                .Returns(GetPlayersPayload());

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.GetPlayers();

            // Assert
            playerProviderMock.Verify(m => m.GetPlayers(), Times.Once);
        }

        [Fact]
        public void TestGetPlayers_ReturnsSortedPlayersAsc()
        {
            // Arrange
            var unorderedPlayers = new List<Player>
            {
                player2,
                player1
            };
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayers())
                .Returns(unorderedPlayers);

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.GetPlayers();

            // Assert
            Assert.Equal(result.Count, unorderedPlayers.Count);
            Assert.Equal(result[0].Id, unorderedPlayers[1].Id);
            Assert.Equal(result[1].Id, unorderedPlayers[0].Id);
        }

        [Fact]
        public void TestGetPlayer_CallsGetPlayerProviderMethod_Once()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.GetPlayer(expectedPlayer.Id);

            // Assert
            playerProviderMock.Verify(m => m.GetPlayer(expectedPlayer.Id), Times.Once);
        }

        [Fact]
        public void TestGetPlayer_ReturnsExpectedPlayer()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var player = service.GetPlayer(expectedPlayer.Id);

            // Assert
            Assert.Equal(player.Id, expectedPlayer.Id);
            Assert.Equal(player.Lastname, expectedPlayer.Lastname);
        }

        [Fact]
        public void TestDeletePlayer_CallsGetPlayerProviderMethod_Once()
        {
            // Arrange
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayer(player2.Id))
                .Returns((Player)null);
            playerProviderMock.Setup(playerProvider => playerProvider.DeletePlayer(It.IsAny<Player>()));

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.DeletePlayer(player2.Id);

            // Assert
            playerProviderMock.Verify(m => m.GetPlayer(player2.Id), Times.Once);
        }

        [Fact]
        public void TestDeletePlayer_NeverCallDeletePlayerProviderMethod_WhenPlayerNotFound()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayer(expectedPlayer.Id))
                .Returns((Player)null);
            playerProviderMock.Setup(playerProvider => playerProvider.DeletePlayer(expectedPlayer));

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.DeletePlayer(expectedPlayer.Id);

            // Assert
            playerProviderMock.Verify(m => m.DeletePlayer(expectedPlayer), Times.Never);
            Assert.False(result);
        }

        [Fact]
        public void TestDeletePlayer_CallsDeletePlayerProviderMethodOnce_WhenPlayerExist()
        {
            // Arrange
            var expectedPlayer = player2;
            var playerProviderMock = new Mock<IPlayerProvider>();
            playerProviderMock.Setup(playerProvider => playerProvider.GetPlayer(expectedPlayer.Id))
                .Returns(expectedPlayer);
            playerProviderMock.Setup(playerProvider => playerProvider.DeletePlayer(expectedPlayer));

            var service = new PlayerService(playerProviderMock.Object);

            // Act
            var result = service.DeletePlayer(expectedPlayer.Id);

            // Assert
            playerProviderMock.Verify(m => m.DeletePlayer(expectedPlayer), Times.Once);
            Assert.True(result);
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
