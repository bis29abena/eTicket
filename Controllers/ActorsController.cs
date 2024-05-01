using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActionService _service;

        public ActorsController(IActionService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var objActorsList = await _service.GetAllAsync();
            return View(objActorsList);
        }

        //GET Actors/create
        public IActionResult Create()
        {
            return View();
        }

        //POST 
        [HttpPost]
        public async Task<IActionResult> Create([Bind("ActorFullName, ActorProfilePictureUrl, ActorBio")] Actor actor) 
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);
            return Redirect(nameof(Index));
        }

        //GET Actor by id
        //URL Actors/Detail/1
        [AllowAnonymous]
        public async Task<IActionResult> Detail(int id)
        {
            var actorDetails =  await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //GET Actors/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //POST 
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            if (id != actor.Id)
            {
                actor.Id = id;
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        //GET Actors/edit/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        //POST 
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id, Actor deleteActor)
        {
            if(id != deleteActor.Id)
            {
                id = deleteActor.Id;
            }

            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id, deleteActor);
            return RedirectToAction(nameof(Index));
        }
    } 
}
