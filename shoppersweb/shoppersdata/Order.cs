using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace shoppersdata
{
    public class Order : IdentityRecord
    {
        public int CustomerId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
        public decimal? OrderTotal { get; set; }
        public decimal? TotalSale { get; set; }
        public decimal? ShippingCost { get; set; }
        public decimal? Tax { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

    }
}
