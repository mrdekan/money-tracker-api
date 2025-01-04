using System.ComponentModel.DataAnnotations;

namespace money_tracker.Application.Dtos.Requests.Stores
{
    public class CreateStoreDto : UpdateStoreDto
    {
        [Required]
        public int JarId { get; set; }
    }
}
