namespace CloudSync
{
    public interface IConfig
    {
        public IReadOnlyDictionary<string, string> PluginConfig { get; }
        public ISharedDataStore SharedDataStore { get; }

        public string AppId { get; }
        public string LaunchTarget { get; }
        public string WatchDirectory { get; }
        public string WatchType { get; }
        public string PluginsDirectory { get; }
        public string DatabaseType { get; }
        public string DatabaseConnectionString { get; }
        public string FileServerType { get; }
        public string FileServerConnectionString { get; }

        public void Validate();
        public bool Load(string configPath);
    }
}
