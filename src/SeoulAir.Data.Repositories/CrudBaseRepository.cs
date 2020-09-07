using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using SeoulAir.Data.Domain.Dtos;
using SeoulAir.Data.Domain.Interfaces.Repositories;
using SeoulAir.Data.Repositories.Attributes;
using SeoulAir.Data.Repositories.Entities;
using System;
using System.Threading.Tasks;

namespace SeoulAir.Data.Repositories
{
    public class CrudBaseRepository<TDto, TEntity> : ICrudBaseRepository<TDto>
        where TDto : BaseDtoWithId
        where TEntity : BaseEntityWithId
    {
        protected readonly IMongoCollection<TEntity> _collection;
        protected readonly IMapper _mapper;

        public CrudBaseRepository(IMapper mapper,IMongoDbContext _dbContext)
        {
            _mapper = mapper;
            _collection = _dbContext.GetCollection<TEntity>();
        }

        public async Task AddAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, new ObjectId(id));

            await _collection.FindOneAndDeleteAsync(filter);
        }

        public async Task<TDto> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(entity => entity.Id, new ObjectId(id));

            return _mapper.Map<TDto>(await _collection.Find(filter).SingleOrDefaultAsync());
        }

        public async Task UpdateAsync(TDto dto)
        {
            TEntity entity = _mapper.Map<TEntity>(dto);

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            await _collection.FindOneAndReplaceAsync(filter, entity);
        }
    }
}
