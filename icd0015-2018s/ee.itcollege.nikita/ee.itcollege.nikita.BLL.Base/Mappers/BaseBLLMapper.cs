using AutoMapper;
using ee.itcollege.nikita.Contracts.BLL.Base.Mappers;

namespace ee.itcollege.nikita.BLL.Base.Mappers
{
    public class BaseBLLMapper<TBLLEntity, TDALEntity> : IBaseBLLMapper
        where TBLLEntity: class, new()
        where TDALEntity: class, new()

    {
        private readonly IMapper _mapper;

        public BaseBLLMapper()
        {
            _mapper = new MapperConfiguration(config =>
                {
                    config.CreateMap<TBLLEntity, TDALEntity>();
                    config.CreateMap<TDALEntity, TBLLEntity>();
                }
            ).CreateMapper();
        }
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            return _mapper.Map<TOutObject>(inObject);
        }
    }
}
