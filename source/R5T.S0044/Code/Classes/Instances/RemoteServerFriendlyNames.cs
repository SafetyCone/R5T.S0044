using System;


namespace R5T.S0044
{
	public class RemoteServerFriendlyNames : IRemoteServerFriendlyNames
	{
		#region Infrastructure

	    public static RemoteServerFriendlyNames Instance { get; } = new();

	    private RemoteServerFriendlyNames()
	    {
        }

	    #endregion
	}
}