using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        [Required]
        public string Name { get; set; }

        [StringLength(250)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }
        public DateTime? CreatdDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
    }
}
