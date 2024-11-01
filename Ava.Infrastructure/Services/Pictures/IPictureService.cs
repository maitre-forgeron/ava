namespace Ava.Infrastructure.Services.Pictures;

public interface IPictureService
{
    public Task<string> UploadPictureAsync(Stream fileStream, string fileName);
    public Task DeletePictureAsync(string fileName);
    public Task<string> GetPictureUrl(string fileName);
}
