using System;
namespace TDD.Api
{
    public interface IAuthenticationProvider
    {
        bool ValidarUsuario(string userName, string password);
    }
}
