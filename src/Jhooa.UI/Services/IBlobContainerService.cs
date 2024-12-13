namespace Jhooa.UI.Services;

public interface IBlobContainerService
{
    Task<string> UploadAsync(string blobName, byte[] blobData);
    Task<string> UploadAsync(string blobName, Stream content);
    Task<List<string>> GetAllBlobs(string startsWith);
}