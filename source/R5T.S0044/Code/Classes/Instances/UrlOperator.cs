using System;


namespace R5T.S0044
{
	public class UrlOperator : IUrlOperator
	{
		#region Infrastructure

	    public static UrlOperator Instance { get; } = new();

	    private UrlOperator()
	    {
        }

	    #endregion
	}
}