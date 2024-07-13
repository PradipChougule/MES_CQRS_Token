namespace Manufacturing.Core.Repositories
{
    using Manufacturing.Core.Domain;
    using Manufacturing.Core.Features;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public interface IUserRepository
    {
        Task<List<GetUserDetailsResponse>> GetAllUsers();

        Task<UserManagement> GetValidUser(string userName, string password);

        Task<GetUserDetailsResponse> GetUser(string userName, string password);
    }

    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<GetUserDetailsResponse>> GetAllUsers()
        {
            return await dbContext.UserManagement.Select(x => new GetUserDetailsResponse
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserPin = x.UserPin,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                District = x.District,
                Taluka = x.Taluka,
                PinCode = x.PinCode,
                Address = x.Address,
                LandMark = x.LandMark,
                AdharCardNumber = x.AdharCardNumber,
                PanCardNumber = x.PanCardNumber,
            }).ToListAsync();
        }


        public async Task<GetUserDetailsResponse> GetUser(string userName, string password)
        {

            var userData = await dbContext.UserManagement.FirstOrDefaultAsync(x => x.UserPin.Equals(userName) && x.Password.Equals(password));
            if (userData != null)
            {
                var roles = GetUserRoles(userData.Id);

                return new GetUserDetailsResponse
                {
                    Id = userData.Id,
                    FirstName = userData.FirstName,
                    LastName = userData.LastName,
                    Email = userData.Email,
                    UserPin = userData.UserPin,
                    Roles = roles ?? new List<string>()
                };
            }
            return new GetUserDetailsResponse();
        }

        public async Task<UserManagement> GetValidUser(string userName, string password)
        {
            return await dbContext.UserManagement.SingleAsync(x => x.UserPin.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
        }

        public List<string> GetUserRoles(int userId)
        {
            return dbContext.UserRolesXref.Where(x => x.UsertId == userId).Select(y => y.Role.Name).ToList();
        }
    }
}
