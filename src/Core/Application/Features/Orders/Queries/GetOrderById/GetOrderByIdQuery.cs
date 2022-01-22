using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<GetOrderViewModel>
    {
        public Guid Id { get; set; }

        public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, GetOrderViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public GetOrderByIdQueryHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetOrderViewModel> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
            {
                var order = await _repository.Order.GetByIdAsync(query.Id);
                if (order == null) throw new ApiException($"Order Not Found.");
                return _mapper.Map<GetOrderViewModel>(order);
            }
        }
    }
}
