﻿using eCommerce.Core.DTO;

namespace eCommerce.Core.ServiceContracts
{
    /// <summary>
    /// Contract for users service that contains use cases for users
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Method to handle user login use case and return an AuthenticationResponse object that contains status of login
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
       Task<AuthenticationResponse?> Login(LoginRequest loginRequest);

        /// <summary>
        ///  Method to handle user register use case and return an AuthenticationResponse object that contains status of registration
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        Task<AuthenticationResponse?> Register(RegisterRequest registerRequest);

        /// <summary>
        /// Returns UserDTO based on the given UserID
        /// </summary>
        /// <param name="userId">User ID to search</param>
        /// <returns>UserDTO object based on the matching UserID</returns>
        Task<UserDTO> GetUserByUserId(Guid? userId);
    }
}
