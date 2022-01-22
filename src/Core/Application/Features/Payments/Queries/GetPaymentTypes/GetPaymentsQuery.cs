using Application.Interfaces;
using Application.Parameters;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Payments.Queries.GetPayments
{
    public class GetPaymentsQuery : QueryStringParameters, IRequest<PagedList<GetPaymentsViewModel>>
    {
        public GetPaymentsQuery()
        {
            OrderBy = "name";
        }

        public string WithTheName { get; set; }
    }

    public class GetAllPaymentsQueryHandler : IRequestHandler<GetPaymentsQuery, PagedList<GetPaymentsViewModel>>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public GetAllPaymentsQueryHandler(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<PagedList<GetPaymentsViewModel>> Handle(GetPaymentsQuery query, CancellationToken cancellationToken)
        {
            var payments = await _repository.Payment.GetPagedListAsync(query);
            return  _mapper.Map<PagedList<GetPaymentsViewModel>>(payments);
        }
    }
}
