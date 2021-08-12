using Domain.Entities;
using Infrastructure.Integrations.Typicode.Contracts.Response.v1;

namespace Infrastructure.Integrations.Mappers
{
    public static class IntegrationsMappers
    {
        public static Album ToAlbum(TypicodeAlbumResponse typicodeAlbumResponse)
        {
            return new Album {
                Id = typicodeAlbumResponse.Id,
                Title = typicodeAlbumResponse.Title,
                UserId = typicodeAlbumResponse.UserId
            };
        }

        public static Photo ToPhoto(TypicodePhotoResponse typicodePhotoResponse)
        {
            return new Photo {
                AlbumId = typicodePhotoResponse.AlbumId,
                Id = typicodePhotoResponse.Id,
                ThumbnailUrl = typicodePhotoResponse.ThumbnailUrl,
                Title = typicodePhotoResponse.Title,
                Url = typicodePhotoResponse.Url,
            };
        }
    }
}
