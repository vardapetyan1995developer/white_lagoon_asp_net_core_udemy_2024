﻿using Microsoft.AspNetCore.Mvc;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;

namespace WhiteLagoon.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;

        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();

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
                _db.Villas.Add(obj);

                _db.SaveChanges();

                return RedirectToAction("Index", "Villa");
            }

            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(villa => villa.Id == villaId);
            //Villa? obj = _db.Villas.Find(villaId);
            //var villaList = _db.Villas.Where(villa => villa.Price > 50 && villa.Occupancy > 0);

            if (obj == null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }
    }
}
