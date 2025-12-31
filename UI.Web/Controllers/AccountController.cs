using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService service;

        public AccountController(IAuthService service)
        {
            this.service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.LoginAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("index", "home");
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model, bool isAdmin = false)
        {
            // ModelState: Gönderilen bilgileri tutarlığı, ve ilgili mesajların tamamı bu yapıda tutulur.
            // IsValid ile form bilgilerinin ilgili kurallara tamamen uyduğunu teyit ederiz.
            if (ModelState.IsValid)
            {
                var result = await service.RegisterAsync(model, isAdmin);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Messages)
                    {
                        ModelState.AddModelError(string.Empty, error);
                    }
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var result = await service.LogoutAsync();
            if (result.Success)
            {
                return RedirectToAction("login", "account");
            }
            else
            {
                return RedirectToAction("index", "home");
            }
        }
    }
}
