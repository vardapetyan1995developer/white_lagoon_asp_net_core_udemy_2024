﻿using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();

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
                _unitOfWork.Villa.Add(obj);

                _unitOfWork.Save();

                TempData["success"] = "The villa has been created successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(villa => villa.Id == villaId);
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
                _unitOfWork.Villa.Update(obj);

                _unitOfWork.Save();

                TempData["success"] = "The villa has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _unitOfWork.Villa.Get(villa => villa.Id == villaId);

            if (obj is null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _unitOfWork.Villa.Get(villa => villa.Id == obj.Id);

            if (objFromDb is not null)
            {
                _unitOfWork.Villa.Remove(objFromDb);

                _unitOfWork.Save();

                TempData["success"] = "The villa deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The villa could not be deleted.";

            return View();
        }
    }
}
