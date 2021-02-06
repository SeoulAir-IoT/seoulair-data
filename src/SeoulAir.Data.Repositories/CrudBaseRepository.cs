using AutoMapper;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories.Entities;
using SeoulAir.Data.Repositories.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeoulAir.Data.Repositories
{
    public class CrudBaseRepository<TDto, TEntity> : ICrudBaseRepository<TDto>
        where TDto : BaseDtoWithId
        where TEntity : BaseEntityWithId
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected readonly IMapper Mapper;

        public CrudBaseRepository(IMapper mapper, IMongoDbContext dbContext)
        {
            Mapper = mapper;
            Collection = dbContext.GetCollection<TEntity>();
        }

        public async Task<string> AddAsync(TDto dto)
        {
            TEntity entity = Mapper.Map<TEntity>(dto);
            entity.Id = null;

            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);

            await Collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<TDto> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, id);

            return Mapper.Map<TDto>(await Collection.Find(filter).SingleOrDefaultAsync());
        }

        public async Task<PaginatedResultDto<TDto>> GetPaginated(Paginator paginator)
        {
            var items = Collection.AsQueryable();
            if (paginator.FilterValue != null)
                items = items.FilterBy(paginator.FilterBy, paginator.FilterValue, paginator.FilterType);

            items = paginator.IsDescending ?
                items.OrderByDescending(paginator.OrderBy) :
                items.OrderBy(paginator.OrderBy);

            var count = await items.CountAsync();

            var result = await items.Skip(paginator.PageIndex - 1 * paginator.PageIndex)
                .Take(paginator.PageSize)
                .ToListAsync();

            return new PaginatedResultDto<TDto>()
            {
                PageIndex = paginator.PageIndex,
                PageSize = paginator.PageSize,
                Result = Mapper.Map<List<TDto>>(result),
                TotalRecords = count
            };
        }

        public async Task UpdateAsync(TDto dto)
        {
            TEntity entity = Mapper.Map<TEntity>(dto);

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            await Collection.FindOneAndReplaceAsync(filter, entity);
        }
    }
}
