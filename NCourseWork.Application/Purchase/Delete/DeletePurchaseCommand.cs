using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Purchase.Delete
{
    public class DeletePurchaseCommand : IRequest
    {
        public DeletePurchaseCommand()
        {
        }

        public DeletePurchaseCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
