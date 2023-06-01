using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("CheckOuts")]
    public class CheckOut : BaseEntitiy<long>
    {
        public long BookId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime ExpectedCheckInDate { get; set; }
        public DateTime? CheckInDate { get; set; }
    }
}
