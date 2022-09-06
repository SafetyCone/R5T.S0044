using System;

using R5T.T0142;


namespace R5T.S0044
{
    [UtilityTypeMarker]
    public class CopyFileAction : IFileSystemAction
    {
        public string SourceFilePath { get; set; }
        public IFileSystem SourceFileSystem { get; set; }
        public string DestinationFilePath { get; set; }
        public IFileSystem DestinationFileSystem { get; set; }


        public void Run()
        {
            var read = this.SourceFileSystem.OpenRead(this.SourceFilePath);

            var write = this.DestinationFileSystem.OpenWrite(this.DestinationFilePath);

            read.CopyTo(write);
        }

        public override string ToString()
        {
            var representation = $"Copy File:\nSource: {this.SourceFilePath}\nDestination: {this.DestinationFilePath}";
            return representation;
        }
    }
}
