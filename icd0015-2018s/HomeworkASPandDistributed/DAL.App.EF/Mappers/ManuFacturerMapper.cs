using System;
using DAL.App.DTO.DomainLikeDTO;
using Domain;
using ee.itcollege.nikita.Contracts.DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ManuFacturerMapper : IBaseDALMapper
    {
        public TOutObject Map<TOutObject>(object inObject) where TOutObject : class
        {
            if (typeof(TOutObject) == typeof(DAL.App.DTO.DomainLikeDTO.ManuFacturer))
            {
                return MapFromDomain((Domain.ManuFacturer) inObject) as TOutObject;
            }

            if (typeof(TOutObject) == typeof(Domain.ManuFacturer))
            {
                return MapFromDAL((DAL.App.DTO.DomainLikeDTO.ManuFacturer) inObject) as TOutObject;
            }

            throw new InvalidCastException(
                $"No conversion from {inObject.GetType().FullName} to {typeof(TOutObject).FullName}");
        }

        public static DAL.App.DTO.DomainLikeDTO.ManuFacturer MapFromDomain(Domain.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new  DAL.App.DTO.DomainLikeDTO.ManuFacturer
            {
                Id = manuFacturer.Id,
                Aadress = manuFacturer.Aadress?.Translate(),
                ManuFacturerName = manuFacturer.ManuFacturerName?.Translate(),
                PhoneNumber = manuFacturer.PhoneNumber?.Translate()
            };

            return res;
        }

        public static Domain.ManuFacturer MapFromDAL(DAL.App.DTO.DomainLikeDTO.ManuFacturer manuFacturer)
        {
            var res = manuFacturer == null ? null : new Domain.ManuFacturer
            {
                Id = manuFacturer.Id,
                Aadress = new MultiLangString(manuFacturer.Aadress),
                ManuFacturerName = new MultiLangString(manuFacturer.ManuFacturerName),
                PhoneNumber = new MultiLangString(manuFacturer.PhoneNumber)
            };

            return res;
        }
    }
}