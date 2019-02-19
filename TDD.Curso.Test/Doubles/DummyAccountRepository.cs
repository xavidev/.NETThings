using System;
using TDD.Doubles;

namespace TDD.Curso.Test
{
    public class DummyAccountRepository : IAccountRepository
    {
        public UserAccount Save(UserAccount userAccount)
        {
            throw new TimeoutException();
        }
    }
}