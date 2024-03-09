namespace align.Utils
{
    public class ServiceResponse<T> where T : class
    {
        public T? Data { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public bool isSuccess => Data is not null;
    }
}
