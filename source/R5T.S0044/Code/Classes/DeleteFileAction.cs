using System;

using R5T.T0142;


namespace R5T.S0044
{
    [UtilityTypeMarker]
    public class DeleteFileAction : IFileSystemAction
    {
        public string FilePath { get; set; }
        public IFileSystem FileSystem { get; set; }


        public void Run()
        {
            this.FileSystem.DeleteFile_Idempotent(FilePath);
        }

        public override string ToString()
        {
            var representation = $"Delete File:\n{this.FilePath}";
            return representation;
        }
    }
}
