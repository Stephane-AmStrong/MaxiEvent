using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Events.Commands.DeleteEventById
{
    public class DeleteEventByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteEventByIdCommandHandler : IRequestHandler<DeleteEventByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeleteEventByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteEventByIdCommand command, CancellationToken cancellationToken)
            {
                var _event = await _repository.Event.GetEventByIdAsync(command.Id);
                if (_event == null) throw new ApiException($"Event Not Found.");
                await _repository.Event.DeleteEventAsync(_event);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
