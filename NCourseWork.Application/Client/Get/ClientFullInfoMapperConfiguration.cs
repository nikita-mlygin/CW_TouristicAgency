namespace NCourseWork.Application.Client.Get
{
    using Mapster;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ClientFullInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Client, ClientFullInfo>()
                .Map(dest => dest.ClientInfo, src => src.Info)
                .Map(dest => dest.StatusId, src => src.Status.Id)
                .Map(dest => dest.StatusName, src => src.Status.StatusName)
                .Map(dest => dest.DiscountPercentage, src => src.Status.DiscountPercentage);
        }
    }
}
