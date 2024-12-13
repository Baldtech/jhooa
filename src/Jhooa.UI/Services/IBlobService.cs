namespace Jhooa.UI.Services;

public interface IBlobService
{
    IBlobContainerService GetBlobService(string blobContainerName);
}