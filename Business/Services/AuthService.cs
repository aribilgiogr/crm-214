using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using Utilities.Responses;

namespace Business.Services
{
    public class AuthService : IAuthService
    {
        // Dependency Inversion: Bağımlılıkların geri döndürülmesi. Bu yapı sayesinde uygulamamız başlatılırken yapıcı metotların parametrelerinin tembel yükleme olarak çalışmasını sağlar. new operatörünü tek bir noktadan yönetiriz.
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationUserRole> roleManager;
        private readonly SignInManager<ApplicationUser> signinManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationUserRole> roleManager, SignInManager<ApplicationUser> signinManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
        }

        public Task<IResult> ChangePasswordAsync(string oldPassword, string newPassword, string confirmPassword)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> ForgotPasswordAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> LoginAsync(LoginDTO model)
        {
            try
            {
                // lockoutOnFailure: Hatalı girişte hesabı kitleyebilirsiniz.
                var result = await signinManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return new SuccessResult();
                }
                else if (result.IsLockedOut)
                {
                    return new ErrorResult(["Hesabınız kilitli, yöneticinizle irtibata geçin!"]);
                }
                else if (result.IsNotAllowed)
                {
                    return new ErrorResult(["Hesabınız henüz doğrulanmamış!"]);
                }
                else
                {
                    // Genel başarısız giriş.
                    return new ErrorResult(["Geçersiz giriş denemesi!"]);
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult([ex.Message]);
            }
        }

        public async Task<IResult> LogoutAsync()
        {
            try
            {
                await signinManager.SignOutAsync();
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                // [ex.Message] -> string collection.
                return new ErrorResult([ex.Message]);
            }
        }

        public async Task<IResult> RegisterAsync(RegisterDTO model, bool isAdmin = false)
        {
            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Aşağıdaki tanımlanmış rollerin veri tabanında daha önceden eklenmiş olması gereklidir.
                    if (isAdmin)
                    {
                        if (!roleManager.Roles.Any(x => x.Name == "Admin"))
                        {
                            await roleManager.CreateAsync(new ApplicationUserRole { Name = "Admin" });
                        }

                        await userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        if (!roleManager.Roles.Any(x => x.Name == "SalesPerson"))
                        {
                            await roleManager.CreateAsync(new ApplicationUserRole { Name = "SalesPerson" });
                        }

                        await userManager.AddToRoleAsync(user, "SalesPerson");
                    }
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult(result.Errors.Select(e => e.Description));
                }
            }
            catch (Exception ex)
            {
                return new ErrorResult([ex.Message]);
            }
        }

        public Task<IResult> ResetPasswordAsync(string newPassword, string confirmPassword, string auth_token)
        {
            throw new NotImplementedException();
        }
    }
}
