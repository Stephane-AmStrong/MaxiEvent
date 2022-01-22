using Application.Features.Places.Queries.GetPlaceById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Places.Commands.CreatePlace
{
    public partial class CreatePlaceCommand : IRequest<GetPlaceViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPlace { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreatePlaceCommandHandler : IRequestHandler<CreatePlaceCommand, GetPlaceViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreatePlaceCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetPlaceViewModel> Handle(CreatePlaceCommand command, CancellationToken cancellationToken)
        {
            var placeEntity = _mapper.Map<Place>(command);

            await _repository.Place.CreateAsync(placeEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetPlaceViewModel>(placeEntity);
        }
    }
}
