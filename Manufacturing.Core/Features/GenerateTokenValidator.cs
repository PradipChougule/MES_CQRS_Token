namespace Manufacturing.Core.Features
{
    using FluentValidation;
    public class GenerateTokenValidator : AbstractValidator<GenerateTokenQuery>
    {
        public GenerateTokenValidator()
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .Length(1, 50).WithMessage("User Name is required");

            RuleFor(x => x.Password)
           .NotEmpty()
           .Length(1, 50).WithMessage("Password is required");
        }
    }
}
