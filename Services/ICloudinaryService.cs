namespace EcommerceAPI.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(string base64Image);
    }
}
