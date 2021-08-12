using Domain.Entities;
using FluentAssertions;
using Infrastructure.Integrations.Typicode;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Integration.Tests.Integrations.Typicode
{
    [TestFixture]

    public class TypicodeClientTests
    {
        private TypicodeClient _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new TypicodeClient(new RestClient(), new TypicodeConfigurations());
        }


        [Test]
        public void GetAlbums_ValidRequest_ShouldReturnListAlbums()
        {
            // Arrange
            var totalExpectedResult = 100;

            // Act
            var serviceResponse = _sut.GetAlbums();

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Album>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().Be(totalExpectedResult);
        }


        [Test]
        public void GetAlbumsByUserId_ValidUserId_ShouldReturnListAlbumsFromASpecificUserId()
        {
            // Arrange
            var userId = 1;

            // Act

            var serviceResponse = _sut.GetAlbumsByUserId(userId);

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Album>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().Be(10);
            serviceResponse.Content.All(i => i.UserId == userId).Should().BeTrue();
        }


        [Test]
        public void GetPhotos_InvalidAlbumId_ShouldReturnFailResponse()
        {
            // Arrange
            int albumId = 999;

            // Act
            var serviceResponse = _sut.GetPhotos(albumId);

            // Assert
            serviceResponse.Failed.Should().BeFalse();
            serviceResponse.Content.Should().BeEmpty();
        }

        [Test]
        public void GetPhotos_ExistingAlbumId_ShouldReturnListofPhotosFromASpecificAlbum()
        {
            // Arrange
            int albumId = 1;

            // Act
            var serviceResponse = _sut.GetPhotos(albumId);

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Photo>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().Be(50);
            serviceResponse.Content.All(i => i.AlbumId == albumId).Should().BeTrue();
        }
    }
}
