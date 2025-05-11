namespace Tuya.CreditCard.Api.DAL.Contracts.Repositories
{
    public interface IAdd<T> where T : class
    {
        Task<T?> AddAsync(T entity);
    }

    public interface IEdit<T> where T : class
    {
        Task<T?> EditAsync(T entity);
    }

    public interface IDelete<T> where T : class
    {
        Task<T?> DeleteAsync(Guid id);
    }

    public interface IGetById<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id);
    }

    public interface IGetAll<T> where T : class
    {
        Task<List<T>> GetAllAsync();
    }

    public interface IGetAllByUserId<T> where T : class
    {
        Task<List<T>> GetAllByUserIdAsync(Guid userId);
    }
}
