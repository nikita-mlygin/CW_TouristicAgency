using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get.WithNameFilter
{
    public class GetCountryWithFilterQuery : IRequest<IEnumerable<CountryListItemInfo>>
    {
        public GetCountryWithFilterQuery()
        {
        }

        public GetCountryWithFilterQuery(string nameFilter)
        {
            NameFilter = nameFilter;
        }

        public string? NameFilter { get; set; } = null;
        public string? ClimateNameFilter { get; set; } = null;
    }
}
