using Mapster;
using NCourseWork.Application.Status.Get;
using NCourseWork.Application.Status.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Status.Open
{
    internal class OpenStatusInputValueMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<StatusFullInfo, OpenStatusInputValue>()
            .Map(dest => dest.Name, src => src.StatusName);

            config.NewConfig<OpenStatusInputValue, UpdateStatusCommand>()
            .Map(dest => dest.NewName, src => src.Name)
            .Map(dest => dest.NewDiscountPercentage, src => src.DiscountPercentage);
        }
    }
}
