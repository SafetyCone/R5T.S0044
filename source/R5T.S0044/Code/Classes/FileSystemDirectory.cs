using System;

using R5T.T0142;


namespace R5T.S0044
{
    [DataTypeMarker]
    public class FileSystemDirectory
    {
        public string DirectoryPath { get; set; }
        public IFileSystem FileSystem { get; set; }
    }
}
