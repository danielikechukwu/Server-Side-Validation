namespace ValidationDemo.Models
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<FieldError> Errors { get; set; } = new List<FieldError>();
    }

    // Represents an error for a specific field.
    public class FieldError
    {
        public string Field { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
    }
}
