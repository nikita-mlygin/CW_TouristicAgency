using Mapster;
using NCourseWork.Application.Purchase.Get;
using NCourseWork.Application.Purchase.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Purchase.Open
{
    internal class PurchaseUpdateInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurchaseFullInfo, PurchaseUpdateInfo>()
                .Map(dest => dest.SelectedTourViewInfo, src => src.TourFullInfo)
                .Map(dest => dest.TourId, src => src.TourFullInfo.Id);

            config.NewConfig<PurchaseUpdateInfo, UpdatePurchaseCommand>();
        }
    }
}
