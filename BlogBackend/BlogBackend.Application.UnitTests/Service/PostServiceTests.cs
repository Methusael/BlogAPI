using BlogBackend.Application.DTOs;
using BlogBackend.Application.Services;
using BlogBackend.Domain.Interfaces;
using BlogBackend.Domain.Models;
using Moq;
using NUnit.Framework;

namespace BlogBackend.Application.UnitTests.Service
{
    [TestFixture]
    public class PostServiceTests
    {
        private PostService _postService;
        private Mock<IRepository<Post>> _repositoryMock;
        private List<Post> _posts;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<IRepository<Post>>(MockBehavior.Strict);
            _postService = new PostService(_repositoryMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldCreatePost()
        {
            Post postToBeCreated = new Post(Guid.NewGuid(), "Test Title", "Test Description");
            _repositoryMock.Setup(post => post.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>())).ReturnsAsync(postToBeCreated.Id);

            Guid idOfCreatedPost = await _postService.AddAsync(postToBeCreated, CancellationToken.None);

            Assert.That(idOfCreatedPost, Is.EqualTo(postToBeCreated.Id));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAll()
        {
            List<Post> posts = new List<Post>() {
                new Post(Guid.NewGuid(), "Test Title1", "Test Description2"),
                new Post(Guid.NewGuid(), "Test Title2", "Test Description2")
            };

            _repositoryMock.Setup(post => post.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(posts);

            IReadOnlyList<PostDTO> actualPosts = await _postService.GetAllAsync(CancellationToken.None);
            List<PostDTO> expectedPostsDTO = posts.Select(post => new PostDTO(post.Id, post.Title, post.Content)).ToList();

            Assert.That(actualPosts, Is.EquivalentTo(expectedPostsDTO));
        }
    }
}
