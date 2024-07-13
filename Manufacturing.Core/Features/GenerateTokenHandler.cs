namespace Manufacturing.Core.Features
{
    using Manufacturing.Core.Repositories;
    using MediatR;

    public class GenerateTokenHandler : IRequestHandler<GenerateTokenQuery, GetUserDetailsResponse>
    {
        private readonly IUserRepository userRepository;

        public GenerateTokenHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<GetUserDetailsResponse> Handle(GenerateTokenQuery query, CancellationToken cancellationToken)
        {
            return await userRepository.GetUser(query.UserName, query.Password);
        }
    }
}
