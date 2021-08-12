using RestSharp;

namespace Infrastructure.Integrations.Interfaces
{
    public interface IIntegrationClient
    {
        T ExecuteRequest<T>(string baseUrl, IRestRequest request);
    }
}
