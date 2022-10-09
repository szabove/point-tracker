namespace Application.Abstraction
{
    public interface IUserRepository
    {
        Task<List<Domain.Model.User>> GetAll();
    }
}
