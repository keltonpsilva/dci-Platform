using FluentAssertions;
using Infrastructure.Services.Gallery;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.Integration.tests.Controllers
{
    [TestFixture]
    public class GalleryControllerTests
    {
        private HttpClient Client;
        private readonly WebApplicationFactory<Startup> _factory;


        public GalleryControllerTests()
        {

            _factory = new WebApplicationFactory<Startup>();

            Client = _factory.CreateClient();

        }

        [Test]
        public async Task GetByUserId_UserId_ShouldReturnOKWithListOfAlbumsAndPhotosForASpecificUserId()
        {
            // Arrange
            var userId = 1;

            // Act
            var response = await Client.GetAsync($"api/gallery/{userId}");
            var responseContent = JsonConvert.DeserializeObject<GalleryServiceResult>(await response.Content.ReadAsStringAsync());

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(responseContent, Is.Not.Null);
            responseContent.Albums.All(a => a.UserId == userId).Should().BeTrue();

        }

    }
}
