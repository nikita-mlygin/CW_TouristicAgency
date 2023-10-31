using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get.All
{
    public class GetAllCountryQuery : IRequest<IEnumerable<CountryListItemInfo>>
    {
    }
}
