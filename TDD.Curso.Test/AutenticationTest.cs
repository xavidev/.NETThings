
using NUnit.Framework;
using FluentAssertions;
using System.Net.Http;

namespace TDD.Curso.Test
{
    public class AutenticationTest
    {
        [SetUp]
        public void SetUp()
        {
            new BootStrap().ActivateBDD();
        }

        [TearDown]
        public void TearDown()
        {
            new BootStrap().DisableBDD();
        }

        [Test]
        public void DebemosRecibirUnaRespuestaExitosa()
        {
            using (var client = ClientFactory.GetClient())
            {
                var respuesta = client.GetAsync("").Result;
                respuesta.IsSuccessStatusCode.Should().BeTrue();
            }
        }

        [Test]
        [TestCase("xtoledano@test.org", "123456789!", true)]
        [TestCase("xtoledano@test.org", "wrongpass", false)]
        [TestCase("user@test.org", "authorizedpass", true)]
        public void PostLoginShouldReciveOk(string user, string pass, bool specResult)
        {
            using (var client = ClientFactory.GetClient())
            {
                var jsonLogin = new
                {
                    userName = user,
                    password = pass
                };

                var respuesta = client.PostAsJsonAsync("", jsonLogin).Result;

                respuesta
                    .IsSuccessStatusCode
                    .Should()
                    .Be(specResult, string.Format("Response code {0} message {1}", respuesta.StatusCode, respuesta.ReasonPhrase));
                respuesta.Headers
                    .Contains("Authorization")
                    .Should()
                    .BeTrue("Should contains an Authorization Header");
            }
        }
    }
}
