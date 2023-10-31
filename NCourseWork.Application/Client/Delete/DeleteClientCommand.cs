using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Delete
{
    public class DeleteClientCommand : IRequest
    {
        public DeleteClientCommand()
        {
        }

        public DeleteClientCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
