using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts;

/// <summary>
/// contracts for users service that contains use cases for users
/// </summary>
/// 
public interface IUserService
{
    /// <summary>
    /// Method to handle user login use case and returns an AuthenticationResponse object that contains the status of the login
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

    /// <summary>
    /// Method to handle user registration use case and returns AuthenticationResponse type that represents the status of the user registration
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);

    Task<GetUserRequest?> GetByUserID(Guid userID);
}
