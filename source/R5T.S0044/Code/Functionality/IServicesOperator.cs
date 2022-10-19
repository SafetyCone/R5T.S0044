using System;

using Microsoft.Extensions.DependencyInjection;

using R5T.T0132;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface IServicesOperator : IFunctionalityMarker
	{
		public ServiceProvider GetServices()
        {
			var output = F0028.ServicesOperator.Instance.BuildServiceProvider(
				F0035.ServicesOperator.Instance.AddLogging);

			return output;
        }
	}
}