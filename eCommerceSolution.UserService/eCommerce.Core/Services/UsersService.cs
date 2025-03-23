using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services
{
    internal class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
                _usersRepository = usersRepository;
        }
        public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
        {
          ApplicationUser? user =  await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
            if (user == null)
            {
                return null;
            }
            return new AuthenticationResponse(user.UserId, user.Email, user.PersonName, user.Gender, "token", Success: true);
        }

        public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
        {
            //create a new applicationuser object from registerrequest                                                                                                      
            ApplicationUser? user = new ApplicationUser()
            {
                PersonName = registerRequest.PersonName,
                Email = registerRequest.Email,
                Password = registerRequest.Password,
                Gender = registerRequest.Gender.ToString(),
            };
            ApplicationUser? registeredUser = await _usersRepository.AddUser(user);
            if (user == null)
            {
                return null;
            }
            //REturn success response
            return new AuthenticationResponse(
                registeredUser.UserId,
                registeredUser.Email,
                registeredUser.PersonName, 
                registeredUser.Gender, "token", Success: true);
        }
    }
}
