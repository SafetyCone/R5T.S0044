using System;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface IExecutableOperations : IFunctionalityMarker
	{
		/// Prior work: <see cref="ITry.FirstArchive"/>.
		public async Task PublishToLocal()
        {
			/// Inputs.
			var projectFilePath =
                // Ithaca.
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.C0003\source\R5T.C0003\R5T.C0003.csproj"
                //// Porto
                //@"C:\Code\DEV\Git\GitHub\davidcoats\D8S.S0001.Private\source\D8S.S0001\D8S.S0001.csproj"
                //// Sulcis
                //@"C:\Code\DEV\Git\GitHub\davidcoats\D8S.S0003.Private\source\D8S.S0003\D8S.S0003.csproj"
                ;


            /// Run.
            await F0028.Instances.ServicesOperator.InServicesContext(
                services =>
                {
                    services.AddLogging(logging =>
                    {
                        logging
                            .SetMinimumLevel(LogLevel.Debug)
                            .AddConsole()
                            ;
                    })
                    ;
                },
                async services =>
                {
                    var logger = services.GetRequiredService<ILogger<ITry>>();

                    logger.LogInformation($"Archiving project:\n\t{projectFilePath}...");

                    logger.LogInformation("Checking if project is a library...");

                    var isLibrary = F0020.Instances.ProjectFileOperator.IsLibrary_Synchronous(projectFilePath);
                    if (isLibrary)
                    {
                        throw new Exception("This publish operations is not for libraries.");
                    }
                    else
                    {
                        // Is executable.
                        logger.LogInformation("(Good) Project is an executable, not a library.");

                        var executableProjectFilePath = projectFilePath;

                        // Publish to timestamped archive directory for program.
                        var publicationBinariesOutputDirectoryPath = Instances.PublicationOperator.GetPublicationBinariesOutputDirectoryPath(
                            Instances.DirectoryPaths.CloudBinariesDirectoryPath,
                            projectFilePath);

                        var timestampedBinariesDirectoryPath = Instances.PublicationOperator.GetTimestampedBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

                        logger.LogInformation($"Publishing project to directory...\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

                        await Instances.PublishOperator.Publish(
                            projectFilePath,
                            timestampedBinariesDirectoryPath);

                        logger.LogInformation($"Publishing project to directory.\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

                        // Copy files to current archive directory for program.
                        var currentBinariesOutputDirectoryPath = Instances.PublicationOperator.GetCurrentBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);
                        var priorBinariesOutputDirectoryPath = Instances.PublicationOperator.GetPriorBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

                        var fileSystemOperator = F0000.Instances.FileSystemOperator;

                        var currentDirectoryExists = fileSystemOperator.DirectoryExists(currentBinariesOutputDirectoryPath);
                        if (currentDirectoryExists)
                        {
                            logger.LogInformation($"Deleting prior directory...\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                            fileSystemOperator.DeleteDirectory_OkIfNotExists(priorBinariesOutputDirectoryPath);

                            logger.LogInformation($"Deleted prior directory.\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                            logger.LogInformation($"Copying current directory to prior directory...\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                            fileSystemOperator.CopyDirectory(
                                currentBinariesOutputDirectoryPath,
                                priorBinariesOutputDirectoryPath);

                            logger.LogInformation($"Copied current directory to prior directory.\n\tCurrent directory:\n\t{currentBinariesOutputDirectoryPath}\n\tPrior directory:\n\t{priorBinariesOutputDirectoryPath}");

                            logger.LogInformation($"Deleting current directory...\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");

                            fileSystemOperator.DeleteDirectory_OkIfNotExists(currentBinariesOutputDirectoryPath);

                            logger.LogInformation($"Deleted current directory.\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");
                        }

                        logger.LogInformation($"Copying timestamped directory to current directory...\n\tTimestamped directory:\n\t{currentBinariesOutputDirectoryPath}\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");

                        fileSystemOperator.CopyDirectory(
                            timestampedBinariesDirectoryPath,
                            currentBinariesOutputDirectoryPath);

                        logger.LogInformation($"Copied timestamped directory to current directory.\n\tTimestamped directory:\n\t{currentBinariesOutputDirectoryPath}\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");

                        Instances.WindowsExplorerOperator._Platform.OpenDirectoryInExplorer(currentBinariesOutputDirectoryPath);
                    }
                });
        }
	}
}