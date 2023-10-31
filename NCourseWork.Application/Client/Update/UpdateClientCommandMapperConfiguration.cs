

namespace NCourseWork.Application.Client.Update
{
    using Mapster;
    using NCourseWork.Domain.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateClientCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateClientCommand, ClientInfo>()
                .Map(dest => dest.FirstName, src => src.UFirstName)
                .Map(dest => dest.LastName, src => src.ULastName)
                .Map(dest => dest.MiddleName, src => src.UMiddleName)
                .Map(dest => dest.Address, src => src.UAddress)
                .Map(dest => dest.PhoneNumber, src => src.UPhone);

            config.NewConfig<UpdateClientCommand, Client>()
                .Map(dest => dest.Info, src => src.Adapt<ClientInfo>());
        }
    }
}
