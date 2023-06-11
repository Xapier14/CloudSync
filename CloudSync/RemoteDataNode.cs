namespace CloudSync
{
    public class RemoteDataNode : DataNode
    {
        public string FileId { get; init; } = Guid.NewGuid().ToString();
        public bool IsDeleted { get; init; }
    }
}
