using Abp.Configuration.Startup;

namespace Abp.AppFactory.Flow.Configuration
{
    public static class FlowConfigurationExtentions
    {
        public static FlowConfiguration SendGridConfiguration(this IModuleConfigurations moduleConfigurations)
        {
            return moduleConfigurations.AbpConfiguration.Get<FlowConfiguration>();
        }
    }
}