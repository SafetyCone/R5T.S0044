using System;

using R5T.T0132;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface IPublicationOperator : IFunctionalityMarker
	{
		public string GetPriorBinariesOutputDirectoryPath(
			string publicationBinariesOutputDirectoryPath)
		{
			var priorBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				publicationBinariesOutputDirectoryPath,
				Instances.DirectoryNames.Prior);

			return priorBinariesOutputDirectoryPath;
		}

		public string GetCurrentBinariesOutputDirectoryPath(
			string publicationBinariesOutputDirectoryPath)
        {
			var currentBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				publicationBinariesOutputDirectoryPath,
				Instances.DirectoryNames.Current);

			return currentBinariesOutputDirectoryPath;
        }

		public string GetTimestampedBinariesOutputDirectoryPath(
			string publicationBinariesOutputDirectoryPath)
        {
			var nowLocal = F0000.Instances.NowOperator.GetNowLocal();

			var yyyymmdd_hhmmss = F0000.Instances.DateTimeOperator.ToString_YYYYMMDD_HHMMSS(nowLocal);

			// Just use the yyymmdd_hhmmss value since it can be a directory name.
			var timestampedDirectoryName = yyyymmdd_hhmmss;

			var timestampedBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				publicationBinariesOutputDirectoryPath,
				timestampedDirectoryName);

			return timestampedBinariesOutputDirectoryPath;
        }

		public string GetPublicationBinariesOutputDirectoryPath(
			string binariesOutputDirectoryPath,
			string projectFilePath)
        {
			var projectName = F0020.Instances.ProjectOperator.GetProjectName(projectFilePath);

			var publicationName = this.GetPublicationName(projectName);

			var publicationDirectoryName = this.GetPublicationDirectoryName(publicationName);

			var publicationBinariesOutputDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				binariesOutputDirectoryPath,
				publicationDirectoryName);

			return publicationBinariesOutputDirectoryPath;
        }

		public string GetPublicationDirectoryName(string publicationName)
        {
			// Just use the publication name, assuming the publication name can be a directory name.
			var publicationDirectoryName = publicationName;
			return publicationDirectoryName;
        }

		public string GetPublicationName(string projectName)
        {
			// Just use the project name, assuming the project name is unique.
			var publicationName = projectName;
			return publicationName;
        }
	}
}