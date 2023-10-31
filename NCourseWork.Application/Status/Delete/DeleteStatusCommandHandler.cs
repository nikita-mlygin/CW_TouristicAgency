using MediatR;
using NCourseWork.Domain.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Delete
{
    internal class DeleteStatusCommandHandler : IRequestHandler<DeleteStatusCommand>
    {
        private readonly IStatusRepository statusRepository;

        public DeleteStatusCommandHandler(IStatusRepository statusRepository)
        {
            this.statusRepository = statusRepository;
        }

        public async Task Handle(DeleteStatusCommand request, CancellationToken cancellationToken)
        {
            await statusRepository.RemoveStatusAsync(request.Id);
        }
    }
}
