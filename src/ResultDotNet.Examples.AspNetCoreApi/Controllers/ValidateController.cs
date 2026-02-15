using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static ResultDotNet.Examples.AspNetCoreApi.Controllers.ValidateController;

namespace ResultDotNet.Examples.AspNetCoreApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ValidateController
    : ControllerBase
{
    [HttpPost]
    public IActionResult Post(
        ValidateInputDto input,
        IValidator<ValidateInputDto> validator)
    {
        return ValidateInput(input)
            .ToActionResult();

        Result<ProblemDetails> ValidateInput(ValidateInputDto input) 
            => validator.Validate(input).ToResult();
    }

    public record ValidateInputDto(
        string Forename,
        string Surname,
        string Email,
        DateTimeOffset DateOfBirth);

    public class ValidateInputDtoValidator
        : AbstractValidator<ValidateInputDto>
    {
        public ValidateInputDtoValidator()
        {
            RuleFor(x => x.Forename).NotEmpty().WithMessage("Forename is required.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.DateOfBirth).LessThan(DateTimeOffset.Now).WithMessage("Date of birth must be in the past.");
        }
    }
}