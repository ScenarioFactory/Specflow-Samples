namespace AutoWorkshop.Specs.Screenplay.BlobStorage.Questions
{
    using Abilities;
    using Azure.Storage.Blobs;
    using Pattern;

    public class StoredBlobExists : IQuestion<bool>
    {
        private readonly string _fileName;
        private string _containerName;

        private StoredBlobExists(string fileName)
        {
            _fileName = fileName;
        }

        public static StoredBlobExists WithName(string fileName)
        {
            return new StoredBlobExists(fileName);
        }

        public StoredBlobExists InContainer(string containerName)
        {
            _containerName = containerName;
            return this;
        }

        public bool AskAs(IActor actor)
        {
            var blobServiceClient = new BlobServiceClient(actor.Using<UseBlobStorage>().ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(_fileName);

            return blobClient.Exists();
        }
    }
}
