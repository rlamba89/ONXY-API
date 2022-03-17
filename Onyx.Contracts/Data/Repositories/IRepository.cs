namespace Onyx.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> Get(Func<T, bool> func);
    }
}