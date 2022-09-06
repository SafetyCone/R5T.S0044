using System;

using R5T.T0131;


namespace R5T.S0044
{
	[ValuesMarker]
	public partial interface IExampleProjectFilePaths : IValuesMarker
	{
		public string ExecutableProject => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0013\source\R5T.S0013\R5T.S0013.csproj";
		public string LibraryProject => @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.L0023\source\R5T.L0023\R5T.L0023.csproj";
	}
}