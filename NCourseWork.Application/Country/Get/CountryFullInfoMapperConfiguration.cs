namespace NCourseWork.Application.Country.Get
{
    using Mapster;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class CountryFullInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryFullInfo>()
                .Map(dest => dest, src => src.Info)
                .Map(dest => dest.Name, src => src.Info.CountryName);
        }
    }
}
