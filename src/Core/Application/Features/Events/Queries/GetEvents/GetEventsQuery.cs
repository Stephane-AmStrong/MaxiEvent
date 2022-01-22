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

namespace Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQuery : QueryStringParameters, IRequest<PagedList<GetEventsViewModel>>
    {
        public GetEventsQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllEventsQueryHandler : IRequestHandler<GetEventsQuery, PagedList<GetEventsViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllEventsQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetEventsViewModel>> Handle(GetEventsQuery query, CancellationToken cancellationToken)
        {
            var events = await _repository.Event.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetEventsViewModel>>(events);
        }
    }
}
