namespace NCourseWork.Application.Status.Update
{

    using Mapster;
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateStatusCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateStatusCommand, Status>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.StatusName, src => src.NewName)
            .Map(dest => dest.DiscountPercentage, src => src.NewDiscountPercentage);
        }
    }
}
