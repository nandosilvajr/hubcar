namespace HubCar.Services
{
    public interface IFileService
    {
        Task<Stream> OpenFileAsync(string filePath);
    }
}