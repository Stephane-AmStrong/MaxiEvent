using Application.Interfaces;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Places.Queries.GetPlaces
{
    public class GetPlacesQuery : QueryStringParameters, IRequest<PagedList<GetPlacesViewModel>>
    {
        public GetPlacesQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllPlacesQueryHandler : IRequestHandler<GetPlacesQuery, PagedList<GetPlacesViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllPlacesQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetPlacesViewModel>> Handle(GetPlacesQuery query, CancellationToken cancellationToken)
        {
            var places = await _repository.Place.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetPlacesViewModel>>(places);
        }
    }
}
