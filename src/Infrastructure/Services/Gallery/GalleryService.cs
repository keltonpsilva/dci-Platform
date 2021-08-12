using Application.Contracts.Response;
using Domain.Entities;
using Infrastructure.Integrations.Typicode.Interfaces;
using Infrastructure.Services.Gallery.Contracts.Request;
using Infrastructure.Services.Gallery.Interfaces;
using System;
using System.Collections.Generic;

namespace Infrastructure.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly ITypicodeClient _typicodeClient;

        private int _totalOfRecords;

        public GalleryService(ITypicodeClient typicodeClient)
        {
            _typicodeClient = typicodeClient;
        }

        public ServiceResponse<GalleryServiceResult> Execute(GalleryServiceRequest request)
        {
            _totalOfRecords = request.TotalOfRecords;

            if (request.UserId.HasValue) {
                return Execute(request.UserId.Value);
            }

            return Execute();
        }

        private ServiceResponse<GalleryServiceResult> Execute()
        {
            var serviceResponse = _typicodeClient.GetAlbums();

            if (serviceResponse.Failed) {
                throw new ArgumentException($"Error {serviceResponse.ErrorMessage}");
            }

            var albums = serviceResponse.Content;

            var response = RequestAlbumPhotos(albums);

            return ServiceResponse<GalleryServiceResult>.Success(response);

        }

        private ServiceResponse<GalleryServiceResult> Execute(int userId)
        {
            var serviceResponse = _typicodeClient.GetAlbumsByUserId(userId);

            if (serviceResponse.Failed) {
                // We should log 
                throw new ArgumentException($"Error {serviceResponse.ErrorMessage}");
            }

            var albums = serviceResponse.Content;

            var response = RequestAlbumPhotos(albums);

            return ServiceResponse<GalleryServiceResult>.Success(response);
        }

        private GalleryServiceResult RequestAlbumPhotos(List<Album> albums)
        {
            var response = new GalleryServiceResult();

            for (int index = 0; index < _totalOfRecords; index++) {

                var album = albums[index];

                var getPhotosResponse = _typicodeClient.GetPhotos(album.Id);

                response.Albums.Add(new Album {
                    Id = album.Id,
                    Title = album.Title,
                    UserId = album.UserId,
                    Photos = getPhotosResponse.Content

                });
            }

            return response;
        }
    }
}
