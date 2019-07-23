using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Model.Entity
{
    public class Basket:EntityBase
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public Product Products { get; set; }
        public int Quantity { get; set; }

    }
}
