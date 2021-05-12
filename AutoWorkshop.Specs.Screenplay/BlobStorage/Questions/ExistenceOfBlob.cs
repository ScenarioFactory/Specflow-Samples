namespace AutoWorkshop.Specs.Screenplay.BlobStorage.Questions
{
    using Azure.Storage.Blobs;
    using Pattern;

    public class ExistenceOfBlob : IQuestion<bool>
    {
        private readonly string _fileName;
        private string _containerName;

        private ExistenceOfBlob(string fileName)
        {
            _fileName = fileName;
        }

        public static ExistenceOfBlob WithName(string fileName)
        {
            return new ExistenceOfBlob(fileName);
        }

        public ExistenceOfBlob InContainer(string containerName)
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
