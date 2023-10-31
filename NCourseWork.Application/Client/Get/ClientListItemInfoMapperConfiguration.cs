namespace NCourseWork.Application.Client.Get
{
    using Mapster;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class ClientListItemInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Client, ClientListItemInfo>()
                .Map(dest => dest, src => src.Info);
        }
    }
}
