﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Web.ViewModels;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties: "Villa");

            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                })
            };

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            //ModelState.Remove("Villa");

            bool roomNumberExists = _unitOfWork.VillaNumber.Any(villaNumber => villaNumber.Villa_Number == obj.VillaNumber.Villa_Number);
             
            if (ModelState.IsValid && !roomNumberExists)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);

                _unitOfWork.Save();

                TempData["success"] = "The villa number has been created successfully.";

                return RedirectToAction(nameof(Index));
            }

            if (roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),

                VillaNumber = _unitOfWork.VillaNumber.Get(villaNum => villaNum.Villa_Number == villaNumberId),
            };

            if (villaNumberVM.VillaNumber is null)
                return RedirectToAction("Error", "Home");

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);

                _unitOfWork.Save();

                TempData["success"] = "The villa number has been updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(item => new SelectListItem
            {
                Text = item.Name,
                Value = item.Id.ToString()
            });

            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(item => new SelectListItem
                {
                    Text = item.Name,
                    Value = item.Id.ToString()
                }),

                VillaNumber = _unitOfWork.VillaNumber.Get(villaNum => villaNum.Villa_Number == villaNumberId),
            };

            if (villaNumberVM.VillaNumber is null)
                return RedirectToAction("Error", "Home");

            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? objFromDb = _unitOfWork.VillaNumber.Get(villa => villa.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (objFromDb is not null)
            {
                _unitOfWork.VillaNumber.Remove(objFromDb);

                _unitOfWork.Save();

                TempData["success"] = "The villa number has been deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            TempData["error"] = "The villa number could not be deleted.";

            return View();
        }
    }
}
