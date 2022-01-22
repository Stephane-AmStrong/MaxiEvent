using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Events.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<GetEventViewModel>
    {
        public Guid Id { get; set; }

        public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, GetEventViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetEventByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetEventViewModel> Handle(GetEventByIdQuery query, CancellationToken cancellationToken)
            {
                var _event = await _repository.Event.GetByIdAsync(query.Id);
                if (_event == null) throw new ApiException($"Event Not Found.");
                return _mapper.Map<GetEventViewModel>(_event);
            }
        }
    }
}
