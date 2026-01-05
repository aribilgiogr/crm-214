using Core.Abstracts.IServices;
using Core.Concretes.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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


        public async Task<IActionResult> Convert(int id)
        {
            var lead = await service.GetDetailAsync(id);

            if (lead == null) return NotFound();

            var model = new CustomerCreateDTO
            {
                Name = lead.Name,
                LeadId = id
            };

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Convert(CustomerCreateDTO model)
        {
            model.AssignedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ModelState.Remove("AssignedUserId");
            if (ModelState.IsValid)
            {
                var result = await service.ConvertToCustomer(model);
                if (result.Success)
                {
                    return RedirectToAction("index", "customers");
                }

                foreach (var message in result.Messages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            return View(model);
        }
    }
}
