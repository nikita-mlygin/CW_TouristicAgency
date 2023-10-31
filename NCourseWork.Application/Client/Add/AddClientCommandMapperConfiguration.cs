

namespace NCourseWork.Application.Client.Add
{
    using Mapster;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class AddClientCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddClientCommand, Client>()
                .Map(dest => dest.Info, src => src);
        }
    }
}
