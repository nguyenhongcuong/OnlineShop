using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Model.Models
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string Url { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        public int MenuGroupId { get; set; }

        [MaxLength(10)]
        public string Target { get; set; }

        [Required]
        public bool Status { get; set; }

        [ForeignKey("MenuGroupId")]
        public virtual MenuGroup MenuGroup { get; set; }
    }
}
