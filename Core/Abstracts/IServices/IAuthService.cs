using Core.Concretes.DTOs;
using Utilities.Responses;

namespace Core.Abstracts.IServices
{
    public interface IAuthService
    {
        Task<IResult> LoginAsync(LoginDTO model);
        Task<IResult> RegisterAsync(RegisterDTO model, bool isAdmin = false);
        Task<IResult> LogoutAsync();
        Task<IResult> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword);
        Task<IResult> ResetPasswordAsync(string newPassword, string confirmPassword, string auth_token);
        Task<IResult> ForgotPasswordAsync(string email);
    }
}
