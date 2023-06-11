namespace CloudSync
{
    [FileServer("cloudinary")]
    public class CloudinaryFileServer : IFileServer
    {
        public void Initialize(IConfig config)
        {
            //throw new NotImplementedException();
        }

        public void GetFile(string filename, string destination)
        {
            // Get the file from the file server
            throw new NotImplementedException();
        }

        public void PutFile(string filename, string source)
        {
            // Put the file on the file server
            throw new NotImplementedException();
        }

        public bool HasFile(string filename)
        {
            throw new NotImplementedException();
        }

        public void DeleteFile(string filename)
        {
            // Delete the file from the file server
            throw new NotImplementedException();
        }
    }
}