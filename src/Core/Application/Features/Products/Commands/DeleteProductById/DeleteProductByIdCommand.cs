using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.DeleteProductById
{
    public class DeleteProductByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeleteProductByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await _repository.Product.GetProductByIdAsync(command.Id);
                if (product == null) throw new ApiException($"Product Not Found.");
                await _repository.Product.DeleteProductAsync(product);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
