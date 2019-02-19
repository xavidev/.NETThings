using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TDD.Curso.Doubles;

namespace TDD.Curso.Test
{
    public class TestDoublesDemo
    {
        [Test]
        public void ShouldSaveAccounts()
        {
            var accounts = new AccountManager();
            var user = accounts.Save(new UserAccount() { Name = "MyName", Surname = "Surname", Password = "MyPassword" });
            user.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldNotSaveAccountWithoutName()
        {
            var accounts = new AccountManager();
            Assert.Throws(typeof(InvalidDataException),
                            delegate {
                                accounts.Save(new UserAccount() { Surname = "Surname", Password = "MyPassword" });
                                     }
            );
        }

        [Test]
        public void ShouldRetryThreeTimesWhenTimeout()
        {
            var accounts = new AccountManager(new DummyAccountRepository());
            accounts.Save(new UserAccount() { Name = "MyName", Surname = "Surname", Password = "MyPassword" });
            accounts.RetryNumber().Should().Be(3);
        }
    }
}
