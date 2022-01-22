using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Places.Queries.GetPlaceById
{
    public class GetPlaceByIdQuery : IRequest<GetPlaceViewModel>
    {
        public Guid Id { get; set; }

        public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, GetPlaceViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetPlaceByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPlaceViewModel> Handle(GetPlaceByIdQuery query, CancellationToken cancellationToken)
            {
                var place = await _repository.Place.GetByIdAsync(query.Id);
                if (place == null) throw new ApiException($"Place Not Found.");
                return _mapper.Map<GetPlaceViewModel>(place);
            }
        }
    }
}
