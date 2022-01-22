using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Workstations.Commands.DeleteWorkstationById
{
    public class DeleteWorkstationByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteWorkstationByIdCommandHandler : IRequestHandler<DeleteWorkstationByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeleteWorkstationByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteWorkstationByIdCommand command, CancellationToken cancellationToken)
            {
                var workstation = await _repository.Workstation.GetByIdAsync(command.Id);
                if (workstation == null) throw new ApiException($"Workstation Not Found.");
                await _repository.Workstation.DeleteAsync(workstation);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
