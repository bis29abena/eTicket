using eTicket.Data;
using eTicket.Data.Services;
using eTicket.Data.Static;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            var objMoviesList = await _service.GetAllAsync(n => n.Cinema);
            return View(objMoviesList);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var objMoviesList = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResults = objMoviesList.Where(n => n.MovieName.ToLower().Contains(searchString) || 
                n.MovieDescription.ToLower().Contains(searchString)).ToList();
                return View("Index", filteredResults);
            }
            return View("Index",objMoviesList);
        }

        //GET Movies/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            Movie movieDetail;

            CookieOptions cookie = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true
            };

            if(id == 0)
            {
                id = Convert.ToInt32(Request.Cookies["MovieId"]);
                movieDetail = await _service.GetMovieByIdAsync(id);
                cookie.Expires = DateTime.Now.AddDays(-1);
                return View(movieDetail);
            }
            movieDetail = await _service.GetMovieByIdAsync(id);
            HttpContext.Response.Cookies.Append("MovieId", id.ToString(), cookie);
            return View(movieDetail);
        }

        //Get Movies/Create
        public async Task<IActionResult> Create()
        {
            var moviesDropDown = await _service.GetNewMovieDropDownValuesVm();

            ViewBag.Cinemas = new SelectList(moviesDropDown.Cinemas, "Id", "CinemaName");
            ViewBag.Producers = new SelectList(moviesDropDown.Producers, "Id", "ProducerFullName");
            ViewBag.Actors = new SelectList(moviesDropDown.Actors, "Id", "ActorFullName");

            return View();
        }

        //POST Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var moviesDropDown = await _service.GetNewMovieDropDownValuesVm();

                ViewBag.Cinemas = new SelectList(moviesDropDown.Cinemas, "Id", "CinemaName");
                ViewBag.Producers = new SelectList(moviesDropDown.Producers, "Id", "ProducerFullName");
                ViewBag.Actors = new SelectList(moviesDropDown.Actors, "Id", "ActorFullName");

                return View(movie);
            }

            await _service.AddMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }

        //Get Movies/Edit/1
        public async Task<IActionResult> Edit(int Id)
        {
            var movieID = await _service.GetMovieByIdAsync(Id);

            if (movieID == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieID.Id,
                MovieName = movieID.MovieName,
                MovieDescription = movieID.MovieDescription,
                MoviePrice = movieID.MoviePrice,
                MovieCategory = movieID.MovieCategory,
                MovieImageUrl = movieID.MovieImageUrl,
                MovieEndDAte = movieID.MovieEndDAte,
                MovieStartDate = movieID.MovieStartDate,
                ProducerId = movieID.ProducerId,
                CinemaId = movieID.CinemaId,
                ActorIds = movieID.Actor_Movies.Select(n => n.ActorId).ToList()
            };

            var moviesDropDown = await _service.GetNewMovieDropDownValuesVm();

            ViewBag.Cinemas = new SelectList(moviesDropDown.Cinemas, "Id", "CinemaName");
            ViewBag.Producers = new SelectList(moviesDropDown.Producers, "Id", "ProducerFullName");
            ViewBag.Actors = new SelectList(moviesDropDown.Actors, "Id", "ActorFullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int Id, NewMovieVM movie)
        {
            if (Id != movie.Id) return View("NotFound");
            if (!ModelState.IsValid)
            {
                var moviesDropDown = await _service.GetNewMovieDropDownValuesVm();

                ViewBag.Cinemas = new SelectList(moviesDropDown.Cinemas, "Id", "CinemaName");
                ViewBag.Producers = new SelectList(moviesDropDown.Producers, "Id", "ProducerFullName");
                ViewBag.Actors = new SelectList(moviesDropDown.Actors, "Id", "ActorFullName");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);

            return RedirectToAction(nameof(Index));
        }
    }
}
