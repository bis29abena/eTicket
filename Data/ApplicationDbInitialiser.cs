using eTicket.Data.Static;
using eTicket.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data
{
    public class ApplicationDbInitialiser
    {
        //Adding data into the database for testing purposes.
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            //Cinema
            if (!context.Cinemas.Any())
            {
                context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {

                            CinemaName = "Cinema 1",
                            CinemaLogo = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            CinemaDescription = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            CinemaName = "Cinema 2",
                            CinemaLogo = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            CinemaDescription = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            CinemaName = "Cinema 3",
                            CinemaLogo = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            CinemaDescription = "This is the Description of the first cinema"
                        },
                        new Cinema()
                        {
                            CinemaName = "Cinema 4",
                            CinemaLogo = "http://dotnethow.net/images/cinemas/cinema-4.jpeg",
                            CinemaDescription = "This is the Description of the first cinema"
                        },
                        new Cinema()
                        {
                            CinemaName = "Cinema 5",
                            CinemaLogo = "http://dotnethow.net/images/cinemas/cinema-5.jpeg",
                            CinemaDescription = "This is the Description of the first cinema"
                        },
                    });
                context.SaveChanges();
            }
            //Actors
            if (!context.Actors.Any())
            {
                context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            ActorFullName = "Actor 1",
                            ActorBio = "This is the Bio of the first actor",
                            ActorProfilePictureUrl = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new Actor()
                        {
                            ActorFullName = "Actor 2",
                            ActorBio = "This is the Bio of the second actor",
                            ActorProfilePictureUrl = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Actor()
                        {
                            ActorFullName = "Actor 3",
                            ActorBio = "This is the Bio of the second actor",
                            ActorProfilePictureUrl = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Actor()
                        {
                            ActorFullName = "Actor 4",
                            ActorBio = "This is the Bio of the second actor",
                            ActorProfilePictureUrl = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Actor()
                        {
                            ActorFullName = "Actor 5",
                            ActorBio = "This is the Bio of the second actor",
                            ActorProfilePictureUrl = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                context.SaveChanges();
            }
            //Producers
            if (!context.Producers.Any())
            {
                context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            ProducerFullName = "Producer 1",
                            ProducerBio = "This is the Bio of the first actor",
                            ProducerProfilePictureUrl = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Producer()
                        {
                            ProducerFullName = "Producer 2",
                            ProducerBio = "This is the Bio of the second actor",
                            ProducerProfilePictureUrl = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Producer()
                        {
                            ProducerFullName = "Producer 3",
                            ProducerBio = "This is the Bio of the second actor",
                            ProducerProfilePictureUrl = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Producer()
                        {
                            ProducerFullName = "Producer 4",
                            ProducerBio = "This is the Bio of the second actor",
                            ProducerProfilePictureUrl = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Producer()
                        {
                            ProducerFullName = "Producer 5",
                            ProducerBio = "This is the Bio of the second actor",
                            ProducerProfilePictureUrl = "http://dotnethow.net/images/producers/producer-5.jpeg"
                        }
                    });
                context.SaveChanges();
            }
            //Movies
            if (!context.Movies.Any())
            {
                context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            MovieName = "Life",
                            MovieDescription = "This is the Life movie MovieDescription",
                            MoviePrice = 39.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            MovieStartDate = DateTime.Now.AddDays(-10),
                            MovieEndDAte = DateTime.Now.AddDays(10),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            MovieName = "The Shawshank Redemption",
                            MovieDescription = "This is the Shawshank Redemption MovieDescription",
                            MoviePrice = 29.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            MovieStartDate = DateTime.Now,
                            MovieEndDAte = DateTime.Now.AddDays(3),
                            CinemaId = 2,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            MovieName = "Ghost",
                            MovieDescription = "This is the Ghost movie MovieDescription",
                            MoviePrice = 39.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            MovieStartDate = DateTime.Now,
                            MovieEndDAte = DateTime.Now.AddDays(7),
                            CinemaId = 1,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            MovieName = "Race",
                            MovieDescription = "This is the Race movie MovieDescription",
                            MoviePrice = 39.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            MovieStartDate = DateTime.Now.AddDays(-10),
                            MovieEndDAte = DateTime.Now.AddDays(-5),
                            CinemaId = 2,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            MovieName = "Scoob",
                            MovieDescription = "This is the Scoob movie MovieDescription",
                            MoviePrice = 39.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            MovieStartDate = DateTime.Now.AddDays(-10),
                            MovieEndDAte = DateTime.Now.AddDays(-2),
                            CinemaId = 4,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            MovieName = "Cold Soles",
                            MovieDescription = "This is the Cold Soles movie MovieDescription",
                            MoviePrice = 39.50,
                            MovieImageUrl = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            MovieStartDate = DateTime.Now.AddDays(3),
                            MovieEndDAte = DateTime.Now.AddDays(20),
                            CinemaId = 5,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                context.SaveChanges();
            }
            //Actors & Movies
            //if (!context.Actors_Movies.Any())
            //{
            //    context.Actors_Movies.AddRange(new List<Actor_Movie>()
            //        {
            //            new Actor_Movie()
            //            {
            //                ActorId = 1,
            //                MovieId = 6
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 3,
            //                MovieId = 6
            //            },

            //             new Actor_Movie()
            //            {
            //                ActorId = 1,
            //                MovieId = 7
            //            },
            //             new Actor_Movie()
            //            {
            //                ActorId = 4,
            //                MovieId = 7
            //            },

            //            new Actor_Movie()
            //            {
            //                ActorId = 1,
            //                MovieId = 2
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 2,
            //                MovieId = 2
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 5,
            //                MovieId = 2
            //            },


            //            new Actor_Movie()
            //            {
            //                ActorId = 2,
            //                MovieId = 2
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 3,
            //                MovieId = 3
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 4,
            //                MovieId = 3
            //            },


            //            new Actor_Movie()
            //            {
            //                ActorId = 2,
            //                MovieId = 4
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 3,
            //                MovieId = 4
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 4,
            //                MovieId = 4
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 5,
            //                MovieId = 4
            //            },


            //            new Actor_Movie()
            //            {
            //                ActorId = 3,
            //                MovieId = 5
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 4,
            //                MovieId = 5
            //            },
            //            new Actor_Movie()
            //            {
            //                ActorId = 5,
            //                MovieId = 5
            //            },
            //        });
            //    context.SaveChanges();
                
            //}
        }

        // Creating User Roles in the Database
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            //Role Section
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Created Role Section for each of the roles in the database.
            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRole.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRole.User));
            }

            //User Section
            var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            //Creating Admin if it doesn't exist
            string adminUserEmail = "admin@eticket.com";

            var adminUser = userManager.FindByEmailAsync(adminUserEmail);

            if (adminUser.Result == null)
            {
                var newAdmin = new ApplicationUser()
                {
                    FirstName = "Admin",
                    SurnName = "User",
                    UserName = "admin",
                    Email = adminUserEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newAdmin, "Bis@2923");
                await userManager.AddToRoleAsync(newAdmin, UserRole.Admin);
            }

            //Creating User if it doesn't exist
            string appUserEmail = "oseib@eticket.com";

            var appUser = userManager.FindByEmailAsync(appUserEmail);

            if (appUser.Result == null)
            {
                var newUser = new ApplicationUser()
                {
                    FirstName = "Bismark",
                    SurnName = "Osei",
                    UserName = "bosei",
                    Email = appUserEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newUser, "BisOsei@2923");
                await userManager.AddToRoleAsync(newUser, UserRole.User);
            }
        }
    }
}
