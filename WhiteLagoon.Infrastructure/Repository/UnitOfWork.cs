using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IVillaRepository Villa { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _dbContext = context;

            Villa = new VillaRepository(_dbContext);
        }
    }
}
