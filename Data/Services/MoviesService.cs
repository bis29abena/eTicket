using eTicket.Data.Base;
using eTicket.Data.ViewModels;
using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _db;
        public MoviesService(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task AddMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                MovieName = data.MovieName,
                MovieDescription = data.MovieDescription,
                MovieStartDate = data.MovieStartDate,
                MovieEndDAte = data.MovieEndDAte,
                MovieImageUrl = data.MovieImageUrl,
                MoviePrice = data.MoviePrice,
                MovieCategory = data.MovieCategory,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId
            };

            await _db.Movies.AddAsync(newMovie);
            await _db.SaveChangesAsync();

            //Add Actors to Movies
            foreach (var actorsId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorsId
                };

                await _db.Actors_Movies.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = _db.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return await movieDetails;
        }

        public async Task<NewMovieDropDownVM> GetNewMovieDropDownValuesVm()
        {
            var response = new NewMovieDropDownVM()
            {
                Actors = await _db.Actors.OrderBy(n => n.ActorFullName).ToListAsync(),
                Cinemas = await _db.Cinemas.OrderBy(n => n.CinemaName).ToListAsync(),
                Producers = await _db.Producers.OrderBy(n => n.ProducerFullName).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _db.Movies.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {

                dbMovie.MovieName = data.MovieName;
                dbMovie.MovieDescription = data.MovieDescription;
                dbMovie.MovieStartDate = data.MovieStartDate;
                dbMovie.MovieEndDAte = data.MovieEndDAte;
                dbMovie.MovieImageUrl = data.MovieImageUrl;
                dbMovie.MoviePrice = data.MoviePrice;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.ProducerId = data.ProducerId;

                await _db.SaveChangesAsync();
            }


            // Remove existing movies
            var existingActors = _db.Actors_Movies.Where(Actors => Actors.MovieId == data.Id);
            _db.Actors_Movies.RemoveRange(existingActors);
            await _db.SaveChangesAsync();


            //Add Actors to Movies
            foreach (var actorsId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorsId
                };

                await _db.Actors_Movies.AddAsync(newActorMovie);
            }
            await _db.SaveChangesAsync();
        }
    }
}
