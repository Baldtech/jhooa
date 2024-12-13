using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Jhooa.UI.Models;
using Microsoft.IdentityModel.Tokens;

namespace Jhooa.UI.Services;

public class BlobContainerService(ILogger<BlobContainerService> logger, BlobContainerClient blobContainerClient)
    : IBlobContainerService
{
    public async Task<string> UploadAsync(string blobName, byte[] blobData)
    {
        await using var stream = new UploadStream(blobData);
        var result = await UploadAsync(blobName, stream);
        return result;
    }

    public async Task<string> UploadAsync(string blobName, Stream content)
    {
        try
        {
            var blobClient = blobContainerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(content, overwrite: true);

            logger.LogInformation("File '{FileName}' uploaded to '{BlobContainer}'", blobName,
                blobContainerClient.Name);

            return blobClient.Uri.AbsoluteUri;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to upload document {BlobName}", blobName);
            throw;
        }
    }

    public async Task<List<string>> GetAllBlobs(string startsWith)
    {
        try
        {
            var results = new List<string>();
            var blobClient = blobContainerClient.GetBlobsAsync(prefix: startsWith);
            await foreach (var blobItem in blobClient)
            {
                results.Add($"{blobContainerClient.Uri}/{blobItem.Name}");
            }

            return results;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to list items for {StartsWith}", startsWith);
            throw;
        }
    }
}