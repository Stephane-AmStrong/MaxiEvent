﻿using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Payments.Commands.UpdatePayment
{
    public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public UpdatePaymentCommandValidator(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Date)
                .Must(BeAValidDate).WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(0).WithMessage("{PropertyName} must not be less than or equal to 0.");

            RuleFor(p => p.CategoryId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Must(BeAValidGuid).WithMessage("{PropertyName} is required.");

            RuleFor(p => p)
                .MustAsync(IsUnique).WithMessage("{PropertyName} already exists.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime)) && date < DateTime.Now;
        }

        private bool BeAValidGuid(Guid id)
        {
            return !id.Equals(new Guid());
        }

        private async Task<bool> IsUnique(UpdatePaymentCommand paymentCommand, CancellationToken cancellationToken)
        {
            var payment = _mapper.Map<Payment>(paymentCommand);
            return await _repository.Payment.ExistAsync(payment);
        }
    }
}
