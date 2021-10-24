using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RPL.Core.ProjectAggregate;
using RPL.Infrastructure.Data;

namespace RPL.IntegrationTests.Data
{
    public abstract class BaseEfRepoTestFixture
    {
        protected MainDbContext _dbContext;

        protected static DbContextOptions<MainDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<MainDbContext>();
            builder.UseInMemoryDatabase("cleanarchitecture")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository<Project> GetRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new MainDbContext(options, mockMediator.Object);
            return new EfRepository<Project>(_dbContext);
        }
    }
}
