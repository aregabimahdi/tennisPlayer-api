using Moq;
using System;
using System.Collections.Generic;
using TennisPlayer.Api.Interfaces;
using TennisPlayer.Api.Models;
using TennisPlayer.Api.Providers;
using Xunit;

namespace TennisPlayer.Api.test.Providers
{
    public class TestPlayerProvider
    {
        private Mock<IDbContext> _dbContextMock;
        PlayerProvider _provider;

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

        public TestPlayerProvider()
        {
            _dbContextMock = new Mock<IDbContext>();
            _dbContextMock.Setup(context => context.GetContext())
                .Returns(GetPayload());

            _provider = new PlayerProvider(_dbContextMock.Object);
        }

        [Fact]
        public void TestGetPlayers_ReturnsExpectedPlayers()
        {
            //Arrange
            var expectedPlayers = GetPayload().Players;

            // Act
            var result = _provider.GetPlayers();

            // Assert
            Assert.Equal(result.Count, expectedPlayers.Count);
            Assert.Equal(result[0].Id, expectedPlayers[0].Id);
            Assert.Equal(result[1].Id, expectedPlayers[1].Id);
        }

        [Fact]
        public void TestGetPlayer_ReturnsExpectedPlayer()
        {
            //Arrange
            var expectedPlayer = GetPayload().Players[0];
           
            // Act
            var result = _provider.GetPlayer(expectedPlayer.Id);

            // Assert
            Assert.Equal(result.Id, expectedPlayer.Id);
            Assert.Equal(result.Firstname, expectedPlayer.Firstname);
        }

        [Fact]
        public void TestDeletePlayer_RemoveExpectedPlayer()
        {
            //Arrange
            var expectedPlayer = GetPayload().Players[0];

            // Act
            _provider.DeletePlayer(expectedPlayer);

            // Assert
            Assert.DoesNotContain(expectedPlayer, _provider.Players);
        }

        private Payload GetPayload()
        {
            var payload = new Payload
            {
                Players = new List<Player>
                {
                    player1,
                    player2
                }
            };

            return payload;
        }
    }
}
