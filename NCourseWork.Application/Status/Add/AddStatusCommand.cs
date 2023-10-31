using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Add
{
    public class AddStatusCommand : IRequest<Guid>
    {
        public string StatusName { get; set; } = null!;
        public double DiscountPercentage { get; set; }
    }
}
