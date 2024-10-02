using HubCar.Services;

namespace HubCar.Maui.Services
{
    public class FileService : IFileService
    {
        public async Task<Stream> OpenFileAsync(string filePath)
        {
            return await FileSystem.OpenAppPackageFileAsync(filePath);
        }
    }
}