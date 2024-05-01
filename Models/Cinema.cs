using eTicket.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Models
{
    public class Cinema : IEntityBase
    {

        //Creating Columns for the table Cinema

        [Key]
        public int Id { get; set; }
        [DisplayName("Cinema Logo")]
        [Required(ErrorMessage = "Please The Logo is required")]
        public string CinemaLogo { get; set; }
        [ DisplayName("Cinema Name")]
        [Required(ErrorMessage = "Please The Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Please Cinema Name should be more than 3 characters")]
        public string CinemaName { get; set; }
        [DisplayName("Description")]
        [Required(ErrorMessage = "Please The Description is required")]

        public string CinemaDescription { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Creating Relationships
        public List<Movie> Movies { get; set; }
    }
}
