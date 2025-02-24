using System.Text.Json.Serialization;

namespace API5.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        // Khóa ngoại liên kết với Category
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
