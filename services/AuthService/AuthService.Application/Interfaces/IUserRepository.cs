using AuthService.Domain.Entities;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddUserAsync(User user);
    Task<User?> GetByIdAsync(int id);

}

