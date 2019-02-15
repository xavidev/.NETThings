using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TDD.Api
{
    public class AuthenticationController : ApiController
    {
        private IAuthenticationProvider authenticationProvider;

        public AuthenticationController(IAuthenticationProvider authenticationProvider)
        {
            this.authenticationProvider = authenticationProvider;
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        public HttpResponseMessage Post(LoginModel login)
        {
            var result = authenticationProvider.ValidarUsuario(login.UserName, login.Password);
            HttpStatusCode code = HttpStatusCode.OK;
            if(!result)
            {
                code = HttpStatusCode.Unauthorized;
            }
            var response = this.Request.CreateResponse(code);
            response.Headers.Add("Authorization", "Bearer Token");
            return response;
        }
    }
}
