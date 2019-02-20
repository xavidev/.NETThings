using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TDD.Curso.Doubles;
using Moq;
using TDD.Doubles;

namespace TDD.Curso.Test
{
    public class TestDoublesDemo
    {
        private Mock<IAccountRepository> mockRepository;
        private UserAccount userAccount;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IAccountRepository>();
            userAccount = new UserAccount() { Name = "MyName", Surname = "Surname", Password = "MyPassword" };
        }

        [TearDown]
        public void TearDown()
        {
            userAccount = null;
            mockRepository = null;
        }

        [Test]
        public void ShouldSaveAccounts()
        {
            var newUser = userAccount;
            newUser.Id = 1;
            mockRepository.Setup(m=>m.Save(userAccount)).Returns(newUser);
            var accounts = new AccountManager(mockRepository.Object);
            var user = accounts.Save(userAccount);
            user.Id.Should().BeGreaterThan(0);
        }

        [Test]
        public void ShouldNotSaveAccountWithoutName()
        {
            var accounts = new AccountManager(mockRepository.Object);
            userAccount.Name = string.Empty;
            Assert.Throws(typeof(InvalidDataException), delegate {
                accounts.Save(userAccount);
            });
        }

        [Test]
        public void ShouldRetryThreeTimesWhenTimeout()
        {
            mockRepository.Setup(m => m.Save(It.IsAny<UserAccount>())).Throws<TimeoutException>();
            var accounts = new AccountManager(mockRepository.Object);

            accounts.Save(new UserAccount() { Name = "MyName", Surname = "Surname", Password = "MyPassword" });
            mockRepository.Verify(m => m.Save( It.IsAny<UserAccount>()),Times.Exactly(3));
        }
    }
}
