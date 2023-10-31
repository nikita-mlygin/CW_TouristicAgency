namespace NCourseWork.Application.Status.Add
{
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.AccessControl;
    using System.Text;
    using System.Threading.Tasks;


    internal class AddStatusCommandHandler : IRequestHandler<AddStatusCommand, Guid>
    {
        private readonly IStatusRepository statusRepository;
        private readonly IMapper mapper;

        public AddStatusCommandHandler(IStatusRepository statusRepository, IMapper mapper)
        {
            this.statusRepository = statusRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(AddStatusCommand request, CancellationToken cancellationToken)
        {
            var newStatusInstance = mapper.Map<Status>(request);

            newStatusInstance.Id = Guid.NewGuid();

            await statusRepository.AddStatusAsync(newStatusInstance);

            return newStatusInstance.Id;
        }
    }
}
