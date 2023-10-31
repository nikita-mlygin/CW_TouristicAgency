namespace NCourseWork.Persistence.Country.Get
{
    using Mapster;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GetCountryByIdResponseMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetCountryByIdResponse, Country>()
                .Map(dest => dest.Info, src => src);
        }
    }
}
