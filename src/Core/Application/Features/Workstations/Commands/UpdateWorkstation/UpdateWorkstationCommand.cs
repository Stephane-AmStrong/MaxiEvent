using Application.Exceptions;
using Application.Features.Workstations.Queries.GetWorkstationById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Workstations.Commands.UpdateWorkstation
{
    public class UpdateWorkstationCommand : IRequest<GetWorkstationViewModel>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusWorkstation { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public String AppUserId { get; set; }

        public Guid SponsorId { get; set; }


        public class UpdateWorkstationCommandHandler : IRequestHandler<UpdateWorkstationCommand, GetWorkstationViewModel>
        {
            private readonly IRepositoryWrapper _repository;
            private readonly IMapper _mapper;

            public UpdateWorkstationCommandHandler(IRepositoryWrapper repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<GetWorkstationViewModel> Handle(UpdateWorkstationCommand command, CancellationToken cancellationToken)
            {
                var workstationEntity = await _repository.Workstation.GetByIdAsync(command.Id);

                if (workstationEntity == null)
                {
                    throw new ApiException($"Workstation Not Found.");
                }

                _mapper.Map(command, workstationEntity);

                await _repository.Workstation.UpdateAsync(workstationEntity);
                await _repository.SaveAsync();

                var workstationReadDto = _mapper.Map<GetWorkstationViewModel>(workstationEntity);

                //if (!string.IsNullOrWhiteSpace(workstationReadDto.ImgLink)) workstationReadDto.ImgLink = $"{_baseURL}{workstationReadDto.ImgLink}";

                return workstationReadDto;


            }
        }
    }
}
