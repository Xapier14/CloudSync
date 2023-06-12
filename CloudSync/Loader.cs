using System.Reflection;

namespace CloudSync
{
    public static class Loader
    {
        private static readonly Dictionary<string, Type> _databaseProviders = new();
        private static readonly Dictionary<string, Type> _fileServerProviders = new();

        private static void ProbeAssembly(Assembly assembly)
        {
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                var databaseAttribute = type.GetCustomAttribute<DatabaseAttribute>();
                if (databaseAttribute != null)
                {
                    _databaseProviders.Add(databaseAttribute.Identifier, type);
                }

                var fileServerAttribute = type.GetCustomAttribute<FileServerAttribute>();
                if (fileServerAttribute != null)
                {
                    _fileServerProviders.Add(fileServerAttribute.Identifier, type);
                }
            }
        }

        public static void ProbeInternal()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                ProbeAssembly(assembly);
            }
        }

        public static void ProbeExternal(string path)
        {
            if (!Directory.Exists(path))
            {
                Console.WriteLine($"Warning, path '{path}' does not exist.");
                Console.WriteLine("Skipping external provider and plugin probing.");
                return;
            }

            var dlls = Directory.GetFiles(path, "*.dll");
            foreach (var dll in dlls)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(dll);
                    ProbeAssembly(assembly);
                }
                catch
                {
                }
            }
        }

        public static IDatabase LoadDatabaseProvider(string identifier)
        {
            if (!_databaseProviders.ContainsKey(identifier))
            {
                throw new Exception($"Database provider with identifier '{identifier}' not found.");
            }

            var provider = (IDatabase?)Activator.CreateInstance(_databaseProviders[identifier])
                ?? throw new Exception($"Database provider with identifier '{identifier}' could not be created.");
            return provider;
        }

        public static IFileServer LoadFileServerProvider(string identifier)
        {
            if (!_fileServerProviders.ContainsKey(identifier))
            {
                throw new Exception($"File server provider with identifier '{identifier}' not found.");
            }

            var provider = (IFileServer?)Activator.CreateInstance(_fileServerProviders[identifier])
                ?? throw new Exception($"File server provider with identifier '{identifier}' could not be created.");
            return provider;
        }
    }
}