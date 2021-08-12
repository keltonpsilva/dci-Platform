namespace Infrastructure.Services.Gallery.Contracts.Request
{
    public class GalleryServiceRequest
    {
        public int TotalOfRecords { get; set; } = 10;
        public int? UserId { get; set; }
    }
}
