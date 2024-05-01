using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
