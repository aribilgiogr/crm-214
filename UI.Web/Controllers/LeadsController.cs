using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UI.Web.Controllers
{
    public class LeadsController : Controller
    {
        private readonly ILeadService service;

        public LeadsController(ILeadService service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await service.GetAllAsync(User));
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeadCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var result = await service.CreateAsync(model);
                if (result.Success)
                {
                    return RedirectToAction("index", "leads");
                }
                else
                {
                    foreach (var message in result.Messages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var result = await service.ImportFromFileAsync(file);
                if (result.Success)
                {
                    return RedirectToAction("index", "leads");
                }
                return Problem(string.Join(", ", result.Messages));
            }
            return Problem("File is empty!");
        }
    }
}
