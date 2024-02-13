using System;

using R5T.T0142;
using R5T.T0221;


namespace R5T.S0044
{
    [DataTypeMarker]
    public class FileSystemDifference
    {
        public string[] DirectoryPathsToCreate { get; set; }
        public string[] DirectoryPathsToDelete { get; set; }
        public FileCopyPair[] FilePathsToCopy { get; set; }
        public string[] FilePathsToDelete { get; set; }
    }
}
