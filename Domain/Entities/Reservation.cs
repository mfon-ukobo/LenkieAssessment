using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("Reservations")]
    public class Reservation : BaseEntity<long>
    {
        public long BookId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
    }
}
