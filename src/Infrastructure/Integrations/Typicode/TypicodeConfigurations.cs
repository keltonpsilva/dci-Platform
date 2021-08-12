using Infrastructure.Integrations.Typicode.Interfaces;

namespace Infrastructure.Integrations.Typicode
{
    public class TypicodeConfigurations : ITypicodeConfigurations
    {
        public string BaseUrl => "http://jsonplaceholder.typicode.com";
    }
}
