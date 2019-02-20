using System;
using TDD.Doubles;

namespace TDD.Curso.Test
{
    public class FakeAccountRepository : IAccountRepository
    {
        public UserAccount Save(UserAccount userAccount)
        {
            throw new TimeoutException();
        }
    }
}