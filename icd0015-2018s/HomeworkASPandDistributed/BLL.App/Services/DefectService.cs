using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Domain;
using ee.itcollege.nikita.BLL.Base.Services;
using Defect = BLL.App.DTO.DomainLikeDTO.Defect;

namespace BLL.App.Services
{
    public class DefectService : BaseEntityService<BLL.App.DTO.DomainLikeDTO.Defect, DAL.App.DTO.DomainLikeDTO.Defect, IAppUnitOfWork>, IDefectService
    {
        public DefectService(IAppUnitOfWork uow) : base(uow, new DefectMapper())
        {
            ServiceRepository = Uow.Defects;
        }


        /*public override async Task<IEnumerable<Defect>> AllAsync()
        {
            return await UOW.Defects.AllAsync();
        }

        public override async Task<Defect> FindAsync(params object[] id)
        {
            return await UOW.Defects.FindAsync(id);
        }

        public async Task<IEnumerable<Defect>> AllAsyncByShop(int? shopId)
        {
            return await UOW.Defects.AllAsyncByShop(shopId);
        }

        public async Task<IEnumerable<DefectDTO>> GetAllWithProductsWithDefectByShopAsync(int? shopId)
        {
            return await UOW.Defects.GetAllWithProductsWithDefectByShopAsync(shopId);
        }

        public async Task<DefectDTO> FindProductsWithDefectByShopAsync(int id, int? shopId)
        {
            return await UOW.Defects.FindProductsWithDefectByShopAsync(id, shopId);
        }

         public async Task<int> CountDefectItems(int id)
        {
            return await UOW.Defects.CountDefectItems(id);
        }*/
        
        public async Task<int> CountDataAmount(string search)
        {
            return await Uow.Defects.CountDataAmount(search);
        }

        public async Task<List<Defect>> AllAsyncByShop(int? shopId, string order, string searchFor, int? pageIndex, int? pageSize)
        {
           return (await Uow.Defects.AllAsyncByShop(shopId, order, searchFor, pageIndex, pageSize)).Select(e => DefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task<List<Defect>> AllAsync(string order, string searchFor, int? pageIndex, int? pageSize)
        {
            return (await Uow.Defects.AllAsync(order, searchFor, pageIndex, pageSize)).Select(e => DefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task DeleteDefect(int defectId)
        {
            var productsWithDefect = await Uow.ProductsWithDefect.AllAsyncByDefectId(defectId);
            foreach (var productWithDefect in productsWithDefect)
            {
                Uow.ProductsWithDefect.Remove(productWithDefect.Id);
            }
            Uow.Defects.Remove(defectId);
            await Uow.SaveChangesAsync();
        }
        
        public async Task<int> CountDataAmount(int? shopId, string search)
        {
            return await Uow.Defects.CountDataAmount(shopId, search);
        }

        public override async Task<List<Defect>> AllAsync()
        {
            return (await Uow.Defects.AllAsync()).Select(e => DefectMapper.MapFromDAL(e)).ToList();
        }

        public override async Task<Defect> FindAsync(params object[] id)
        {
            return DefectMapper.MapFromDAL(await Uow.Defects.FindAsync(id));
        }

        public async Task<List<DefectWithProductCount>> GetAllWithProductsWithDefectByShopAsync(int? shopId, string search, int? pageIndex, int? pageSize)
        {
            return (await Uow.Defects.GetAllWithProductsWithDefectByShopAsync(shopId, search, pageIndex, pageSize)).Select(e => DefectMapper.MapFromDAL(e)).ToList();
        }

        public async Task<DefectWithProductCount> FindProductsWithDefectByShopAsync(int id, int? shopId)
        {
            return DefectMapper.MapFromDAL(await Uow.Defects.FindProductsWithDefectByShopAsync(id, shopId));
        }
    }
}