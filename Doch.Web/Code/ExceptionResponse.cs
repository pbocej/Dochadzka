namespace Doch.Web.Code
{
    public class ExceptionResponse
    {
        public string Detail { get; set; } = string.Empty;
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
        public int Status { get; set; }
        public string Title { get; set; } = string.Empty;
        public string TraceId { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
