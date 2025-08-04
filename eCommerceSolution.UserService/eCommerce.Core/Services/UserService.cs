using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RespositoryContracts;
using eCommerce.Core.ServiceContracts;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    public readonly IUserRepository _userRepository;
    public readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<GetUserRequest?> GetByUserID(Guid userID)
    {
        if (userID == Guid.Empty)
            return null;

        ApplicationUser? user = await _userRepository.GetUserByUserID(userID);

        if (user == null)
            return null;

        return _mapper.Map<GetUserRequest?>(user);
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password!);

        if (user == null)
            return null;


        // Auto Mapper is used to Map values from LoginRequest to AuthenticationResponse Type
        //return new AuthenticationResponse(user.UserID, user.Email, user.PersonName, user.Gender, "Token", Success: true);
        return _mapper.Map<AuthenticationResponse>(user) with { Success = true, Token = "Token" };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {

        //Create a new ApplicationUserobject from the RegisterRequest
        //ApplicationUser user = new()
        //{
        //    PersonName = registerRequest.PersonName,
        //    Email = registerRequest.Email,
        //    Password = registerRequest.Password,
        //    Gender = registerRequest.Gender.ToString()
        //};

        ApplicationUser user  = _mapper.Map<ApplicationUser>(registerRequest);

        ApplicationUser? registeredUser = await _userRepository.AddUser(user);

        if (registeredUser == null) 
            return null;


        //// Auto Mapper is used to Map values from LoginRequest to AuthenticationResponse Type
        //return new AuthenticationResponse(registeredUser.UserID, registeredUser.Email, registeredUser.PersonName, registeredUser.Gender, "token", Success: true);
        return _mapper.Map<AuthenticationResponse>(registeredUser) with { Success = true, Token = "Token" };
    }
}
