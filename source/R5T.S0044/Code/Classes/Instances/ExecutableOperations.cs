using System;


namespace R5T.S0044
{
	public class ExecutableOperations : IExecutableOperations
	{
		#region Infrastructure

	    public static IExecutableOperations Instance { get; } = new ExecutableOperations();

	    private ExecutableOperations()
	    {
        }

	    #endregion
	}
}