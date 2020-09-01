using System.ComponentModel.DataAnnotations.Schema;

namespace shoppersdata
{
    public class CartItem : IdentityRecord
    {
        public int ItemId { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }
    }
}
