namespace Manufacturing.Core.Features
{
    using MediatR;

    public class GenerateTokenQuery : IRequest<GetUserDetailsResponse>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
