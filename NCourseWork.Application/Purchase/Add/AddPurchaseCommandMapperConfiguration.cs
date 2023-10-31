namespace NCourseWork.Application.Purchase.Add
{
    using Mapster;
    using NCourseWork.Domain.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AddPurchaseCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddPurchaseCommand, Purchase>()
                .Map(dest => dest.Client.Id, src => src.ClientId)
                .Map(dest => dest.Tour.Id, src => src.TourId);
        }
    }
}
