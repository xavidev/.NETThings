using System;
using System.Net.Http;
using System.Web.Http.SelfHost;

namespace TDD.Curso.Test
{
    public static class ClientFactory
    {
        public static HttpClient GetClient()
        {
            var urlBase = new Uri("http://localhost:9874");
            var config = new HttpSelfHostConfiguration(urlBase);
            var server = new HttpSelfHostServer(config);
            new BootStrap().Configure(config);
            var client = new HttpClient(server);
            try
            {
                client.BaseAddress = urlBase;
                return client;
            }
            catch (Exception e)
            {
                client.Dispose();
                throw;
            }
        }
    }
}
