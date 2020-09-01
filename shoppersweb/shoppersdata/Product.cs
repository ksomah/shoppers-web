using System.ComponentModel.DataAnnotations.Schema;

namespace shoppersdata
{
    public class Product : IdentityRecord
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public string ImageUrl { get; set; }
        public string ProductSize { get; set; }
        public bool? NewWithTag { get; set; }
        public string Brand { get; set; }
        public string Color { get; set; }
        public decimal? OrginalPrice { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
