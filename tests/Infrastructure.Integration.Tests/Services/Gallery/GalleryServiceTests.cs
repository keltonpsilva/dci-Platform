using FluentAssertions;
using Infrastructure.Integrations.Typicode;
using Infrastructure.Services.Gallery;
using Infrastructure.Services.Gallery.Contracts.Request;
using NUnit.Framework;
using RestSharp;

namespace Infrastructure.Integration.Tests.Services.Gallery
{
    [TestFixture]
    public class GalleryServiceTests
    {
        private GalleryService _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new GalleryService(new TypicodeClient(new RestClient(), new TypicodeConfigurations()));

        }

        [Test]
        public void Execute_ValidRequest_ShouldReturnListOfAlbumsIncludedPhotos()
        {
            // Arrange
            var totalExpectedAlbums = 10;

            // Act
            var response = _sut.Execute(new GalleryServiceRequest { TotalOfRecords = totalExpectedAlbums });

            // Assert
            response.Succeeded.Should().BeTrue();
            response.Content.Albums.Count.Should().Be(totalExpectedAlbums);
        }

        [Test]
        public void Execute_RequestByUserId_ShouldReturnListOfAlbumsIncludedPhotosForASpecificUserId()
        {
            // Arrange
            var totalExpectedAlbums = 10;
            var userId = 1;

            // Act
            var response = _sut.Execute(new GalleryServiceRequest { UserId = userId });

            // Assert
            response.Succeeded.Should().BeTrue();
            response.Content.Albums.Count.Should().Be(totalExpectedAlbums);
        }

    }
}
