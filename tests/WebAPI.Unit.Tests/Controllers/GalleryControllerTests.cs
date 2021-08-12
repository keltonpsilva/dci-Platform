using Application.Contracts.Response;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Infrastructure.Services.Gallery;
using Infrastructure.Services.Gallery.Contracts.Request;
using Infrastructure.Services.Gallery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Linq;
using WebAPI.Controllers;

namespace WebAPI.Unit.Tests.Controllers
{
    [TestFixture]
    public class GalleryControllerTests
    {

        private GalleryController _sut;

        private Fixture _fixture;

        private Mock<IGalleryService> galleryService;


        [SetUp]
        public void SetUp()
        {
            galleryService = new Mock<IGalleryService>();

            _fixture = new Fixture();

            _sut = new GalleryController(galleryService.Object);
        }

        [Test]
        public void Get_ValidRequest_ShouldReturnOKWithListOfAlbumsAndPhotos()
        {
            // Arrange

            var totalOfRecords = 25;
            var expecteResponse = new GalleryServiceResult { Albums = _fixture.CreateMany<Album>(totalOfRecords).ToList() };

            galleryService.Setup(g => g.Execute(It.IsAny<GalleryServiceRequest>())).Returns(ServiceResponse<GalleryServiceResult>.Success(expecteResponse));

            // Act
            var actionResult = _sut.Get();
            var actionResultContent = (GalleryServiceResult)(actionResult as OkObjectResult).Value;

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
            actionResultContent.Should().NotBeNull();
            actionResultContent.Should().BeEquivalentTo(expecteResponse);

        }
    }
}
