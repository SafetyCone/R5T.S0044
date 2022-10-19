using System;

using R5T.F0002;
using R5T.F0034;


namespace R5T.S0044
{
    public static class Instances
    {
        public static IConfigurationOperator ConfigurationOperator { get; } = S0044.ConfigurationOperator.Instance;
        public static IDirectoryNames DirectoryNames { get; } = S0044.DirectoryNames.Instance;
        public static IDirectoryPaths DirectoryPaths { get; } = S0044.DirectoryPaths.Instance;
        public static IExampleProjectFilePaths ExampleProjectFilePaths { get; } = S0044.ExampleProjectFilePaths.Instance;
        public static IExecutableOperations ExecutableOperations { get; } = S0044.ExecutableOperations.Instance;
        public static ILibraryOperations LibraryOperations { get; } = S0044.LibraryOperations.Instance;
        public static IOperations Operations { get; } = S0044.Operations.Instance;
        public static IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IPublicationOperator PublicationOperator { get; } = S0044.PublicationOperator.Instance;
        public static IRemoteCommandOperator RemoteCommandOperator { get; } = S0044.RemoteCommandOperator.Instance;
        public static IRemoteServerFriendlyNames RemoteServerFriendlyNames { get; } = S0044.RemoteServerFriendlyNames.Instance;
        public static IServicesOperator ServicesOperator { get; } = S0044.ServicesOperator.Instance;
        public static ITry Try { get; } = S0044.Try.Instance;
        public static IUrlOperator UrlOperator { get; } = S0044.UrlOperator.Instance;
        public static IWindowsExplorerOperator WindowsExplorerOperator { get; } = F0034.WindowsExplorerOperator.Instance;
    }
}