using System;
using System.Threading.Tasks;


namespace R5T.S0044
{
    class Program
    {
        static async Task Main()
        {
            //Instances.LibraryOperations.PackAndPushToLocal();
            //Instances.LibraryOperations.PackAndPushToLocalAndRemote();

            //await Instances.ExecutableOperations.PublishToLocal();

            //Instances.Try.FirstPublish();
            //Instances.Try.FirstPack();
            //Instances.Try.FirstPush();
            //Instances.Try.FirstArchive();
            //Instances.Try.FirstPushToNuGet();
            //Instances.Try.FirstSsh();
            //Instances.Try.FirstSftpDirectoryListing();
            //Instances.Try.FirstRecursiveDirectoryListing();
            //Instances.Try.FirstZipOfFile();
            //Instances.Try.UploadFile();
            //Instances.Try.UnzipOfFile();

            //Instances.Try.DeployWebsiteByZipFile();

            /// Useful.
            Instances.Operations.OpenBinariesDirectory();
            //Instances.Operations.OpenPackagesDirectory();
        }
    }
}