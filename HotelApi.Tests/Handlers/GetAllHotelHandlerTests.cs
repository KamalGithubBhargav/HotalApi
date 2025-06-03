using HotelApi.Application.Handlers;
using HotelApi.Application.Queries;
using HotelApi.Domain.Entities;
using HotelApi.Domain.Repositories;
using Moq;
using FluentAssertions;

namespace HotelApi.Tests.Handlers
{
    public class GetAllHotelHandlerTests
    {
        private readonly Mock<IHotelRepository> _hotelRepositoryMock;
        private readonly GetAllHotelHandler _handler;

        public GetAllHotelHandlerTests()
        {
            _hotelRepositoryMock = new Mock<IHotelRepository>();
            _handler = new GetAllHotelHandler(_hotelRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsMappedHotelDetailsResponseList_WhenDataExists()
        {
            // Arrange
            var hotels = new List<HotelDetail>
            {
                new HotelDetail { Name = "Test Hotel", Rating = 4.5, Location = "Delhi", Images = new List<string> { "img1.jpg" } }
            };

            _hotelRepositoryMock
                .Setup(repo => repo.GetHotelsAsync(1, 10, null, 0, "name", It.IsAny<CancellationToken>()))
                .ReturnsAsync(hotels);

            var query = new GetAllHotelQuery(1, 10, null, 0, "name");

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(1);
            result.First().Name.Should().Be("Test Hotel");
            _hotelRepositoryMock.Verify(repo => repo.GetHotelsAsync(1, 10, null, 0, "name", It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoHotelsFound()
        {
            // Arrange
            _hotelRepositoryMock
                .Setup(repo => repo.GetHotelsAsync(1, 10, null, 0, "name", It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<HotelDetail>());

            var query = new GetAllHotelQuery(1, 10, null, 0, "name");

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task Handle_ThrowsException_WhenRepositoryFails()
        {
            // Arrange
            _hotelRepositoryMock
                .Setup(repo => repo.GetHotelsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string?>(), It.IsAny<double>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new Exception("Repository failure"));

            var query = new GetAllHotelQuery(1, 10, null, 0, "name");

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
        }
    }
}
