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
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var objProducersList = await _service.GetAllAsync();
            return View(objProducersList);
        }

        //GET Producer/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Post 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProducerProfilePictureUrl, ProducerFullName, ProducerBio")] Producer newProducer)
        {
            if (!ModelState.IsValid)
            {
                return View(newProducer);
            }
            await _service.AddAsync(newProducer);
            return RedirectToAction(nameof(Index));

        }

        //GET Producers/Details/1
        public async Task<IActionResult> Update(int id)
        {
            var producerDetail = await _service.GetByIdAsync(id);
            if (producerDetail == null) return View("NotFound");
            return View(producerDetail);
        }

        //POST Producer/Update/1
        [HttpPost]
        public async Task<IActionResult> Update(int id, Producer updateProducer)
        {
            if (!ModelState.IsValid)
            {
                return View(updateProducer);
            }

            if(id != updateProducer.Id)
            {
                updateProducer.Id = id;
            }

            await _service.UpdateAsync(id,updateProducer);
            return RedirectToAction(nameof(Index));
        }

        //GET Producer/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var ProducerToDelete = await _service.GetByIdAsync(id);
            if (ProducerToDelete == null) return View("NotFound");
            return View(ProducerToDelete);
        }

        //POST Producer/Delete/1
        [HttpPost]
        public async Task<IActionResult> Delete(int id, Producer deleteProducer)
        {
            if(id != deleteProducer.Id)
            {
                deleteProducer.Id = id;
            }

            await _service.DeleteAsync(id, deleteProducer);
            return RedirectToAction(nameof(Index));
        }
    }
}
