using System;
using System.Collections.Generic;


namespace R5T.S0044
{
    public class FileSystemEntryPathEqualityComparer : IEqualityComparer<FileSystemEntry>
    {
        #region Static

        public static FileSystemEntryPathEqualityComparer Instance { get; } = new();

        #endregion


        public bool Equals(FileSystemEntry x, FileSystemEntry y)
        {
            var output = true
                && x.Path == y.Path
                && x.IsDirectory == y.IsDirectory
                ;

            return output;
        }

        public int GetHashCode(FileSystemEntry obj)
        {
            var output = HashCode.Combine(
                obj.Path,
                obj.IsDirectory);

            return output;
        }
    }
}
