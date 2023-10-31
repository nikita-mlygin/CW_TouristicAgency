namespace NCourseWork.Application.Country.Get
{
    using Mapster;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class CountryListItemInfoMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Country, CountryListItemInfo>()
                .Map(dest => dest.Name, src => src.Info.CountryName);
        }
    }
}
