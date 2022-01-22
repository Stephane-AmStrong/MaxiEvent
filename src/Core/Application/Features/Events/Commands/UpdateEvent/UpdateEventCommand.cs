using Application.Exceptions;
using Application.Features.Events.Queries.GetEventById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest<GetEventViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusEvent { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, GetEventViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdateEventCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetEventViewModel> Handle(UpdateEventCommand command, CancellationToken cancellationToken)
            {
                var _eventEntity = await _repository.Event.GetEventByIdAsync(command.Id);

                if (_eventEntity == null)
                {
                    throw new ApiException($"Event Not Found.");
                }

                _mapper.Map(command, _eventEntity);

                await _repository.Event.UpdateEventAsync(_eventEntity);
                await _repository.SaveAsync();

                var _eventReadDto = _mapper.Map<GetEventViewModel>(_eventEntity);

                //if (!string.IsNullOrWhiteSpace(_eventReadDto.ImgLink)) _eventReadDto.ImgLink = $"{_baseURL}{_eventReadDto.ImgLink}";

                return _eventReadDto;


            }
        }
    }
}
