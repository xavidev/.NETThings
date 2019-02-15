using System;
namespace TDD.Api
{
    public class BasicAuthProvider : IAuthenticationProvider
    {
        public BasicAuthProvider()
        {
        }

        public bool ValidarUsuario(string userName, string password)
        {
            return true;
        }
    }
}
