using eCommerce.Core.Entities;

namespace eCommerce.Core.RespositoryContracts;


/// <summary>
/// Contracts to be implemented by UserRepository that contains data access logic of Users data store
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Method to add a user to the data store and return the added user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<ApplicationUser?> AddUser(ApplicationUser user);

    /// <summary>
    /// Method to retrieve existing user by email and password    
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<ApplicationUser?> GetUserByEmailAndPassword(string? email, string password);

    Task<ApplicationUser?> GetUserByUserID(Guid userID);
}
