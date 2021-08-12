using Application.Contracts.Response;
using Domain.Entities;
using System.Collections.Generic;

namespace Infrastructure.Integrations.Typicode.Interfaces
{
    public interface ITypicodeClient
    {
        ServiceResponse<List<Album>> GetAlbums();
        ServiceResponse<List<Album>> GetAlbumsByUserId(int userId);
        ServiceResponse<List<Photo>> GetPhotos(int albumId);
    }
}
