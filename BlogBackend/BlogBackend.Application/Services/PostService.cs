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
            return posts.Select(post => new PostDTO(post.Id, post.Title, post.Content, post.TopicId)).ToList();
        }

        public async Task<IReadOnlyList<PostDTO>> GetByIdsAsync(IReadOnlyList<Guid> ids, CancellationToken cancellationToken)
        {
            var posts = await _repository.GetByIdsAsync(ids, cancellationToken);
            return posts.Select(post => new PostDTO(post.Id, post.Title, post.Content, post.TopicId)).ToList();
        }

        public async Task<PostDTO> GetByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(postId, cancellationToken);
        }

        public async Task<Guid> AddAsync(PostDTO postDTO, CancellationToken cancellationToken)
        {
            var postToBeAdded = new Post(postDTO.Id, postDTO.Title, postDTO.Content, postDTO.TopicId);

            return await _repository.AddAsync(postToBeAdded, cancellationToken);
        }

        public async Task UpdateAsync(PostDTO topicDto, CancellationToken cancellationToken)
        {
            var postToBeModified = await _repository.GetByIdAsync(topicDto.Id, cancellationToken);
            postToBeModified.Title = topicDto.Title;

            _repository.Update(postToBeModified);
        }

        public async Task DeleteByIdAsync(Guid postId, CancellationToken cancellationToken)
        {
            var postToBeDeleted = await _repository.GetByIdAsync(postId, cancellationToken);
            _repository.Delete(postToBeDeleted);
        }
    }
}
