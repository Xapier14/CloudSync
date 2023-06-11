namespace CloudSync
{
    /// <summary>
    /// Represents an abstracted flat file system.
    /// </summary>
    public interface IFileServer
    {
        public void Initialize(string connectionString);
        public void GetFile(string filename, string destination);
        public bool HasFile(string filename);
        public void PutFile(string filename, string source);
        public void DeleteFile(string filename);
    }
}