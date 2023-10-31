using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Hotel.Delete
{
    public class DeleteHotelCommand : IRequest
    {
        public DeleteHotelCommand()
        {
        }

        public DeleteHotelCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
