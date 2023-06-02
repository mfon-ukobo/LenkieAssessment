namespace Infrastructure.Features.Books.CheckOutBook
{
    public class CheckOutBookRequest
    {
        public long BookId { get; set; }
        public DateTime CheckInDate { get; set; }
    }
}
