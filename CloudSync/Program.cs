using CloudSync;

var config = new Config();
var loadResult = config.Load();
if (!loadResult)
{
    Console.WriteLine("Configuration is not valid.");
    Console.Write("Press any key to exit...");
    Console.ReadKey(true);
    return;
}
Console.WriteLine("Configuration is valid.");

Loader.ProbeInternal();
Loader.ProbeExternal(config.PluginsDirectory);

IDatabase database;
IFileServer fileServer;

try
{
    Console.WriteLine("Initializing database provider...");
    database = Loader.LoadDatabaseProvider(config.DatabaseType);
    database.Initialize(config);

    Console.WriteLine("Initializing file server provider...");
    fileServer = Loader.LoadFileServerProvider(config.FileServerType);
    fileServer.Initialize(config);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
    Console.Write("Press any key to exit...");
    Console.ReadKey(true);
    return;
}
Console.WriteLine("AppId: " + config.AppId);

// check for failed post-sync
Routines.CheckAndResolveUnfinishedPostSync();

Console.WriteLine("Doing pre-sync...");
Console.WriteLine("Generating remote repository information...");
var remoteRepository = new RemoteRepository();
remoteRepository.GenerateFrom(database, fileServer, config.AppId);
Console.WriteLine("Generating local repository information...");
var localRepository = new LocalRepository(config.WatchDirectory);
Routines.DoPreSync(localRepository, remoteRepository);

// run app
Routines.RunApp(config);

Console.WriteLine("Doing post-sync...");
Console.WriteLine("Updating remote repository information...");
remoteRepository = new RemoteRepository();
remoteRepository.GenerateFrom(database, fileServer, config.AppId);
Console.WriteLine("Updating local repository information...");
localRepository = new LocalRepository(config.WatchDirectory);
Routines.DoPostSync(localRepository, remoteRepository);
