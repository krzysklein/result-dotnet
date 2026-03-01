using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ResultDotNet.Examples.AspNetCoreApi.Controllers;

/// <summary>
/// This controller demonstrates how to use FluentValidation to validate input data in an ASP.NET Core API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ValidateController
    : ControllerBase
{
    [HttpPost]
    public IActionResult Post(
        ValidateInputDto input,
        IValidator<ValidateInputDto> validator) 
        => ValidateInput(input, validator)
            .ToActionResult();

    private static Result<ProblemDetails> ValidateInput(ValidateInputDto input, IValidator<ValidateInputDto> validator) 
        => validator.Validate(input).ToResult();

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