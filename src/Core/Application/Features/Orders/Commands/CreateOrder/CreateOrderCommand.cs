using Application.Features.Orders.Queries.GetOrderById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public partial class CreateOrderCommand : IRequest<GetOrderViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusOrder { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, GetOrderViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateOrderCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetOrderViewModel> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(command);

            await _repository.Order.CreateAsync(orderEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetOrderViewModel>(orderEntity);
        }
    }
}
