using eTicket.Data;
using eTicket.Data.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Models
{
    public class Movie : IEntityBase
    {
        //Created columns for the Movie Table

        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public string MovieName { get; set; }
        [Required]
        [DisplayName("Description")]
        public string MovieDescription { get; set; }
        [Required]
        [DisplayName("Start Date")]
        public DateTime MovieStartDate { get; set; }
        [Required]
        [DisplayName("End Date")]
        public DateTime MovieEndDAte { get; set; }
        [Required]
        [DisplayName("Price")]
        public double MoviePrice { get; set; }
        [DisplayName("Cover Image")]
        [Required]
        public string MovieImageUrl { get; set; }
        [Required]
        [DisplayName("Movie Category")]
        public MovieCategory MovieCategory { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        //Relationships
        public List<Actor_Movie> Actor_Movies { get; set; }

        //Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        //Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
