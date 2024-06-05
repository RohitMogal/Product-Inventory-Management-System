using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace ServicesContracts.Model
{
    public class ProductsModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int ProductID { get; set; }

        [MaxLength(90)]
        public string Name { get; set; }
        [MaxLength(100)]

        public string Category { get; set; }
        [MaxLength(10)]

        public string Price { get; set; }
        public int StockQuantity { get; set; }

        public DateTime LastUpdatedDate { get; set; }= DateTime.Now;
        public override string ToString()
        {
            return $"ProductID:{ProductID}, Name:{Name}, Category:{Category}, Price:{Price}, StockQuantity:{StockQuantity}";
        }

    }
}
