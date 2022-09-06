using System;


namespace R5T.S0044
{
	public class Try : ITry
	{
		#region Infrastructure

	    public static Try Instance { get; } = new();

	    private Try()
	    {
        }

	    #endregion
	}
}