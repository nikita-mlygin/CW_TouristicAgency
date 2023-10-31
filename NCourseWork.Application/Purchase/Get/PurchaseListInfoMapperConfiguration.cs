namespace NCourseWork.Application.Purchase.Get
{
    using Mapster;
    using NCourseWork.Domain.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class PurchaseListInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Purchase, PurchaseListInfo>()
                .Map(dest => dest.ClientBaseInfo, src => src.Client)
                .Map(dest => dest.TourBaseInfo, src => src.Tour);
        }
    }
}
