using System.ComponentModel.DataAnnotations;

namespace Jap.Web.Models
{
    public class ProductDto
    {
        public ProductDto()
        {
            Count = 1;
        }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ImageUrl { get; set; }
        [Range(1,100)]
        public int Count { get; set; }
    }
}
