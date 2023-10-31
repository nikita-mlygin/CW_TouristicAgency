namespace NCourseWork.Persistence.Client.Get
{
    using Mapster;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GetClientByIdResponseMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetClientByIdResponse, Client>()
                .Map(dest => dest.Info, src => src);
        }
    }
}
