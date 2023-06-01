namespace Infrastructure
{
    public class Error
    {
        public Error(string error, int statusCode = 400)
        {
            Errors = new() { error };
            StatusCode = statusCode;
        }

        public Error(IEnumerable<string> errors, int statusCode = 400)
        {
            Errors = errors.ToList();
            StatusCode = statusCode;
        }

        public List<string> Errors { get; set; } = default!;
        public int StatusCode { get; set; }
    }
}
