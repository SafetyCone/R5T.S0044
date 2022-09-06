using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using R5T.T0141;


namespace R5T.S0044
{
	[TriesMarker]
	public partial interface ITry : ITriesMarker
	{
        public void DeployWebsiteByZipFile()
        {
            // Starts in: ...\bin\Debug\net5.0
            //F0000.Instances.CommandLineOperator.Run_Synchronous(
            //    "cmd.exe",
            //    "/c echo %cd%");

            // Ensure we are working with the right .NET version.
            //F0000.Instances.CommandLineOperator.Run_Synchronous(
            //    "dotnet",
            //    "--version");

            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\davidcoats\D8S.W0002.Private\source\D8S.W0002\D8S.W0002.csproj";
            var remoteDeployDirectoryPath = @"/var/www/D8S.W0002";
            var remoteServiceName = "D8S.W0002";

            var binariesDirectoryPath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries";

            /// Run.
            var archiveFileName = @"Archive.zip";
            var localTemporaryDirectoryPath = @"C:\Temp";
            var remoteTemporaryDirectoryPath = @"/home/ec2-user";

            // Publish to local timestamped directory.
            var publicationBinariesOutputDirectoryPath = Instances.PublicationOperator.GetPublicationBinariesOutputDirectoryPath(
                            binariesDirectoryPath,
                            projectFilePath);

            var timestampedBinariesDirectoryPath = Instances.PublicationOperator.GetTimestampedBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

            F0027.Instances.DotnetPublishOperator.Publish(
                projectFilePath,
                timestampedBinariesDirectoryPath);

            // Archive locally.
            var localArchiveFilePath = F0002.Instances.PathOperator.GetFilePath(
                localTemporaryDirectoryPath,
                archiveFileName);

            F0000.Instances.FileSystemOperator.DeleteFile_OkIfNotExists(localArchiveFilePath);

            ZipFile.CreateFromDirectory(
                timestampedBinariesDirectoryPath,
                localArchiveFilePath);

            // SFTP archive to remote.
            var awsRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

            var remoteArchiveFilePath = F0002.Instances.PathOperator.GetFilePath(
                remoteTemporaryDirectoryPath,
                archiveFileName);

            F0030.Instances.SshOperator.InConnectionContext(
                awsRemoteServerAuthentication,
                connection =>
                {
                    // Upload the file.
                    F0030.Instances.SftpOperator.InSftpContext_Connected_Synchronous(
                        connection,
                        sftpClient =>
                        {
                            using var fileStream = File.OpenRead(localArchiveFilePath);

                            sftpClient.UploadFile(fileStream, remoteArchiveFilePath, true);
                        });
                    
                    F0030.Instances.SshOperator.InSshContext_Connected_Synchronous(
                        connection,
                        sshClient =>
                        {
                            // Delete the directory, using SSH command.
                            var deleteDirectoryCommand = sshClient.RunCommand($"sudo rm -rf \"{remoteDeployDirectoryPath}\"");

                            // Unzip to the directory.
                            var unzipCommand = sshClient.RunCommand($"sudo unzip -o \"{remoteArchiveFilePath}\" -d \"{remoteDeployDirectoryPath}\"");

                            Console.WriteLine(unzipCommand.Result);

                            var changePermissionsCommand = sshClient.RunCommand($"sudo chmod +777 {remoteDeployDirectoryPath}");

                            // Restart the web server.
                            var restartCommand = sshClient.RunCommand($"sudo systemctl restart {remoteServiceName}");

                            Console.WriteLine(restartCommand.Result);

                            // Check status of the web server.
                            var statusCommand = sshClient.RunCommand($"sudo systemctl status {remoteServiceName}");

                            Console.WriteLine(statusCommand.Result);
                        });
                });
        }

        public void UnzipOfFile()
        {
            var archiveFilePath = @"/home/ec2-user/R5T.S0013_Current.zip";
            var destinationDirectoryPath = @"/home/ec2-user/R5T.S0013";

            var awsRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

            F0030.Instances.SshOperator.InSshContext_Synchronous(
                awsRemoteServerAuthentication,
                sshClient =>
                {
                    var command = sshClient.RunCommand($"unzip -o \"{archiveFilePath}\" -d \"{destinationDirectoryPath}\"");

                    Console.Write(command.Result);
                });
        }

        public void UploadFile()
        {
            var sourceFilePath = @"C:\Temp\R5T.S0013_Current.zip";
            var destinationFilePath = @"/home/ec2-user/R5T.S0013_Current.zip";

            var awsRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

            F0030.Instances.SftpOperator.InSftpContext_Synchronous(
                awsRemoteServerAuthentication,
                sftpClient =>
                {
                    using var fileStream = File.OpenRead(sourceFilePath);

                    sftpClient.UploadFile(fileStream, destinationFilePath, true);
                });

        }

        public void FirstZipOfFile()
        {
            var sourceDirectoryPath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries\R5T.S0013\_Current";
            var outputArchiveFilePath = @"C:\Temp\R5T.S0013_Current.zip";

            ZipFile.CreateFromDirectory(
                sourceDirectoryPath,
                outputArchiveFilePath);
        }

        public void ComputeFileSystemDifference()
        {
            // Source.
            var sourceDirectoryPath = @"";
            IFileSystem sourceFileSystem = null;
            //var fileSystemDirectory1 = new FileSystemDirectory
            //{
            //    DirectoryPath = directoryPath1,
            //    FileSystem = fileSystem1,
            //};

            // Destination.
            var destinationDirectoryPath = @"";
            IFileSystem destinationFileSystem = null;
            //var fileSystemDirectory2 = new FileSystemDirectory
            //{
            //    DirectoryPath = directoryPath2,
            //    FileSystem = fileSystem2,
            //};

            var sourceDirectoryFileSystemEntriesSet = sourceFileSystem.EnumerateFileSystemEntries(sourceDirectoryPath);
            var destinationDirectoryFileSystemEntriesSet = destinationFileSystem.EnumerateFileSystemEntries(destinationDirectoryPath);

            //IEnumerable<string> GetRelativePathsForChildren(
            //    string parentDirectoryPath,
            //    IEnumerable<string> childPaths)
            //{
            //    var parentDirectoryPathLength = parentDirectoryPath.Length;

            //    var output = childPaths.Select(childPath => childPath.Substring(parentDirectoryPathLength);
            //    return output;
            //}

            string GetRelativePathForChild(
                string parentDirectoryPath,
                string childPath)
            {
                var parentDirectoryPathLength = parentDirectoryPath.Length;

                var output = childPath[parentDirectoryPathLength..];
                return output;
            }

            IEnumerable<FileSystemEntry> GetRelativePathFileSystemEntries(
                string directoryPath,
                IEnumerable<FileSystemEntry> fileSystemEntries)
            {
                var output = fileSystemEntries
                    .Select(entry =>
                    {
                        var relativePath = GetRelativePathForChild(directoryPath, entry.Path);

                        var output = new FileSystemEntry
                        {
                            IsDirectory = entry.IsDirectory,
                            LastModified = entry.LastModified,
                            Path = relativePath,
                        };
                        return output;
                    });

                return output;
            }

            var sourceDirectoryRelativePathsFileSystemEntriesSet = GetRelativePathFileSystemEntries(
                sourceDirectoryPath,
                sourceDirectoryFileSystemEntriesSet);

            var destinationDirectoryRelativePathsFileSystemEntriesSet = GetRelativePathFileSystemEntries(
                destinationDirectoryPath,
                destinationDirectoryFileSystemEntriesSet);

            // Now compute relative paths difference.
            IEnumerable<FileSystemEntry> StandardizeDirectorySeparators(
               IEnumerable<FileSystemEntry> fileSystemEntries)
            {
                var output = fileSystemEntries
                    .Select(entry =>
                    {
                        var standardizedPath = Instances.PathOperator.EnsureDirectorySeparator(entry.Path, F0002.Instances.DirectorySeparators.Standard);

                        var output = new FileSystemEntry
                        {
                            IsDirectory = entry.IsDirectory,
                            LastModified = entry.LastModified,
                            Path = standardizedPath,
                        };
                        return output;
                    });

                return output;
            }

            var sourceDirectoryStandardizedRelativePathsFileSystemEntriesSet = StandardizeDirectorySeparators(sourceDirectoryRelativePathsFileSystemEntriesSet);
            var destinationDirectoryStandardizedRelativePathsFileSystemEntriesSet = StandardizeDirectorySeparators(destinationDirectoryRelativePathsFileSystemEntriesSet);

            var sourceDirectories = sourceDirectoryStandardizedRelativePathsFileSystemEntriesSet
                .Where(x => x.IsDirectory)
                .Now();
            var sourceFiles = sourceDirectoryStandardizedRelativePathsFileSystemEntriesSet
                .Where(x => !x.IsDirectory)
                .Now();

            var destinationDirectories = destinationDirectoryStandardizedRelativePathsFileSystemEntriesSet
                .Where(x => x.IsDirectory)
                .Now();
            var destinationFiles = destinationDirectoryStandardizedRelativePathsFileSystemEntriesSet
                .Where(x => !x.IsDirectory)
                .Now();

            // Directories to create.
            var relativeDirectoryPathsToCreate = sourceDirectories
                .Except(destinationDirectories, FileSystemEntryPathEqualityComparer.Instance)
                .Now();

            // Directories to delete.
            var relativeDirectoryPathsToDelete = destinationDirectories
                .Except(sourceDirectories, FileSystemEntryPathEqualityComparer.Instance)
                .Now();

            // Files to copy.
            var relativeFilePathsToWrite = sourceFiles
                .Except(destinationFiles, FileSystemEntryPathEqualityComparer.Instance)
                .Now();

            var sourceFileModifiedTimesByRelativePath = sourceFiles
                .ToDictionary(
                    x => x.Path,
                    x => x.LastModified);

            var destinationFileModifiedTimesByRelativePath = destinationFiles
                .ToDictionary(
                    x => x.Path,
                    x => x.LastModified);

            var relativeFilePathsToUpdate = sourceFiles
                .Intersect(destinationFiles, FileSystemEntryPathEqualityComparer.Instance)
                .Where(x => sourceFileModifiedTimesByRelativePath[x.Path] > destinationFileModifiedTimesByRelativePath[x.Path]);

            var relativeFilePathsToCopy = relativeFilePathsToWrite.Append(relativeFilePathsToUpdate).Now();

            // Files to delete.
            var relativeFilePathsToDelete = destinationFiles
                .Except(sourceFiles)
                .Now();

            // Create difference object.
            string GetPath(
                string directoryPath,
                string relativePath)
            {
                var output = directoryPath + relativePath;
                return output;
            }

            var destinationDirectoriesToCreate = relativeDirectoryPathsToCreate
                .Select(x => GetPath(
                    destinationDirectoryPath,
                    x.Path))
                .Now();

            var destinationDirectoriesToDelete = relativeDirectoryPathsToDelete
                .Select(x => GetPath(
                    destinationDirectoryPath,
                    x.Path))
                .Now();

            var destinationFilesToDelete = relativeFilePathsToDelete
                .Select(x => GetPath(
                    destinationDirectoryPath,
                    x.Path))
                .Now();

            // Match files to copy.
            var destinationFilePathsByRelativeFilePath = destinationFiles
                .ToDictionary(
                    x => GetRelativePathForChild(destinationDirectoryPath, x.Path),
                    x => x.Path);

            var sourceFilePathsByRelativeFilePath = sourceFiles
                .ToDictionary(
                    x => GetRelativePathForChild(sourceDirectoryPath, x.Path),
                    x => x.Path);

            var fileCopyPairs = relativeFilePathsToUpdate
                .Select(x =>
                {
                    var sourceFilePath = sourceFilePathsByRelativeFilePath[x.Path];
                    var destinationFilePath = destinationFilePathsByRelativeFilePath[x.Path];

                    var output = new FileCopyPair
                    {
                        DestinationFilePath = destinationFilePath,
                        SourceFilePath = sourceFilePath,
                    };

                    return output;
                })
                .Now();

            var fileSystemDifference = new FileSystemDifference
            {
                DirectoryPathsToCreate = destinationDirectoriesToCreate,
                DirectoryPathsToDelete = destinationDirectoriesToDelete,
                FilePathsToCopy = fileCopyPairs,
                FilePathsToDelete = destinationFilesToDelete,
            };

            // Validate file system differences.

            // Optimize file system difference.
            // Foreach directory to delete, if a parent directory is also a directory to delete, no need to delete it.


            // Foreach directory to create, if a child directory is also a directory to create, no need to create it.
            //var newDirectoriesToCreea

            // Foreach directory to be deleted, see if there are files to be deleted that are inside that directory.
            var newFilePathsToDelete = new List<string>(fileSystemDifference.FilePathsToDelete);

            foreach (var directoryPath in fileSystemDifference.DirectoryPathsToDelete)
            {
                foreach (var filePathToDelete in fileSystemDifference.FilePathsToDelete)
                {
                    if(filePathToDelete.StartsWith(directoryPath))
                    {
                        newFilePathsToDelete.Remove(filePathToDelete);
                    }
                }    
            }

            fileSystemDifference.FilePathsToDelete = newFilePathsToDelete.ToArray();

            // Create actions.
            var actions = new List<IFileSystemAction>();

            foreach (var directoryToDelete in fileSystemDifference.DirectoryPathsToDelete)
            {
                var action = new DeleteDirectoryAction
                {
                    DirectoryPath = directoryToDelete,
                    FileSystem = destinationFileSystem,
                };

                actions.Add(action);
            }

            //foreach (var directoryToCreate in collection)
            //{

            //}
        }

        public void FirstRecursiveDirectoryListing()
        {
            /// Inputs.
            var remoteDirectoryPath = @"/var/www/D8S.W0002";

            /// Run.
            var technicalBlogRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

            F0030.Instances.SftpOperator.InSftpContext_Synchronous(
                technicalBlogRemoteServerAuthentication,
                sftpClient =>
                {
                    var fileSystemEntries = F0030.Instances.SftpOperator.EnumerateFileSystemEntries_Recursive_Synchronous(
                        sftpClient,
                        remoteDirectoryPath);

                    var lines = fileSystemEntries
                        .Select(directoryEntry =>
                        {
                            var directoryIndicator = directoryEntry.IsDirectory ? "D" : "F";

                            var line = $"{directoryIndicator}: {directoryEntry.Name}";
                            return line;
                        })
                        .OrderAlphabetically()
                        ;

                    lines.ForEach(line => Console.WriteLine(line));
                });
        }

        public void FirstSftpDirectoryListing()
        {
            /// Inputs.
            var remoteDirectoryPath = @"/var/www/D8S.W0002";

            /// Run.
            var technicalBlogRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

            F0030.Instances.SftpOperator.InSftpContext_Unconnected_Synchronous(
                technicalBlogRemoteServerAuthentication,
                sftpClient =>
                {
                    sftpClient.Connect();

                    var directoryEntries = sftpClient.ListDirectory(remoteDirectoryPath);

                    var lines = directoryEntries
                        .Select(directoryEntry =>
                        {
                            var directoryIndicator = directoryEntry.IsDirectory ? "D" : "F";

                            var line = $"{directoryIndicator}: {directoryEntry.Name}";
                            return line;
                        })
                        .OrderAlphabetically()
                        ;

                    lines.ForEach(line => Console.WriteLine(line));
                });
        }

        public void FirstSsh()
        {
            /// Run.
            F0028.Instances.ServicesOperator.InServicesContext_Synchronous(
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
                services =>
                {
                    var logger = services.GetRequiredService<ILogger<ITry>>();

                    var technicalBlogRemoteServerAuthentication = Instances.Operations.GetTechnicalBlogRemoteServerAuthentication();

                    F0030.Instances.SshOperator.InSshContext_Synchronous(
                        technicalBlogRemoteServerAuthentication,
                        sshClient =>
                        {
                            sshClient.Connect();

                            using var command = sshClient.RunCommand("echo hello world!");

                            var result = command.Result;
                        });
                });
        }

        public void FirstPushToNuGet()
        {
            /// Inputs.
            var packageName = "R5T.L0023";
            var packageVersion = "0.0.4";
            
            /// Run.
            var packageFilePath =
                $@"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Packages\{packageName}.{packageVersion}.nupkg"
                ;

            F0028.Instances.ServicesOperator.InServicesContext_Synchronous(
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
                services =>
                {
                    var logger = services.GetRequiredService<ILogger<ITry>>();

                    var nugetAuthenticationJsonFilePath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Data\Secrets\Authentication-NuGet.json";

                    var configuration = F0029.Instances.ConfigurationOperator.BuildConfiguration_Synchronous(configurationBuilder =>
                    {
                        configurationBuilder
                            .AddJsonFile(nugetAuthenticationJsonFilePath)
                            ;
                    });

                    var nugetAuthentication = F0029.Instances.ConfigurationOperator.Get<NuGetAuthentication>(configuration); // configuration.GetSection(nameof(NuGetAuthentication)).Get<NuGetAuthentication>();

                    var apiKey = nugetAuthentication.API_Key;

                    int exitCode = F0027.Instances.DotnetCommandLineOperator.Run_InterceptErrorInOutput_ThrowIfError_Synchronous(
                            F0027.Instances.DotnetNugetOperator.BuildDotnetArguments(
                                F0027.Instances.DotnetNugetPushOperator.BuildNugetArguments(
                                    F0027.Instances.DotnetNugetPushOperator.BuildPushArguments_ToNuGet(
                                        packageFilePath,
                                        apiKey))));

                    //F0027.Instances.DotnetNugetPushOperator.Push_ToNuGet(
                    //    packageFilePath,
                    //    apiKey);

                    var packageVersionUrl = Instances.UrlOperator.GetPackageVersionUrl(
                        packageName,
                        packageVersion);

                    F0000.Instances.UrlOperator.OpenInBrowser(packageVersionUrl);
                });
        }

        public void FirstArchive()
        {
            /// Inputs.
            var projectFilePath =
                //Instances.ExampleProjectFilePaths.ExecutableProject
                //Instances.ExampleProjectFilePaths.LibraryProject
                @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.L0023\source\R5T.L0023\R5T.L0023.csproj"
                ;

            var binariesDirectoryPath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries";
            var packagesDirectoryPath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Packages";

            /// Run.
            F0028.Instances.ServicesOperator.InServicesContext_Synchronous(
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
                services =>
                {
                    var logger = services.GetRequiredService<ILogger<ITry>>();

                    logger.LogInformation($"Archiving project:\n\t{projectFilePath}...");

                    logger.LogInformation("Checking if project is a library...");

                    var isLibrary = F0020.Instances.ProjectFileOperator.IsLibrary_Synchronous(projectFilePath);
                    if (isLibrary)
                    {
                        logger.LogInformation("Project is a library.");

                        var libraryProjectFilePath = projectFilePath;

                        // Pack to NuGet package.
                        logger.LogInformation("Packing project...");

                        var packageFilePath = F0027.Instances.DotnetPackOperator.Pack(libraryProjectFilePath);

                        logger.LogInformation($"Packed project. Package file path:\n\t{packageFilePath}");

                        // Push NuGet package to packages directory.
                        logger.LogInformation($"Pushing project to packages directory...\n\tPackages directory:\n\t{packagesDirectoryPath}");

                        F0027.Instances.DotnetNugetPushOperator.Push_ToLocalDirectory(
                            packageFilePath,
                            packagesDirectoryPath);

                        logger.LogInformation($"Pushed project to packages directory.\n\tPackages directory:\n\t{packagesDirectoryPath}");
                    }
                    else
                    {
                        // Is executable.
                        logger.LogInformation("Project is an executable (not a library).");

                        var executableProjectFilePath = projectFilePath;

                        // Publish to timestamped archive directory for program.
                        var publicationBinariesOutputDirectoryPath = Instances.PublicationOperator.GetPublicationBinariesOutputDirectoryPath(
                            binariesDirectoryPath,
                            projectFilePath);

                        var timestampedBinariesDirectoryPath = Instances.PublicationOperator.GetTimestampedBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

                        logger.LogInformation($"Publishing project to directory...\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

                        F0027.Instances.DotnetPublishOperator.Publish(
                            projectFilePath,
                            timestampedBinariesDirectoryPath);

                        logger.LogInformation($"Publishing project to directory.\n\tPublish directory:\n\t{timestampedBinariesDirectoryPath}");

                        // Copy files to current archive directory for program.
                        var currentBinariesOutputDirectoryPath = Instances.PublicationOperator.GetCurrentBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);
                        var priorBinariesOutputDirectoryPath = Instances.PublicationOperator.GetPriorBinariesOutputDirectoryPath(publicationBinariesOutputDirectoryPath);

                        var fileSystemOperator = F0000.Instances.FileSystemOperator;

                        var currentDirectoryExists = fileSystemOperator.DirectoryExists(currentBinariesOutputDirectoryPath);
                        if(currentDirectoryExists)
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

                        logger.LogInformation($"Copyied timestamped directory to current directory.\n\tTimestamped directory:\n\t{currentBinariesOutputDirectoryPath}\n\tCurrent directory:\n\t{priorBinariesOutputDirectoryPath}");
                    }
                });
        }

        public void FirstPush()
        {
            var packageFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.L0023\source\R5T.L0023\bin\Release\R5T.L0023.1.0.0.nupkg";

            var packagesDirectoryPath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Packages";

            F0027.Instances.DotnetNugetPushOperator.Push_ToLocalDirectory(
                packageFilePath,
                packagesDirectoryPath);
        }

        public void FirstPack()
        {
            var libraryProjectFilePath = Instances.ExampleProjectFilePaths.LibraryProject;

            var _ = F0027.Instances.DotnetPackOperator.Pack(libraryProjectFilePath);
        }

        public void FirstPublish()
        {
            var libraryProjectFilePath = Instances.ExampleProjectFilePaths.LibraryProject;

            F0027.Instances.DotnetPublishOperator.Publish(libraryProjectFilePath);
        }
    }
}
