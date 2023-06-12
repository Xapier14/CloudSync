using MongoDB.Driver;

namespace CloudSync;

[Database("mongodb")]
public class MongoDbDatabase : IDatabase
{
    private MongoClient? _client;
    private IClientSessionHandle? _handle;

    public void Initialize(IConfig config)
    {
        var mongoUrl = new MongoUrl(config.DatabaseConnectionString);
        _client = new MongoClient(mongoUrl);
        _handle = _client.StartSession();
        var clusterId = _handle.Client.Cluster.ClusterId.Value;
        Console.WriteLine($"Initialized MongoDB Connection. (ClusterId: {clusterId})");
    }

    public void Close()
    {
        _handle?.Dispose();
    }

    public IReadOnlyList<DataEntry> GetAllEntriesFromAppId(string appId)
    {
        throw new NotImplementedException();
    }
}