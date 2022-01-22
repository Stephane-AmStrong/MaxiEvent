using Application.Exceptions;
using Application.Features.Orders.Queries.GetOrderById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<GetOrderViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusOrder { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, GetOrderViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdateOrderCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetOrderViewModel> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
            {
                var orderEntity = await _repository.Order.GetByIdAsync(command.Id);

                if (orderEntity == null)
                {
                    throw new ApiException($"Order Not Found.");
                }

                _mapper.Map(command, orderEntity);

                await _repository.Order.UpdateAsync(orderEntity);
                await _repository.SaveAsync();

                var orderReadDto = _mapper.Map<GetOrderViewModel>(orderEntity);

                //if (!string.IsNullOrWhiteSpace(orderReadDto.ImgLink)) orderReadDto.ImgLink = $"{_baseURL}{orderReadDto.ImgLink}";

                return orderReadDto;


            }
        }
    }
}
