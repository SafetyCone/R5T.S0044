using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;

using R5T.T0132;
using R5T.T0144;


namespace R5T.S0044
{
	[FunctionalityMarker]
	public partial interface IOperations : IFunctionalityMarker
	{
        public string GetNuGetApiKey()
        {
            var nugetAuthenticationJsonFilePath = @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Data\Secrets\Authentication-NuGet.json";

            var nugetAuthentication = F0032.JsonOperator.Instance.LoadFromFile_Synchronous<NuGetAuthentication>(nugetAuthenticationJsonFilePath);

            var apiKey = nugetAuthentication.API_Key;
            return apiKey;
        }

        public Dictionary<string, RemoteServerAuthentication> GetAwsRemoteServerAuthenticationsByFriendlyName()
        {
            var awsRemoteServerConfigurationJsonFilePath = @"C:\Users\David\Dropbox\Organizations\David\Data\Secrets\Configuration-AWS Authentications.json";

            var configuration = F0029.Instances.ConfigurationOperator.BuildConfiguration_Synchronous(configurationBuilder =>
            {
                configurationBuilder
                    .AddJsonFile(awsRemoteServerConfigurationJsonFilePath)
                    ;
            });

            var awsRemoveServerConfigurations = F0029.Instances.ConfigurationOperator.Get<AwsServerConnectionSet>(configuration);

            var awsRemoteServerAuthenticationsByFriendlyName = Instances.ConfigurationOperator.GetRemoteServerAuthenticationsByFriendlyName(awsRemoveServerConfigurations);
            return awsRemoteServerAuthenticationsByFriendlyName;
        }

        public RemoteServerAuthentication GetTechnicalBlogRemoteServerAuthentication()
        {
            var awsRemoteServerAuthenticationsByFriendlyName = this.GetAwsRemoteServerAuthenticationsByFriendlyName();

            var technicalBlogRemoteServerAuthentication = awsRemoteServerAuthenticationsByFriendlyName[Instances.RemoteServerFriendlyNames.TechnicalBlog];
            return technicalBlogRemoteServerAuthentication;
        }
	}
}