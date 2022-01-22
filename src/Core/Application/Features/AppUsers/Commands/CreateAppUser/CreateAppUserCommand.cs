using Application.Features.AppUsers.Queries.GetAppUserById;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.AppUsers.Commands.CreateAppUser
{
    public partial class CreateAppUserCommand : IRequest<GetAppUserViewModel>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Price { get; set; }
        public string StatusAppUser { get; set; }
        public int NoPriority { get; set; }

        public Guid CategoryId { get; set; }

        public Guid SponsorId { get; set; }
    }

    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, GetAppUserViewModel>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;


        public CreateAppUserCommandHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<GetAppUserViewModel> Handle(CreateAppUserCommand command, CancellationToken cancellationToken)
        {
            var appUserEntity = _mapper.Map<AppUser>(command);

            await _repository.AccountService.RegisterAsync(appUserEntity);
            await _repository.SaveAsync();

            return _mapper.Map<GetAppUserViewModel>(appUserEntity);
        }
    }
}
