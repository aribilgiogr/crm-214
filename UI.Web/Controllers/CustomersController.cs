using Core.Abstracts.IServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService service;

        public CustomersController(ICustomerService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync(User));
        }
    }
}
