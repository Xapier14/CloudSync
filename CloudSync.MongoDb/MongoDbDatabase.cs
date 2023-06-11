namespace CloudSync
{
    [Database("mongodb")]
    public class MongoDbDatabase : IDatabase
    {
        public void Initialize(string connectionString)
        {
            // Initialize the database connection
            //throw new NotImplementedException();
        }

        public IReadOnlyList<DataEntry> GetAllEntries()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            // Close the database connection
            throw new NotImplementedException();
        }

        public IReadOnlyList<DataEntry> GetAllEntriesFromAppId(string appId)
        {
            throw new NotImplementedException();
        }
    }
}