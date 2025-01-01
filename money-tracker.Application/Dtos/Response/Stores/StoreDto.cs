using money_tracker.Domain.Entities;

namespace money_tracker.Application.Dtos.Response.Stores
{
    public class StoreDto
    {
        public StoreDto(Store entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
