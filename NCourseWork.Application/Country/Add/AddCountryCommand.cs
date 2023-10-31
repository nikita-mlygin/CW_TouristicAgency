using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Add
{
    public class AddCountryCommand : IRequest<Guid>
    {
        public string CountryName { get; set; } = null!;
        public string Climate { get; set; } = null!;
    }
}
