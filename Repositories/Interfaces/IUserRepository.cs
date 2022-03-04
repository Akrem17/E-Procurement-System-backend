using E_proc.Models;

namespace E_proc.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> ReadAsync();
        Task<User >Read(int id);
        Task<int> CreateAsync(User user);
        Task<User> UpdateAsync(int id, User user);
        Task<int> Delete(int id);
        Task<List<User>> FindBy(string? email, bool? confirmed,DateTime? date);
        Task<User> ResetPasswordAsync(User user, string token, string newPassword);


    }
}
