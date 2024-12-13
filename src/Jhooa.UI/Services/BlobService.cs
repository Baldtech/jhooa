using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;

namespace Jhooa.UI.Services;

public class BlobService(
    IAzureClientFactory<BlobServiceClient> factory,
    ILogger<BlobContainerService> logger,
    string clientName)
    : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient = factory.CreateClient(clientName);

    public IBlobContainerService GetBlobService(string blobContainerName)
    {
        return new BlobContainerService(logger, _blobServiceClient.GetBlobContainerClient(blobContainerName));
    }
}