using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Abp.AppFactory.Flow
{
    public class FlowResponse : IEmailResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public HttpContent Body { get; set; }
        public HttpResponseHeaders Headers { get; set; }

        public static async Task<FlowResponse> GetFlowResponse(HttpResponseMessage message)
        {
            await message.Content.ReadAsStringAsync();

            var response = new FlowResponse
            {
                StatusCode = message.StatusCode,
                Body = message.Content,
                Headers = message.Headers
            };

            return response;
        }
    }
}