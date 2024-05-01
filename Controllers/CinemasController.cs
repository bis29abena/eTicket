using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {

            var objCinemasList = await _service.GetAllAsync();
            return View(objCinemasList);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Post Cinema/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema newCinema)
        {
            if (!ModelState.IsValid)
            {
                return View(newCinema);
            }
            await _service.AddAsync(newCinema);
            return RedirectToAction(nameof(Index));
        }

        //Get Cinemas/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaInfo = await _service.GetByIdAsync(id);
            if (cinemaInfo == null) return View("NotFound");
            return View(cinemaInfo);
        }

        //Get Cinema/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaInfo = await _service.GetByIdAsync(id);
            if (cinemaInfo == null) return View("NotFound");
            return View(cinemaInfo);
        }

        //Post Cinema/Edit/2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cinema editCinema)
        {
            if (!ModelState.IsValid)
            {
                return View(editCinema);
            }

            if (id != editCinema.Id)
            {
                editCinema.Id = id;
            }
            await _service.UpdateAsync(id, editCinema);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinemaInfo = await _service.GetByIdAsync(id);
            if (cinemaInfo == null) return View("NotFound");
            return View(cinemaInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Cinema editCinema)
        {
            if (!ModelState.IsValid)
            {
                return View(editCinema);
            }

            if (id != editCinema.Id)
            {
                editCinema.Id = id;
            }
            await _service.DeleteAsync(id, editCinema);
            return RedirectToAction(nameof(Index));
        }
    }
}
