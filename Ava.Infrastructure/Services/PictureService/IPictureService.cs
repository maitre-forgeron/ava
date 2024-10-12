namespace Ava.Infrastructure.Services.PictureService
{
    public interface IPictureService
    {
        public Task<string> UploadPictureAsync(Stream fileStream, string fileName);
        public Task DeletePictureAsync(string fileName);
        public Task<string> GetPictureUrl(string fileName);
    }
}
