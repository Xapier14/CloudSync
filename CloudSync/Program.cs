using CloudSync;

var loadResult = Config.Load();
if (!loadResult)
{
    Console.WriteLine("Configuration is not valid.");
    Console.Write("Press any key to exit...");
    Console.ReadKey(true);
    return;
}
Console.WriteLine("Configuration is valid.");

Loader.ProbeInternal();
Loader.ProbeExternal(Config.PluginsDirectory);

IDatabase database;
IFileServer fileServer;

try
{
    Console.WriteLine("Initializing database provider...");
    database = Loader.LoadDatabaseProvider(Config.DatabaseType);
    database.Initialize(Config.DatabaseConnectionString);

    Console.WriteLine("Initializing file server provider...");
    fileServer = Loader.LoadFileServerProvider(Config.FileServerType);
    fileServer.Initialize(Config.FileServerConnectionString);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.Write("Press any key to exit...");
    Console.ReadKey(true);
    return;
}
Console.WriteLine("AppId: " + Config.AppId);

// check for failed post-sync
Routines.CheckAndResolveUnfinishedPostSync();

Console.WriteLine("Doing pre-sync...");
Console.WriteLine("Generating remote repository information...");
var remoteRepository = new RemoteRepository();
remoteRepository.GenerateFrom(database, fileServer, Config.AppId);
Console.WriteLine("Generating local repository information...");
var localRepository = new LocalRepository(Config.WatchDirectory);
Routines.DoPreSync(localRepository, remoteRepository);

// run app
Routines.RunApp();

Console.WriteLine("Doing post-sync...");
Console.WriteLine("Updating remote repository information...");
remoteRepository = new RemoteRepository();
remoteRepository.GenerateFrom(database, fileServer, Config.AppId);
Console.WriteLine("Updating local repository information...");
localRepository = new LocalRepository(Config.WatchDirectory);
Routines.DoPostSync(localRepository, remoteRepository);
