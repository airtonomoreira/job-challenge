using Api.Domain.Entities;

namespace Api.Domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<UserEntity> InsertAsync(UserEntity user);
    }
}
