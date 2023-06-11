using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CloudSync
{
    public static class Routines
    {
        public static void CheckAndResolveUnfinishedPostSync()
        {
            var remote = XmlRemoteInfo.GenerateRepositoryFromXML("testRemoteInfo.xml");
            XmlRemoteInfo.GenerateXMLFromRepository(remote, "generatedRemote.xml");
        }

        public static void DoPreSync(LocalRepository localRepository, RemoteRepository remoteRepository)
        {

        }

        public static void DoPostSync(LocalRepository localRepository, RemoteRepository remoteRepository)
        {

        }

        public static void RunApp(IConfig config)
        {
            var appPath = config.LaunchTarget;
            if (!File.Exists(appPath))
            {
                Console.Error.WriteLine("Launch target could not be found!");
                throw new NotImplementedException();
            }

            var process = Process.Start(appPath);
            Console.Error.WriteLine("Application launched.");
            process.WaitForExit();
            Console.Error.WriteLine("Application exited.");
        }
    }
}
