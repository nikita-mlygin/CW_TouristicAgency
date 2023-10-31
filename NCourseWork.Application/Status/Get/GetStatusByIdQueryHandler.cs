using MapsterMapper;
using MediatR;
using NCourseWork.Domain.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Application.Status.Get
{
    internal class GetStatusByIdQueryHandler : IRequestHandler<GetStatusByIdQuery, StatusFullInfo?>
    {
        private readonly IStatusRepository statusRepository;
        private readonly IMapper mapper;

        public GetStatusByIdQueryHandler(IStatusRepository statusRepository, IMapper mapper)
        {
            this.statusRepository = statusRepository;
            this.mapper = mapper;
        }

        public async Task<StatusFullInfo?> Handle(GetStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await statusRepository.GetStatusByIdAsync(request.Id);

            return res is null ? null : mapper.Map<StatusFullInfo>(res);
        }
    }
}
