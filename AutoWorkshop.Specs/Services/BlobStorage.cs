namespace AutoWorkshop.Specs.Services
{
    using Azure.Storage.Blobs;

    public class BlobStorage
    {
        private readonly AppSettings _appSettings;

        public BlobStorage(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public bool Exists(string containerName, string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_appSettings.BlobStorageConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(fileName);

            return blobClient.Exists();
        }
    }
}
