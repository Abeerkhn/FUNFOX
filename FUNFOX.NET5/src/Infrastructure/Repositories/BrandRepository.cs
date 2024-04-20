sing FUNFOX.NET5.Application.Interfaces.Repositories;
using FUNFOX.NET5.Domain.Entities.Catalog;

namespace FUNFOX.NET5.Infrastructure.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly IRepositoryAsync<Brand, int> _repository;

        public BrandRepository(IRepositoryAsync<Brand, int> repository)
        {
            _repository = repository;
        }
    }
}