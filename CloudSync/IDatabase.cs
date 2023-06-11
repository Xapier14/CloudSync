namespace CloudSync
{
    public readonly struct DataEntry
    {
        public string RelativePath { get; init; }
        public string Checksum { get; init; }
        public string FileId { get; init; }
        public long SizeInBytes { get; init; }
        public bool IsDeleted { get; init; }
    }
    public interface IDatabase
    {
        public void Initialize(string connectionString);

        public IReadOnlyList<DataEntry> GetAllEntriesFromAppId(string appId);

        public void Close();
    }
}