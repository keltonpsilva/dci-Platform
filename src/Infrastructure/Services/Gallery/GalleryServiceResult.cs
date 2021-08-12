using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Services.Gallery
{
    public class GalleryServiceResult
    {
        public GalleryServiceResult()
        {
            Albums = new List<Album>();
        }

        public List<Album> Albums { get; set; }
    }
}
