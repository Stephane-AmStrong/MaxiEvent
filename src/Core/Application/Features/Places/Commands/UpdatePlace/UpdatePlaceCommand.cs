using Application.Exceptions;
using Application.Features.Places.Queries.GetPlaceById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Places.Commands.UpdatePlace
{
    public class UpdatePlaceCommand : IRequest<GetPlaceViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusPlace { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, GetPlaceViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdatePlaceCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetPlaceViewModel> Handle(UpdatePlaceCommand command, CancellationToken cancellationToken)
            {
                var placeEntity = await _repository.Place.GetPlaceByIdAsync(command.Id);

                if (placeEntity == null)
                {
                    throw new ApiException($"Place Not Found.");
                }

                _mapper.Map(command, placeEntity);

                await _repository.Place.UpdatePlaceAsync(placeEntity);
                await _repository.SaveAsync();

                var placeReadDto = _mapper.Map<GetPlaceViewModel>(placeEntity);

                //if (!string.IsNullOrWhiteSpace(placeReadDto.ImgLink)) placeReadDto.ImgLink = $"{_baseURL}{placeReadDto.ImgLink}";

                return placeReadDto;


            }
        }
    }
}
