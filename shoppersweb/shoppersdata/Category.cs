using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace shoppersdata
{
    public class Category : IdentityRecord
    {
        public string CategoryName { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }

        [ForeignKey(nameof(ParentCategoryId))]
        public virtual Category CategoryChild { get; set; }

    }
}
