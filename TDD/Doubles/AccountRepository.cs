using System;
namespace TDD.Doubles
{
    public class AccountRepository : IAccountRepository
    {
        public AccountRepository()
        {
        }

        public UserAccount Save(UserAccount userAccount)
        {
            userAccount.Id = 1;
            return userAccount;
        }
    }
}
