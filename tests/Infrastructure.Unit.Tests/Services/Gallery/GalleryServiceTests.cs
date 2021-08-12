using Application.Contracts.Response;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Integrations.Typicode.Interfaces;
using Infrastructure.Services.Gallery;
using Infrastructure.Services.Gallery.Contracts.Request;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Unit.Tests.Services.Gallery
{
    [TestFixture]
    public class GalleryServiceTests
    {

        private GalleryService _sut;
        private Fixture _fixture;

        private Mock<ITypicodeClient> typicodeClient;

        [SetUp]
        public void SetUp()
        {
            typicodeClient = new Mock<ITypicodeClient>();

            _fixture = new Fixture();

            _sut = new GalleryService(typicodeClient.Object);
        }

        [Test]
        public void Execute_ExternalServiceFaild_ShouldThrowsArgumentException()
        {
            // Arrange
            typicodeClient.Setup(t => t.GetAlbums()).Returns(() => ServiceResponse<List<Album>>.Fail(string.Empty));

            // Act
            Func<object> resultFunction = () => _sut.Execute(new GalleryServiceRequest { TotalOfRecords = 1 });


            // Assert
            resultFunction.Should().Throw<ArgumentException>();
        }


        [Test]
        public void Execute_ValidRequest_ShouldReturnListOfAlbumsIncludedPhotos()
        {
            // Arrange
            var totalOfRecords = 25;
            var expectedAlbumsResponse = _fixture.CreateMany<Album>(totalOfRecords).ToList();
            var expectedPhotosResponse = _fixture.CreateMany<Photo>(totalOfRecords).ToList();

            typicodeClient.Setup(t => t.GetAlbums()).Returns(() => ServiceResponse<List<Album>>.Success(expectedAlbumsResponse));
            typicodeClient.Setup(t => t.GetPhotos(It.IsAny<int>())).Returns(() => ServiceResponse<List<Photo>>.Success(expectedPhotosResponse));

            // Act
            var response = _sut.Execute(new GalleryServiceRequest { TotalOfRecords = totalOfRecords });


            // Assert
            response.Succeeded.Should().BeTrue();
            response.Content.Albums.Count.Should().Be(totalOfRecords);
        }


    }
}
