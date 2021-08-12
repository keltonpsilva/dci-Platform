using Infrastructure.Services.Gallery;
using Infrastructure.Services.Gallery.Contracts.Request;
using Infrastructure.Services.Gallery.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IGalleryService _galleryService;

        public GalleryController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }

        /// <summary>
        /// Get Gallery information
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/gallery
        /// 
        /// </remarks>
        /// <returns>Return basic Gallery information</returns>
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GalleryServiceResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Get(int totalOfRecords = 10)
        {
            var serviceResponse = _galleryService.Execute(new GalleryServiceRequest { TotalOfRecords = totalOfRecords });

            if (serviceResponse.Failed) {
                return NotFound();
            }

            return Ok(serviceResponse.Content);

        }

        /// <summary>
        /// Get Gallery information filter by user Id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// GET /api/gallery/1
        /// 
        /// </remarks>
        /// <param name="userId">The Id of the user</param>
        /// <returns>Return basic Gallery information</returns>
        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GalleryServiceResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult GetByUserId(int userId)
        {
            var serviceResponse = _galleryService.Execute(new GalleryServiceRequest { UserId = userId });

            if (serviceResponse.Failed) {
                return NotFound();
            }

            return Ok(serviceResponse.Content);

        }
    }
}
