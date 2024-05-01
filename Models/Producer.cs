using eTicket.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Models
{
    public class Producer: IEntityBase
    {
        //Creating columns for the table Producer

        [Key]
        public int Id { get; set; }
        [DisplayName("Profile Picture")]
        [Required(ErrorMessage ="Profile Picture field cannot be empty")]
        public string ProducerProfilePictureUrl { get; set; }
        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full Name field cannot be empty")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = ("Full Name Should be Between 3-50 characters"))]
        public string ProducerFullName { get; set; }
        [DisplayName("Bio")]
        [Required(ErrorMessage = "Bio field cannot be empty")]
        public string ProducerBio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


        //creating Relationships
        public List<Movie> Movies { get; set; }
    }
}
