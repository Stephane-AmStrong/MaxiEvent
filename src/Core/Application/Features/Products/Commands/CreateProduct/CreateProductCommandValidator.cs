using Application.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        private readonly IRepositoryWrapper repository;

        public CreateProductCommandValidator(IRepositoryWrapper repository)
        {
            this.repository = repository;

            RuleFor(p => p.Barcode)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                /*.MustAsync(IsUniqueBarcode).WithMessage("{PropertyName} already exists.")*/;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        }

        //private async Task<bool> IsUniqueBarcode(Product product, CancellationToken cancellationToken)
        //{
        //    return await repository.Product.ProductExistAsync(product);
        //}
    }
}
