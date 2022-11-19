using System;

using R5T.T0131;


namespace R5T.S0044
{
	[ValuesMarker]
	public partial interface IDirectoryPaths : IValuesMarker
	{
		public string CloudBinariesDirectoryPath => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries";
		public string LocalPackagesDirectoryPath => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Packages";
	}
}