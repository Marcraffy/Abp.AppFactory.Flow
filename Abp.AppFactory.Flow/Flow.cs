using Abp.AppFactory.Flow.Configuration;
using Abp.AppFactory.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Abp.AppFactory.Flow
{
    public class Flow : IEmailService
    {
        private readonly FlowConfiguration config;

        private readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public Flow(FlowConfiguration config)
        {
            this.config = config;
        }

        public async Task<IEmailResponse> SendAsync(IEmail email)
        {
            var json = JsonConvert.SerializeObject(email, settings);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Secret", config.FlowKey);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(config.FlowEndpoint, content);
                return await FlowResponse.GetFlowResponse(response);
            }
        }

        [Obsolete]
        public async Task<IEmailResponse> SendAsync(ISendGridEmail email)
        {
            return await SendAsync(email);
        }
    }
}