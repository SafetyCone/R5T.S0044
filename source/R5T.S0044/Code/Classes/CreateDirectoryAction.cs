using System;

using R5T.T0142;


namespace R5T.S0044
{
    [UtilityTypeMarker]
    public class CreateDirectoryAction : IFileSystemAction
    {
        public string DirectoryPath { get; set; }
        public IFileSystem FileSystem { get; set; }


        public void Run()
        {
            this.FileSystem.CreateDirectory_Idempotent(DirectoryPath);
        }

        public override string ToString()
        {
            var representation = $"Create Directory:\n{this.DirectoryPath}";
            return representation;
        }
    }
}
