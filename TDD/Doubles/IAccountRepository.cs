namespace TDD.Doubles
{
    public interface IAccountRepository
    {
        UserAccount Save(UserAccount userAccount);
    }
}