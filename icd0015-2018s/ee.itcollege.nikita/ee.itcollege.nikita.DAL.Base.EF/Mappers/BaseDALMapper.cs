using AutoMapper;
using ee.itcollege.nikita.Contracts.DAL.Base;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace ee.itcollege.nikita.DAL.Base.EF.Mappers
{
    public class BaseDALMapper<TDALEntity, TDomainEntity> : IBaseDALMapper
        where TDALEntity: class, new()
        where TDomainEntity: class, IDomainEntity, new()
    {
        private readonly IMapper _mapper;

        public BaseDALMapper()
        {
            _mapper = new MapperConfiguration(config =>
                {
                    config.CreateMap<TDALEntity, TDomainEntity>();
                    config.CreateMap<TDomainEntity, TDALEntity>();
                }
            ).CreateMapper();
        }
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            return _mapper.Map<TOutObject>(inObject);
        }
    }
}
