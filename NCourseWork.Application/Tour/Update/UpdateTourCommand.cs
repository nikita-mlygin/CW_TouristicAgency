using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Update
{
    public class UpdateTourCommand : IRequest
    {
        public Guid Id { get; set; }
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
    }
}
