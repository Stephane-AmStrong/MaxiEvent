using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.DeleteOrderById
{
    public class DeleteOrderByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeleteOrderByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
            {
                var order = await _repository.Order.GetByIdAsync(command.Id);
                if (order == null) throw new ApiException($"Order Not Found.");
                await _repository.Order.DeleteAsync(order);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
