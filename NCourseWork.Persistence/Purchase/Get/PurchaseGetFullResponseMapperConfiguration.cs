namespace NCourseWork.Persistence.Purchase.Get
{
    using Mapster;
    using NCourseWork.Domain.Purchase;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class PurchaseGetFullResponseMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PurchaseGetFullResponse, Purchase>()
                .Map(dest => dest.Tour, src => src)
                .Map(dest => dest.Tour.Id, src => src.TourId)
                .Map(dest => dest.Tour.Hotel, src => src)
                .Map(dest => dest.Tour.Hotel.Id, src => src.HotelId)
                .Map(dest => dest.Tour.Hotel.Country.Info, src => src)
                .Map(dest => dest.Tour.Hotel.Country.Id, src => src.CountryId)
                .Map(dest => dest.Client.Id, src => src.ClientId)
                .Map(dest => dest.Client.Info, src => src)
                .Map(dest => dest.Client.Status, src => src)
                .Map(dest => dest.Client.Status.Id, src => src.StatusId);
        }
    }
}
