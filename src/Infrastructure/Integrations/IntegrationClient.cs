using Infrastructure.Integrations.Interfaces;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System;

namespace Infrastructure.Integrations
{
    public class IntegrationClient : IIntegrationClient
    {
        private readonly IRestClient _restClient;

        public IntegrationClient(IRestClient restClient)
        {
            _restClient = restClient;
            _restClient.UseNewtonsoftJson();
        }

        public IRestResponse ExecuteRequest(string baseUrl, IRestRequest request)
        {
            _restClient.BaseUrl = new Uri(baseUrl);

            IRestResponse response = _restClient.Execute(request);

            return HandlerResponse(response);

        }

        private IRestResponse HandlerResponse(IRestResponse response)
        {
            var baseErrorMessage = $"Failed execute {response.Request.Method} to '{_restClient.BaseUrl.OriginalString}/{response.Request.Resource}'";

            if (response.ErrorException != null || !string.IsNullOrEmpty(response.ErrorMessage))
                throw new ApplicationException($"{baseErrorMessage}: {response.ErrorMessage}", response.ErrorException);

            if (!response.IsSuccessful)
                throw new ApplicationException($"{baseErrorMessage}: {response.Content}");

            return response;
        }

        public T ExecuteRequest<T>(string baseUrl, IRestRequest request)
        {
            _restClient.BaseUrl = new Uri(baseUrl);

            IRestResponse<T> response = _restClient.Execute<T>(request);

            return HandlerResponse<T>(response);

        }

        private T HandlerResponse<T>(IRestResponse<T> response)
        {
            var baseErrorMessage = $"Failed execute {response.Request.Method} to '{_restClient.BaseUrl.OriginalString}/{response.Request.Resource}'";

            if (response.ErrorException != null || !string.IsNullOrEmpty(response.ErrorMessage))
                throw new ApplicationException($"{baseErrorMessage}: {response.ErrorMessage}", response.ErrorException);

            if (!response.IsSuccessful)
                throw new ApplicationException($"{baseErrorMessage}: {response.Content}");

            if (response.Data == null)
                throw new ApplicationException(baseErrorMessage);

            return response.Data;
        }
    }
}
