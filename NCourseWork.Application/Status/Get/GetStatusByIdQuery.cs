using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Get
{
    public class GetStatusByIdQuery : IRequest<StatusFullInfo?>
    {
        public GetStatusByIdQuery()
        {
        }

        public GetStatusByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
