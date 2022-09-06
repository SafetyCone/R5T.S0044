using System;

using R5T.T0131;


namespace R5T.S0044
{
	[ValuesMarker]
	public partial interface IDirectoryNames : IValuesMarker
	{
		/// <summary>
		/// _Current, with a leading underscore so that it sorts to the top.
		/// </summary>
		public string Current => "_Current";

		public string Prior => "_Prior";
	}
}