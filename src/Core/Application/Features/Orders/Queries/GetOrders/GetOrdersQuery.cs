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

namespace Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : QueryStringParameters, IRequest<PagedList<GetOrdersViewModel>>
    {
        public GetOrdersQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllOrdersQueryHandler : IRequestHandler<GetOrdersQuery, PagedList<GetOrdersViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetOrdersViewModel>> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await _repository.Order.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetOrdersViewModel>>(orders);
        }
    }
}
