using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using money_tracker.Domain.Interfaces;

namespace money_tracker.Domain.Entities
{
    public class Store : IDbStorable
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "JarId is required.")]
        [ForeignKey("Jar")]
        public int JarId { get; set; }
        public Jar Jar { get; set; }

        public ICollection<CurrencyBalance> CurrencyBalances { get; set; } = new List<CurrencyBalance>();
    }
}
