using Application.Features.Workstations.Queries.GetWorkstationById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Workstations.Commands.CreateWorkstation
{
    public partial class CreateWorkstationCommand : IRequest<GetWorkstationViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusWorkstation { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreateWorkstationCommandHandler : IRequestHandler<CreateWorkstationCommand, GetWorkstationViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateWorkstationCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetWorkstationViewModel> Handle(CreateWorkstationCommand command, CancellationToken cancellationToken)
        {
            var workstationEntity = _mapper.Map<Workstation>(command);

            await _repository.Workstation.CreateAsync(workstationEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetWorkstationViewModel>(workstationEntity);
        }
    }
}
