using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Get.ByCountry
{
    public class GetToursByCountryQuery : IRequest<IEnumerable<TourListItemInfo>>
    {
        public GetToursByCountryQuery(Guid id)
        {
            CountryId = id;
        }

        public Guid CountryId { get; set; }
    }
}
