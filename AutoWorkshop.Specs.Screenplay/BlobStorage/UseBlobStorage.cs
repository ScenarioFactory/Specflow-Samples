namespace AutoWorkshop.Specs.Screenplay.BlobStorage
{
    using Pattern;

    public class UseBlobStorage : IAbility
    {
        public string ConnectionString { get; }

        private UseBlobStorage(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static UseBlobStorage With(string connectionString)
        {
            return new UseBlobStorage(connectionString);
        }
    }
}