using eTicket.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Models
{
    public class Actor : IEntityBase
    {
        //Created a columns for the table Actor

        [Key]
        public int Id { get; set; }
        [DisplayName("Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is required")]
        public string ActorProfilePictureUrl { get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Name of the actor is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name should have more than 3 characters")]
        public string ActorFullName { get; set; }
        [DisplayName("Bio")]
        [Required(ErrorMessage = "Actors Biography is required")]
        public string ActorBio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Relationship 
        public List<Actor_Movie> Actor_Movies { get; set; }
    }
}
