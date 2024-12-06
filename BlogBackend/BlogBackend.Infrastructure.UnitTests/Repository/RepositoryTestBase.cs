using BlogBackend.Domain.Models;
using BlogBackend.Infrastructure.Data;
using BlogBackend.Infrastructure.Repository;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

namespace BlogBackend.UnitTests.Services
{
    [TestFixture]
    public class RepositoryTestBase<TEntity> where TEntity : BaseEntity
    {
        protected ApplicationDbContext _dbContext = default!;
        protected GenericRepository<TEntity> _repository = default!;

        private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("InMemoryDB").Options;

        [SetUp]
        public void SetUpBase()
        {
            _dbContext = new ApplicationDbContext(_dbContextOptions);
        }

        [TearDown]
        public void TearDownBase()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
