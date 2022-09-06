using System;

using R5T.T0132;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface IUrlOperator : IFunctionalityMarker
	{
		public string GetPackageVersionUrl(
			string packageName,
			string packageVersion)
        {
			var output = $"https://www.nuget.org/packages/{packageName}/{packageVersion}";
			return output;
        }
	}
}