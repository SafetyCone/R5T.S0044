using System;

using R5T.T0131;


namespace R5T.S0044
{
	[ValuesMarker]
	public partial interface IDirectoryPaths : IValuesMarker
	{
		/// <summary>
		/// Directory is backed-up to the cloud, and shared across computers by the cloud.
		/// (A Dropbox directory.)
		/// </summary>
		public string CloudBinariesDirectoryPath => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Binaries";
		public string LocalPackagesDirectoryPath => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Packages";
	}
}