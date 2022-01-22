using Application.Features.Events.Queries.GetEventById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Events.Commands.CreateEvent
{
    public partial class CreateEventCommand : IRequest<GetEventViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusEvent { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, GetEventViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateEventCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetEventViewModel> Handle(CreateEventCommand command, CancellationToken cancellationToken)
        {
            var _eventEntity = _mapper.Map<Event>(command);

            await _repository.Event.CreateEventAsync(_eventEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetEventViewModel>(_eventEntity);
        }
    }
}
