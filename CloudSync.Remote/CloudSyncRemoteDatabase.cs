namespace CloudSync
{
    [Database("cloudsync")]
    public class CloudSyncRemoteDatabase : IDatabase
    {
        public void Initialize(string connectionString)
        {

            Config.SharedDataStore["CLOUDSYNC_DATABASE_INIT", this] = true;
        }

        public IReadOnlyList<DataEntry> GetAllEntriesFromAppId(string appId)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}
