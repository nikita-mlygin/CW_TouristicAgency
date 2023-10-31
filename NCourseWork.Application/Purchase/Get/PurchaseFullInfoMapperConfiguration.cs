namespace NCourseWork.Application.Purchase.Get
{
    using Mapster;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Purchase;


    internal class PurchaseFullInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Purchase, PurchaseFullInfo>()
                .Map(dest => dest.ClientFullInfo, src => src.Client)
                .Map(dest => dest.TourFullInfo, src => src.Tour);
        }
    }
}
