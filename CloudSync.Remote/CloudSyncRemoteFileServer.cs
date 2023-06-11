namespace CloudSync
{
    [Database("cloudsync")]
    public class CloudSyncRemoteFileServer : IFileServer
    {
        public void Initialize(IConfig config)
        {

            config.SharedDataStore["CLOUDSYNC_FILESERVER_INIT", this] = true;
        }

        public void GetFile(string filename, string destination)
        {
            throw new NotImplementedException();
        }

        public bool HasFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void PutFile(string filename, string source)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
