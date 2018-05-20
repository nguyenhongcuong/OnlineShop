using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OnlineShop.Model.Abstract;

namespace OnlineShop.Model.Models
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerName { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerAddress { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerEmail { get; set; }

        [Required]
        [MaxLength(50)]
        public string CustomerMobile { get; set; }

        [Required]
        [MaxLength(256)]
        public string CustomerMessage { get; set; }

        [MaxLength(256)]
        public string PaymentMethod { get; set; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public string PaymentStatus { get; set; }

        public bool Status { set; get; }

        [StringLength(128)]
        [Column(TypeName = "nvarchar")]
        public string CustomerId { set; get; }

        [ForeignKey("CustomerId")]
        public virtual ApplicationUser User { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
