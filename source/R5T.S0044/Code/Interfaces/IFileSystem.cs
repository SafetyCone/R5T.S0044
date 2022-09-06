using System;
using System.IO;

using R5T.T0142;


namespace R5T.S0044
{
    [UtilityTypeMarker]
    public interface IFileSystem
    {
        public void CreateDirectory_Idempotent(string directoryPath);
        public void DeleteDirectory_Idempotent(string directoryPath);
        public void DeleteFile_Idempotent(string directoryPath);
        public FileSystemEntry[] EnumerateFileSystemEntries(string directoryPath);

        public Stream OpenRead(string filePath);
        public Stream OpenWrite(string filePath);
    }
}
