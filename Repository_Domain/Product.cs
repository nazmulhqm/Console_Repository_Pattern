using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository_Domain
{
    [Serializable]
    [Table("Product")]
    public class Product : IEntity<int>
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        [StringLength(30)]
        public string ProductCatagory { get; set; }

        [Required]
        [StringLength(50)]
        public string BrandName { get; set; }

        [Required]
        public decimal CostPerUnit { get; set; }

    }
}
