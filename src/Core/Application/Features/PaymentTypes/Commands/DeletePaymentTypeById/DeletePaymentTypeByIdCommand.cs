using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PaymentTypes.Commands.DeletePaymentTypeById
{
    public class DeletePaymentTypeByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeletePaymentTypeByIdCommandHandler : IRequestHandler<DeletePaymentTypeByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeletePaymentTypeByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeletePaymentTypeByIdCommand command, CancellationToken cancellationToken)
            {
                var paymentType = await _repository.PaymentType.GetByIdAsync(command.Id);
                if (paymentType == null) throw new ApiException($"PaymentType Not Found.");
                await _repository.PaymentType.DeleteAsync(paymentType);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
