using Abp.AppFactory.Flow.Configuration;
using Abp.AppFactory.Interfaces;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Abp.AppFactory.Flow
{
    public class FlowModule : AbpModule
    {
        private readonly IHostingEnvironment env;
        private IConfigurationRoot _appConfiguration;

        public FlowModule(IHostingEnvironment env)
        {
            this.env = env;
        }

        public override void PreInitialize()
        {
            IocManager.Register<FlowConfiguration>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _appConfiguration = builder.Build();

            Configuration.Modules.SendGridConfiguration().FlowKey = _appConfiguration["Flow:Key"];
            Configuration.Modules.SendGridConfiguration().FlowEndpoint = _appConfiguration["Flow:Endpoint"];
            IocManager.Register<IEmailService, Flow>(DependencyLifeStyle.Transient);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(FlowModule).GetAssembly());
        }
    }
}