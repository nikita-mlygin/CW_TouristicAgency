namespace NCourseWork.Persistence.Purchase.Get
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
            config.NewConfig<PurchaseListInfoResponse, Purchase>()
                .Map(dest => dest.Client, src => src.ClientInfo)
                .Map(dest => dest.Tour, src => src.TourInfo)
                .Map(dest => dest.Id, src => src.PurchaseId);
        }
    }
}
