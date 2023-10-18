using System;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface ILibraryOperations : IFunctionalityMarker
	{
		/// <summary>
		/// Packs a project, and pushes to both the local directory and the remote NuGet package repositories.
		/// </summary>
		public void PackAndPushToLocalAndRemote()
		{
			/// Inputs.
			var libraryProjectFilePaths = new[]
			{
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.NG0002\source\R5T.NG0002\R5T.NG0002.csproj",
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0143\source\R5T.T0143\R5T.T0143.csproj",
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0028\source\R5T.F0028\R5T.F0028.csproj",
			};

			var packagesDirectoryPath = Instances.DirectoryPaths.LocalPackagesDirectoryPath;


			/// Run.
			using var services = Instances.ServicesOperator.GetServices();

			var logger = services.GetLogger(nameof(PackAndPushToLocalAndRemote));

			var nugetApiKey = Instances.Operations.GetNuGetApiKey();

			foreach (var libraryProjectFilePath in libraryProjectFilePaths)
			{
				var projectName = F0020.ProjectFileOperator.Instance.GetProjectName(libraryProjectFilePath);

				var packageFilePath = F0027.Instances.DotnetPackOperator.Pack(libraryProjectFilePath);

				var localDirectoryPackageFilePath = F0027.Instances.DotnetNugetPushOperator.Push_ToLocalDirectory(
					packageFilePath,
					packagesDirectoryPath);

                logger.LogInformation($"Pushing {projectName} to NuGet...");

				F0027.Instances.DotnetNugetPushOperator.Push_ToNuGet(
					// Push the exact version of the file that is in the local package directory.
					localDirectoryPackageFilePath,
					nugetApiKey);

				logger.LogInformation($"Pushed {projectName} to NuGet.");

				// Open the package's URL.
				var packageName = projectName;

				var packageFileNameStem = F0002.PathOperator.Instance.GetFileNameStem(packageFilePath);
				var packageVersion = packageFileNameStem[(packageName.Length + 1)..];

				var url = $"https://www.nuget.org/packages/{packageName}/{packageVersion}#versions-body-tab";

				F0000.WebOperator.Instance.OpenUrlInDefaultBrowser(url);
			}
		}

		public void PackAndPushToLocal()
        {
			/// Inputs.
			var libraryProjectFilePaths = new[]
			{
                //@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.NG0002\source\R5T.NG0002\R5T.NG0002.csproj",
                //@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0143\source\R5T.T0143\R5T.T0143.csproj",
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0028\source\R5T.F0028\R5T.F0028.csproj",
				//@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0142\source\R5T.T0142\R5T.T0142.csproj",
				//@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0147\source\R5T.T0147.T000\R5T.T0147.T000.csproj",
				//@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0147\source\R5T.T0147.T001\R5T.T0147.T001.csproj",
				//@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0132\source\R5T.T0132\R5T.T0132.csproj",
				//@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0147\source\R5T.T0147\R5T.T0147.csproj",
			};

            /// Run.
            foreach (var libraryProjectFilePath in libraryProjectFilePaths)
            {
				var packageFilePath = F0027.Instances.DotnetPackOperator.Pack(libraryProjectFilePath);

				F0027.Instances.DotnetNugetPushOperator.Push_ToLocalDirectory(
					packageFilePath,
					Instances.DirectoryPaths.LocalPackagesDirectoryPath);
            }

			/// Finish.
			// Open the packages directory in Windows Explorer.
			F0034.WindowsExplorerOperator.Instance._Platform.OpenDirectoryInExplorer(
				Instances.DirectoryPaths.LocalPackagesDirectoryPath);
		}
	}
}