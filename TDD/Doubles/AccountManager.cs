using System;
using System.IO;
using TDD.Doubles;

namespace TDD.Curso.Doubles
{
    public class AccountManager
    {
        private IAccountRepository accountRepository;

        private int retryNumber;

        public AccountManager(): this(new AccountRepository()) { }

        public AccountManager(IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        public UserAccount Save(UserAccount userAccount)
        {
            if (string.IsNullOrEmpty(userAccount.Name))
            {
                throw new InvalidDataException();
            }

            UserAccount newUserAccount = new UserAccount();
            retryNumber = 0;

            while (retryNumber < 3)
            {
                try
                {
                    newUserAccount = accountRepository.Save(userAccount);
                    retryNumber = 4;
                }
                catch (TimeoutException ex)
                {
                    Console.WriteLine(ex.Message);
                    retryNumber++;
                }
            }

            return newUserAccount;
        }
    }
}


