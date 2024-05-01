using eTicket.Data;
using eTicket.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [DisplayName("Movie name")]
        [Required(ErrorMessage = "Name is required")]
        public string MovieName { get; set; }

        [DisplayName("Movie Description")]
        [Required(ErrorMessage = "Name is required")]
        public string MovieDescription { get; set; }

        [DisplayName("Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double MoviePrice { get; set; }

        [DisplayName("Movie poster URL")]
        [Required(ErrorMessage = "Movie poster URL is required")]
        public string MovieImageUrl { get; set; }

        [DisplayName("Movie start date")]
        [Required(ErrorMessage ="Start date is required")]
        public DateTime MovieStartDate { get; set; }

        [DisplayName("Movie end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime MovieEndDAte { get; set; }

        [DisplayName("Select a category")]
        [Required(ErrorMessage = "Movie category is required")]
        public MovieCategory MovieCategory { get; set; }

        //Relationships
        [DisplayName("Select actor(s)")]
        [Required(ErrorMessage = "Movie actor(s) is required")]
        public List<int> ActorIds { get; set; }

        [DisplayName("Select a cinema")]
        [Required(ErrorMessage = "Movie cinema is required")]
        public int CinemaId { get; set; }

        [DisplayName("Select a producer")]
        [Required(ErrorMessage = "Movie producer is required")]
        public int ProducerId { get; set; }
    }
}
