using Mapster;
using NCourseWork.Application.Client.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Clients.Open
{
    internal class ClientUpdateInputDataMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
                config.NewConfig<ClientUpdateInputData, UpdateClientCommand>()
                    .Map(dest => dest.Id, src => Guid.NewGuid())
                    .Map(dest => dest.UFirstName, src => src.FirstName)
                    .Map(dest => dest.ULastName, src => src.LastName)
                    .Map(dest => dest.UMiddleName, src => src.MiddleName)
                    .Map(dest => dest.UAddress, src => src.Address)
                    .Map(dest => dest.UPhone, src => src.PhoneNumber);
        }
    }
}
