using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Integrations.Typicode;
using Infrastructure.Integrations.Typicode.Contracts.Response.v1;
using Infrastructure.Integrations.Typicode.Interfaces;
using Moq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Infrastructure.Unit.Tests.Integrations.Typicode
{
    [TestFixture]
    public class TypicodeClientTests
    {
        private TypicodeClient _sut;

        private Fixture _fixture;

        private Mock<IRestClient> restClient;
        private Mock<ITypicodeConfigurations> typicodeConfigurations;

        [SetUp]
        public void SetUp()
        {
            var baseUrl = $"http://{Guid.NewGuid()}";

            _fixture = new Fixture() { RepeatCount = 100 };

            restClient = new Mock<IRestClient>();

            restClient.Setup(r => r.BaseUrl).Returns(new Uri(baseUrl));

            typicodeConfigurations = new Mock<ITypicodeConfigurations>();
            typicodeConfigurations.Setup(p => p.BaseUrl).Returns(baseUrl);

            _sut = new TypicodeClient(restClient.Object, typicodeConfigurations.Object);

        }

        [Test]
        public void GetAlbums_ValidRequest_ShouldReturnListAlbums()
        {
            // Arrange
            var expectedResponse = _fixture.Create<List<TypicodeAlbumResponse>>();

            var apiReponse = _fixture.Build<RestResponse<List<TypicodeAlbumResponse>>>()
                                 .With(p => p.Data, expectedResponse)
                                 .With(p => p.StatusCode, HttpStatusCode.OK)
                                 .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                 .With(p => p.Request, new RestRequest { Resource = Guid.NewGuid().ToString() })
                                 .Without(p => p.ErrorException)
                                 .Without(p => p.ErrorMessage)
                                 .WithAutoProperties()
                                 .Create();

            restClient.Setup(r => r.Execute<List<TypicodeAlbumResponse>>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act

            var serviceResponse = _sut.GetAlbums();

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Album>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().Equals(100);
        }

        [Test]
        public void GetAlbums_InvalidRequest_ShouldReturnFailResponse()
        {
            // Arrange
            var apiReponse = _fixture.Build<RestResponse<List<TypicodeAlbumResponse>>>()
                                     .With(p => p.StatusCode, HttpStatusCode.NotFound)
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .OmitAutoProperties()
                                     .Create();

            restClient.Setup(r => r.Execute<List<TypicodeAlbumResponse>>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act

            var serviceResponse = _sut.GetAlbums();

            // Assert
            serviceResponse.Failed.Should().BeTrue();
            serviceResponse.Content.Should().BeNull();
        }

        [Test]
        public void GetAlbumsByUserId_ValidUserId_ShouldReturnListAlbumsFromASpecificUserId()
        {
            // Arrange
            var userId = 1;
            var expectedResponse = _fixture.Build<TypicodeAlbumResponse>()
                                           .With(p => p.UserId, userId)
                                           .CreateMany(25).ToList();

            var apiReponse = _fixture.Build<RestResponse<List<TypicodeAlbumResponse>>>()
                                 .With(p => p.Data, expectedResponse)
                                 .With(p => p.StatusCode, HttpStatusCode.OK)
                                 .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                 .With(p => p.Request, new RestRequest { Resource = Guid.NewGuid().ToString() })
                                 .Without(p => p.ErrorException)
                                 .Without(p => p.ErrorMessage)
                                 .WithAutoProperties()
                                 .Create();

            restClient.Setup(r => r.Execute<List<TypicodeAlbumResponse>>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act

            var serviceResponse = _sut.GetAlbumsByUserId(userId);

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Album>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().BeLessOrEqualTo(25);
            serviceResponse.Content.All(i => i.UserId == userId).Should().BeTrue();
        }

        [Test]
        public void GetPhotos_InvalidAlbumId_ShouldReturnFailResponse()
        {
            // Arrange
            int albumId = 999;
            var apiReponse = _fixture.Build<RestResponse<List<TypicodePhotoResponse>>>()
                                     .With(p => p.StatusCode, HttpStatusCode.NotFound)
                                     .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                     .OmitAutoProperties()
                                     .Create();

            restClient.Setup(r => r.Execute<List<TypicodePhotoResponse>>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act

            var serviceResponse = _sut.GetPhotos(albumId);

            // Assert
            serviceResponse.Failed.Should().BeTrue();
            serviceResponse.Content.Should().BeNull();
        }

        [Test]
        public void GetPhotos_ExistingAlbumId_ShouldReturnListofPhotosFromASpecificAlbum()
        {
            // Arrange
            int albumId = 1;
            var expectedResponse = _fixture.Build<TypicodePhotoResponse>()
                                           .With(p => p.AlbumId, albumId)
                                           .CreateMany(10).ToList();

            var apiReponse = _fixture.Build<RestResponse<List<TypicodePhotoResponse>>>()
                                 .With(p => p.Data, expectedResponse)
                                 .With(p => p.StatusCode, HttpStatusCode.OK)
                                 .With(p => p.ResponseStatus, ResponseStatus.Completed)
                                 .With(p => p.Request, new RestRequest { Resource = Guid.NewGuid().ToString() })
                                 .Without(p => p.ErrorException)
                                 .Without(p => p.ErrorMessage)
                                 .WithAutoProperties()
                                 .Create();

            restClient.Setup(r => r.Execute<List<TypicodePhotoResponse>>(It.IsAny<RestRequest>()))
                      .Returns(() => apiReponse);

            // Act

            var serviceResponse = _sut.GetPhotos(albumId);

            // Assert
            serviceResponse.Content.Should().BeOfType(typeof(List<Photo>));
            serviceResponse.Content.Should().NotBeNull();
            serviceResponse.Content.Count.Should().BeLessOrEqualTo(25);
            serviceResponse.Content.All(i => i.AlbumId == albumId).Should().BeTrue();
        }

    }
}
