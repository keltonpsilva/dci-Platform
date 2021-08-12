using Application.Contracts.Response;
using Infrastructure.Services.Gallery.Contracts.Request;

namespace Infrastructure.Services.Gallery.Interfaces
{

    public interface IGalleryService
    {
        ServiceResponse<GalleryServiceResult> Execute(GalleryServiceRequest request);
    }
}
