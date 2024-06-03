using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ServicesContracts.Model
{
    public class ProductsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int ProductID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]

        public string Category { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }

        public override string ToString()
        {
            return $"ProductID:{ProductID}, Name:{Name}, Category:{Category}, Price:{Price}, StockQuantity:{StockQuantity}";
        }

    }
}
