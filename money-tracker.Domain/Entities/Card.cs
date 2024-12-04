using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class Card : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Color is required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Color must be 6 characters.")]
        public string Color { get; set; }

        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}
