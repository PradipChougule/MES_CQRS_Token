namespace Manufacturing.Core.Features
{
    using Manufacturing.Core.Repositories;
    using MediatR;

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, List<GetUserDetailsResponse>>
    {
        private readonly IUserRepository userRepository;

        public GetUserDetailsQueryHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<List<GetUserDetailsResponse>> Handle(GetUserDetailsQuery query, CancellationToken cancellationToken)
        {
            return await userRepository.GetAllUsers();
        }
    }
}
