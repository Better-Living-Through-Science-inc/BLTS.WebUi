using BLTS.WebUi.DataAccessLayer;
using Microsoft.Extensions.DependencyInjection;

namespace BLTS.WebUi.Database
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DefaultDbContext _context;
        public UnitOfWork(IServiceScopeFactory serviceScopeFactory)
        {
            _context = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<DefaultDbContext>();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Releases the allocated resources for this context.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
