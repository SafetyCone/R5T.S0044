using System;


namespace R5T.S0044
{
	public class ConfigurationOperator : IConfigurationOperator
	{
		#region Infrastructure

	    public static ConfigurationOperator Instance { get; } = new();

	    private ConfigurationOperator()
	    {
        }

	    #endregion
	}
}