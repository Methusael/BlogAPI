using BlogBackend.Application.DTOs;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;

namespace BlogBackend.Application.Services
{
    public class PostService : IPostService
    {
        private IRepository<Post> _repository;

        public PostService(IRepository<Post> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<PostDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var posts = await _repository.GetAllAsync(cancellationToken);
            List<PostDTO> list = new List<PostDTO>();
            foreach (var post in posts)
            {
                list.Add(post);
            }

            return list;
        }

        public Task<IReadOnlyList<PostDTO>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<PostDTO> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(postId, cancellationToken);
        }

        public async Task<Guid> AddAsync(PostDTO postDTO, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(postDTO, cancellationToken);
        }

        public async Task UpdateAsync(PostDTO postDTO, CancellationToken cancellationToken)
        {
            await _repository.UpdateAsync(postDTO,cancellationToken);
        }

        public async Task DeleteByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            var postToBeDeleted = await _repository.GetByIdAsync(postId, cancellationToken);
            await _repository.DeleteAsync(postToBeDeleted, cancellationToken);
        }
    }
}
