using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrueStory.Domain.Dtos;

namespace TrueStory.Domain.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(d => d.Name)
            .NotEmpty().WithMessage("Name is required.");

        RuleFor(d => d.Data)
            .NotEmpty().WithMessage("Other Informations are required.");

    }
}


public class GetProductValidator : AbstractValidator<GetProductByIdDto>
{
    public GetProductValidator()
    {
        RuleFor(d => d.id)
            .NotEmpty().WithMessage("Id is required.");

    }
}
