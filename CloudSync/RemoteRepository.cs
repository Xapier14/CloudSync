namespace CloudSync
{
    public class RemoteRepository
    {
        private readonly List<RemoteDataNode> _dataNodes = new();
        public IReadOnlyCollection<RemoteDataNode> DataNodes => _dataNodes;

        /// <summary>
        /// Generates a RemoteRepository from a remote resource cache.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="fileServer"></param>
        /// <param name="appId"></param>
        public void GenerateFrom(IDatabase database, IFileServer fileServer, string appId)
        {
            _dataNodes.Clear();
            var remoteData = database.GetAllEntriesFromAppId(appId);
            foreach (var dataEntry in remoteData)
            {
                var remoteDataNode = new RemoteDataNode
                {
                    RelativePath = dataEntry.RelativePath,
                    Checksum = dataEntry.Checksum,
                    FileId = dataEntry.FileId,
                    IsDeleted = dataEntry.IsDeleted,
                };
                if (!remoteDataNode.IsDeleted
                    && !fileServer.HasFile(remoteDataNode.FileId))
                {
                    Console.WriteLine("[!] File with the id '{0}' does not exist in the specified file server.",
                        remoteDataNode.FileId);
                }
                _dataNodes.Add(remoteDataNode);
            }
        }
        /// <summary>
        /// Serializes a RemoteRepository into an XML file.
        /// </summary>
        /// <param name="remoteSyncFilePath"></param>
        public void SerializeIntoFile(string remoteSyncFilePath)
        {

        }

        internal void AddDataNode(RemoteDataNode remoteDataNode)
        {
            _dataNodes.Add(remoteDataNode);
        }
    }
}
