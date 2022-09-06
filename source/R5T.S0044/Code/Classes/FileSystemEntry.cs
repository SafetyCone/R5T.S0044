using System;

using R5T.T0142;


namespace R5T.S0044
{
    [DataTypeMarker]
    public sealed class FileSystemEntry : IEquatable<FileSystemEntry>
    {
        public bool IsDirectory { get; set; }
        public string Path { get; set; }
        public DateTime LastModified { get; set; }


        public bool Equals(FileSystemEntry other)
        {
            if(other is null)
            {
                // This cannot be null, since it exists!
                return false;
            }

            var output = true
                && this.Path == other.Path
                && this.IsDirectory == other.IsDirectory
                && this.LastModified == other.LastModified
                ;

            return output;
        }

        public override bool Equals(object obj)
        {
            // Sealed class, so no need to handle derived classes.
            if(obj is FileSystemEntry objAsEntry)
            {
                return this.Equals(objAsEntry);
            }

            // Else
            return false;
        }

        public override int GetHashCode()
        {
            var output = HashCode.Combine(
                this.Path,
                this.IsDirectory);

            return output;
        }
    }
}
