using System;

using Microsoft.Extensions.Logging;

using Renci.SshNet;

using R5T.T0132;


namespace R5T.S0044
{
	[DraftFunctionalityMarker]
	public partial interface IRemoteCommandOperator : IDraftFunctionalityMarker
	{
		public void LogCommandResult(
			SshCommand command,
			ILogger logger)
        {
            logger.LogInformation(command.Result);

            if (F0000.Instances.ExitCodeOperator.IsFailure(command.ExitStatus))
            {
                logger.LogError(command.Error);
            }
        }
	}
}