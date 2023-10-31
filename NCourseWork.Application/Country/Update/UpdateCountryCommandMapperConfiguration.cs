namespace NCourseWork.Application.Country.Update
{
    using Mapster;
    using NCourseWork.Domain.Country;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class UpdateCountryCommandMapperConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<UpdateCountryInfoCommand, Country>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Info.CountryName, src => src.Name)
            .Map(dest => dest.Info.Climate, src => src.Climate);
        }
    }
}
