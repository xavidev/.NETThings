using System;
using System.Configuration;
using Simple.Data;

namespace TDD.Api
{
    public class BasicAuthProvider : IAuthenticationProvider
    {
        private dynamic db;

        public BasicAuthProvider()
        {
            var conn = ConfigurationManager.ConnectionStrings["Authentication"].ConnectionString;
            db = Database.OpenConnection(conn);
        }

        public bool ValidarUsuario(string userName, string password)
        {
            var user = db.Users.Find(db.Users.UserName == userName);

            return (user.Password == password);
        }
    }
}
