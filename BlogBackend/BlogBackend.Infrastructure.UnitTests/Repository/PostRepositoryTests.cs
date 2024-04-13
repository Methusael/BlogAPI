//using BlogBackend.Domain.Models;
//using BlogBackend.Infrastructure.Repository;
//using NUnit.Framework;

//namespace BlogBackend.UnitTests.Services
//{
//    [TestFixture]
//    public class PostRepositoryTests : RepositoryTestBase<Post>
//    {
//        private List<Post> _initialData = default!;

//        [SetUp]
//        public void SetUp()
//        {
//            _repository = new Repository<Post>(_dbContext);
//            _initialData = new List<Post>()
//            {
//                new Post(Guid.NewGuid(),"Test Title1", "Test Description1"),
//                new Post(Guid.NewGuid(),"Test Title2", "Test Description3")
//            };

//            _dbContext.AddRange(_initialData);
//            _dbContext.SaveChanges();
//        }

//        [Test]
//        public async Task CreateAsync_ShouldAddOneNewPost()
//        {
//            Post postToBeAdded = new Post(Guid.NewGuid(), "Test Title", "Test Description");

//            Guid idOfAddedPost = await _repository.AddAsync(postToBeAdded, CancellationToken.None);
//            Post? actualPost = _dbContext.Posts.FirstOrDefault(post => post.Id == idOfAddedPost);

//            Assert.Multiple(() =>
//            {
//                Assert.That(actualPost, Is.Not.Null);
//                Assert.That(_dbContext.Posts.Count(), Is.EqualTo(_initialData.Count + 1));
//            });
//        }

//        [Test]
//        public async Task DeleteAsync_ShouldDeletePost()
//        {
//            await _repository.DeleteAsync(_initialData[0], CancellationToken.None);
//            Post? actualPost = _dbContext.Posts.FirstOrDefault(post => post.Id == _initialData[0].Id);

//            Assert.Multiple(() =>
//            {
//                Assert.That(actualPost, Is.Null);
//                Assert.That(_dbContext.Posts.Count(), Is.EqualTo(_initialData.Count - 1));
//            });
//        }

//        [Test]
//        public async Task GetAllAsync_ShouldGetAllPosts()
//        {
//            IReadOnlyList<Post> actualPosts = await _repository.GetAllAsync(CancellationToken.None);

//            Assert.Multiple(() =>
//            {
//                Assert.That(_dbContext.Posts.Count(), Is.EqualTo(_initialData.Count));
//                Assert.That(actualPosts, Is.EquivalentTo(_initialData));
//            });
//        }

//        [Test]
//        public async Task GetByIdAsync_ShouldGetPostById()
//        {
//            Post actualPost = await _repository.GetByIdAsync(_initialData[0].Id, CancellationToken.None);

//            Assert.Multiple(() =>
//            {
//                Assert.That(actualPost, Is.EqualTo(_initialData[0]));
//            });
//        }

//        [Test]
//        public async Task UpdateAsync_ShouldUpdatePost()
//        {
//            _initialData[0].Title = "Updated title";
//            _initialData[0].Content = "Updated content";
//            await _repository.UpdateAsync(_initialData[0], CancellationToken.None);
//            Post? actualPost = _dbContext.Posts.FirstOrDefault(x => x.Id == _initialData[0].Id);

//            Assert.Multiple(() =>
//            {
//                Assert.That(actualPost?.Title, Is.EqualTo(_initialData[0].Title));
//                Assert.That(actualPost?.Content, Is.EqualTo(_initialData[0].Content));
//            });
//        }
//    }
//}
