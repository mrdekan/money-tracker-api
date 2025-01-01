using System.Text.Json.Serialization;

namespace money_tracker.Application.Services
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ErrorMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public object? Data { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? StatusCode { get; set; }

        public static ServiceResult Ok(object? data = null) =>
            new ServiceResult { Success = true, StatusCode = 200, Data = data };

        public static ServiceResult Fail(string errorMessage, HttpCodes statusCode = HttpCodes.BadRequest) =>
            new ServiceResult { Success = false, StatusCode = (int)statusCode, ErrorMessage = errorMessage };
    }
}
