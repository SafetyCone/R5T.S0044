using System;


namespace R5T.S0044
{
	public class ExampleProjectFilePaths : IExampleProjectFilePaths
	{
		#region Infrastructure

	    public static ExampleProjectFilePaths Instance { get; } = new();

	    private ExampleProjectFilePaths()
	    {
        }

	    #endregion
	}
}