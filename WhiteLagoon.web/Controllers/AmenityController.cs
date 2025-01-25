using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WhiteLagoon.Application.Common.Interfaces;
using WhiteLagoon.Application.Common.Utility;
using WhiteLagoon.Domain.Entities;
using WhiteLagoon.Infrastructure.Data;
using WhiteLagoon.web.ViewModels;

namespace WhiteLagoon.web.Controllers
{
    [Authorize(Roles =SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var amenitys = _unitOfWork.Amenity.GetAll(includeproperties : "Villa");
            return View(amenitys);
        }
        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }).ToList()
            };

            return View(amenityVM);
        }

        
        [HttpPost]
        public IActionResult Create(AmenityVM obj)
        {

            if (ModelState.IsValid )
            {
                
                _unitOfWork.Amenity.Add(obj.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity has been Created Successfully";
                return RedirectToAction(nameof(Index));
            }

            obj.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            }).ToList();

            return View(obj);
        }

        public IActionResult Update(int amenityID) 
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
           
                Amenity = _unitOfWork.Amenity.Get(u=>u.Id == amenityID)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");           
            }
            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid )
            {

                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The Amenity has been Updated Successfully";
                return RedirectToAction(nameof(Index));
            }

            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
             return View(amenityVM);
           
        }
        public IActionResult Delete(int amenityID)
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),

                Amenity = _unitOfWork.Amenity.Get(u => u.Id == amenityID)
            };
            if (amenityVM.Amenity == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }
       

        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            Amenity? opjfromDb = _unitOfWork.Amenity
                .Get(u=>u.Id==amenityVM.Amenity.Id);
            if (opjfromDb is not null)
            {
                _unitOfWork.Amenity.Remove(opjfromDb);
                _unitOfWork.Save();
                TempData["success"]= "The Amenity has been Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["success"] = "The Amenity Could Not be Deleted.";
            return View();
        }
    }
}
