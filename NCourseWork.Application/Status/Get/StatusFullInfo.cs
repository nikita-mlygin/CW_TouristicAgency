using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Get
{
    public class StatusFullInfo
    {
        public Guid Id { get; set; }
        public string StatusName { get; set; } = null!;
        public double DiscountPercentage { get; set; }
    }
}
