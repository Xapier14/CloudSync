using Microsoft.Extensions.Configuration;

namespace CloudSync
{
    public class Config : IConfig
    {
        private readonly Dictionary<string, string> _pluginConfig = new();
        private readonly SharedDataStore _sharedDataStore = new();
        public IReadOnlyDictionary<string, string> PluginConfig => _pluginConfig;
        public ISharedDataStore SharedDataStore => _sharedDataStore;

        public string AppId { get; private set; } = string.Empty;
        public string LaunchTarget { get; private set; } = string.Empty;
        public string WatchDirectory { get; private set; } = string.Empty;
        public string WatchType { get; private set; } = string.Empty;
        public string PluginsDirectory { get; private set; } = string.Empty;
        public string DatabaseType { get; private set; } = string.Empty;
        public string DatabaseConnectionString { get; private set; } = string.Empty;
        public string FileServerType { get; private set; } = string.Empty;
        public string FileServerConnectionString { get; private set; } = string.Empty;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(AppId))
                throw new Exception("AppId is not set");
            if (string.IsNullOrWhiteSpace(LaunchTarget))
                throw new Exception("LaunchTarget is not set");
            if (string.IsNullOrWhiteSpace(WatchDirectory))
                throw new Exception("WatchDir is not set");

            if (string.IsNullOrWhiteSpace(DatabaseType))
                throw new Exception("DatabaseType is not set");
            if (string.IsNullOrWhiteSpace(FileServerType))
                throw new Exception("FileServerType is not set");
        }

        public bool Load(string configPath = "config.ini")
        {
            var config = new ConfigurationBuilder()
                .AddIniFile(configPath)
                .Build();

            var settingsSection = config.GetSection("Settings");
            AppId = settingsSection["AppId"] ?? string.Empty;
            LaunchTarget = settingsSection["LaunchTarget"] ?? string.Empty;
            WatchDirectory = settingsSection["WatchDirectory"] ?? string.Empty;
            WatchType = settingsSection["WatchType"] ?? string.Empty;
            PluginsDirectory = settingsSection["PluginsDirectory"] ?? "plugins";

            var connectionSection = config.GetSection("Connection");
            DatabaseType = connectionSection["DatabaseType"] ?? string.Empty;
            DatabaseConnectionString = connectionSection["DatabaseConnectionString"] ?? string.Empty;
            FileServerType = connectionSection["FileServerType"] ?? string.Empty;
            FileServerConnectionString = connectionSection["FileServerConnectionString"] ?? string.Empty;

            var pluginSection = config.GetSection("Plugin");
            foreach (var child in pluginSection.GetChildren())
            {
                _pluginConfig[child.Key] = child.Value ?? string.Empty;
            }

            try
            {
                Validate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}