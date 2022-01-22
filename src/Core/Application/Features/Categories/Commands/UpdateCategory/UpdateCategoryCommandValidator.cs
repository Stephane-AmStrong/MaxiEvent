using Application.Interfaces;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        private readonly IRepositoryWrapper repository;

        public UpdateCategoryCommandValidator(IRepositoryWrapper repository)
        {
            this.repository = repository;

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                /*.MustAsync(IsUniqueBarcode).WithMessage("{PropertyName} already exists.")*/;

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(10).WithMessage("{PropertyName} must be at least 10 characters.");

        }

        //private async Task<bool> IsUniqueBarcode(Category product, CancellationToken cancellationToken)
        //{
        //    return await _repository.Category.CategoryExistAsync(product);
        //}
    }
}
