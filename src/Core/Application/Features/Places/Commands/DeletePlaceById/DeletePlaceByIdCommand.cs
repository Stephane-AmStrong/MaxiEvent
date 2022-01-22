using Application.Exceptions;
using Application.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Places.Commands.DeletePlaceById
{
    public class DeletePlaceByIdCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeletePlaceByIdCommandHandler : IRequestHandler<DeletePlaceByIdCommand>
        {
            private readonly IRepositoryWrapper _repository;

            public DeletePlaceByIdCommandHandler(IRepositoryWrapper repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeletePlaceByIdCommand command, CancellationToken cancellationToken)
            {
                var place = await _repository.Place.GetByIdAsync(command.Id);
                if (place == null) throw new ApiException($"Place Not Found.");
                await _repository.Place.DeleteAsync(place);
                await _repository.SaveAsync();

                return Unit.Value;
            }
        }
    }
}
