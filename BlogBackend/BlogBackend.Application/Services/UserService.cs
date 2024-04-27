using BlogBackend.Domain.Exceptions;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            var user = users.FirstOrDefault(x=> x.Email == email);
            if (user == null) throw new ItemNotFoundException(email);
            return user;
        }
    }
}
