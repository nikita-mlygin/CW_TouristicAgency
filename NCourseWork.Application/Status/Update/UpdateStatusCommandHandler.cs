
namespace NCourseWork.Application.Status.Update
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    internal class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand>
    {
        private readonly IMapper mapper;
        private readonly IStatusRepository statusRepository;

        public UpdateStatusCommandHandler(IMapper mapper, IStatusRepository statusRepository)
        {
            this.mapper = mapper;
            this.statusRepository = statusRepository;
        }

        public async Task Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatus = mapper.Map<Status>(request);

            await statusRepository.UpdateStatusAsync(newStatus);
        }
    }
}
