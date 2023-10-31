using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Country.Delete
{
    public class DeleteCountryCommand : IRequest
    {
        public DeleteCountryCommand()
        {
        }

        public DeleteCountryCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
