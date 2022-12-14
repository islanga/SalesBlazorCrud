using System.ComponentModel.DataAnnotations;

namespace SalesCrud.Data
{
    public class SalesDto
    {
        public int SalesId { get; set; }

        [Required(ErrorMessage = "Please Product Name"), MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Product Quantity")]
        public int Quantity { get; set; }
        public bool IsUpdate { get; set; } = false;
    }
}
