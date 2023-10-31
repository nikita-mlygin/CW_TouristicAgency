using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Add
{
    public class AddTourCommand : IRequest<Guid>
    {
        public double PricePerWeek { get; set; }
        public Guid HotelId { get; set; }
        public Guid CountryId { get; set; }
    }
}
