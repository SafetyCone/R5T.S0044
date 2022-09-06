using System;


namespace R5T.S0044
{
	public class PublicationOperator : IPublicationOperator
	{
		#region Infrastructure

	    public static PublicationOperator Instance { get; } = new();

	    private PublicationOperator()
	    {
        }

	    #endregion
	}
}