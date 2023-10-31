using Mapster;
using NCourseWork.Application.Client.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Clients.Open
{
    internal class ClientViewDataMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ClientFullInfo, ClientViewData>()
                .Map(dest => dest.FirstName, src => src.ClientInfo.FirstName)
                .Map(dest => dest.LastName, src => src.ClientInfo.LastName)
                .Map(dest => dest.MiddleName, src => src.ClientInfo.MiddleName)
                .Map(dest => dest.PhoneNumber, src => src.ClientInfo.PhoneNumber)
                .Map(dest => dest.Address, src => src.ClientInfo.Address)
                .Map(dest => dest.StatusName, src => src.StatusName);
        }
    }
}
