using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportsStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description 不能为空")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "请输入一个价格")]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
