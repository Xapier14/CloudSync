using Microsoft.Extensions.Configuration;

namespace CloudSync
{
    public static class Config
    {
        private static readonly Dictionary<string, string> _pluginConfig = new();
        public static readonly SharedDataStore _sharedDataStore = new();
        public static IReadOnlyDictionary<string, string> PluginConfig => _pluginConfig;
        public static SharedDataStore SharedDataStore => _sharedDataStore;

        public static string AppId { get; private set; } = string.Empty;
        public static string LaunchTarget { get; private set; } = string.Empty;
        public static string WatchDirectory { get; private set; } = string.Empty;
        public static string WatchType { get; private set; } = string.Empty;
        public static string PluginsDirectory { get; private set; } = string.Empty;
        public static string DatabaseType { get; private set; } = string.Empty;
        public static string DatabaseConnectionString { get; private set; } = string.Empty;
        public static string FileServerType { get; private set; } = string.Empty;
        public static string FileServerConnectionString { get; private set; } = string.Empty;

        public static void Validate()
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

        public static bool Load(string configPath = "config.ini")
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