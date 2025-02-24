using System.Text.Json.Serialization;

namespace API5.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Liên kết với Product
        [JsonIgnore] // loại bỏ lặp product ở category
        public virtual ICollection<Product>? Products { get; set; }
        //1 category - * product
    }
}
