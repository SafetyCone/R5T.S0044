using System;


namespace R5T.S0044
{
	public class RemoteCommandOperator : IRemoteCommandOperator
	{
		#region Infrastructure

	    public static RemoteCommandOperator Instance { get; } = new();

	    private RemoteCommandOperator()
	    {
        }

	    #endregion
	}
}