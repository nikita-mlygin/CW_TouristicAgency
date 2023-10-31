using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Persistence.Country.Get
{
    internal class GetCountryByNameResponse
    {
        public GetCountryByNameResponse()
        {
        }

        public Guid Id { get; set; }
        public string CountryName { get; set; } = null!;
    }
}
