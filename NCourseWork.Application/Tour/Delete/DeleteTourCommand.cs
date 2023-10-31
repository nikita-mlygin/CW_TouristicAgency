using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Tour.Delete
{
    public class DeleteTourCommand : IRequest
    {
        public DeleteTourCommand()
        {
        }

        public DeleteTourCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
