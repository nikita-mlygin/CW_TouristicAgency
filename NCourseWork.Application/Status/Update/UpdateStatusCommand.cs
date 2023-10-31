using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Update
{
    public  class UpdateStatusCommand : IRequest
    {
        public Guid Id { get; set; }
        public string NewName { get; set; } = null!;
        public double NewDiscountPercentage { get; set; }
    }
}
