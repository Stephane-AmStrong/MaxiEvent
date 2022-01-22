using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Workstations.Queries.GetWorkstationById
{
    public class GetWorkstationByIdQuery : IRequest<GetWorkstationViewModel>
    {
        public Guid Id { get; set; }

        public class GetWorkstationByIdQueryHandler : IRequestHandler<GetWorkstationByIdQuery, GetWorkstationViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetWorkstationByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetWorkstationViewModel> Handle(GetWorkstationByIdQuery query, CancellationToken cancellationToken)
            {
                var workstation = await _repository.Workstation.GetByIdAsync(query.Id);
                if (workstation == null) throw new ApiException($"Workstation Not Found.");
                return _mapper.Map<GetWorkstationViewModel>(workstation);
            }
        }
    }
}
