using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests
{
    public class PaginatedRequest
    {
        [Range(1, int.MaxValue, ErrorMessage = "At least 1 item")]
        public int Limit { get; set; } = 10;

        [Range(0, int.MaxValue, ErrorMessage = "Can't be negative")]
        public int Offset { get; set; } = 0;
    }
}
