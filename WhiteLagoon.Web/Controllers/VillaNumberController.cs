﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();

            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString(),
            });

            ViewBag.VillaList = list;

            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            //ModelState.Remove("Villa");

            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(obj);

                _db.SaveChanges();

                TempData["success"] = "The villa number has been created successfully.";

                return RedirectToAction("Index", "VillaNumber");
            }

            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(villa => villa.Id == villaId);
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
                _db.Villas.Update(obj);

                _db.SaveChanges();

                TempData["success"] = "The villa has been updated successfully.";

                return RedirectToAction("Index", "Villa");
            }

            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(villa => villa.Id == villaId);

            if (obj is null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(villa => villa.Id == obj.Id);

            if (objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);

                _db.SaveChanges();

                TempData["success"] = "The villa deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["error"] = "The villa could not be deleted.";

            return View();
        }
    }
}
