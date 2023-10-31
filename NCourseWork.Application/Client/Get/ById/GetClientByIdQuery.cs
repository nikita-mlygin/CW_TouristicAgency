using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Client.Get.ById
{
    public class GetClientByIdQuery : IRequest<ClientFullInfo?>
    {
        public GetClientByIdQuery()
        { 
        }

        public GetClientByIdQuery(Guid clientId)
        {
            ClientId = clientId;
        }

        public Guid ClientId { get; set; }
    }
}
