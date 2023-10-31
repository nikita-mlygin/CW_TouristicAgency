namespace NCourseWork.Application.Tour.Get.ByCountry
{
    using Mapster;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class GetToursByCountryQueryMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetToursByCountryQuery, Country>()
                .Map(dest => dest.Id, src => src.CountryId);
        }
    }
}
