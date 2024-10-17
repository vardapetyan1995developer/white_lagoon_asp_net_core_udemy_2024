using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaRepository _villaRepository;

        public VillaController(IVillaRepository villaRepository)
        {
            _villaRepository = villaRepository;
        }

        public IActionResult Index()
        {
            var villas = _villaRepository.GetAll();

            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if(obj.Name == obj.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the name.");
            }

            if (ModelState.IsValid)
            {
                _villaRepository.Add(obj);

                _villaRepository.Save();

                TempData["success"] = "The villa has been created successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _villaRepository.Get(villa => villa.Id == villaId);
            //Villa? obj = _db.Villas.Find(villaId);
            //var villaList = _db.Villas.Where(villa => villa.Price > 50 && villa.Occupancy > 0);

            if (obj is null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _villaRepository.Update(obj);

                _villaRepository.Save();

                TempData["success"] = "The villa has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _villaRepository.Get(villa => villa.Id == villaId);

            if (obj is null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _villaRepository.Get(villa => villa.Id == obj.Id);

            if (objFromDb is not null)
            {
                _villaRepository.Remove(objFromDb);

                _villaRepository.Save();

                TempData["success"] = "The villa deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The villa could not be deleted.";

            return View();
        }
    }
}
