using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Get
{
    public class CountryFullInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Climate { get; set; } = null!;
    }
}
