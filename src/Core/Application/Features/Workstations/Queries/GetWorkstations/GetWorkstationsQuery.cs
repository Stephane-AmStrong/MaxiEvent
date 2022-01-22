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

namespace Application.Features.Workstations.Queries.GetWorkstations
{
    public class GetWorkstationsQuery : QueryStringParameters, IRequest<PagedList<GetWorkstationsViewModel>>
    {
        public GetWorkstationsQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllWorkstationsQueryHandler : IRequestHandler<GetWorkstationsQuery, PagedList<GetWorkstationsViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllWorkstationsQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetWorkstationsViewModel>> Handle(GetWorkstationsQuery query, CancellationToken cancellationToken)
        {
            var workstations = await _repository.Workstation.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetWorkstationsViewModel>>(workstations);
        }
    }
}
