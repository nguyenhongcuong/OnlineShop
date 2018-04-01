using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Model.Models
{
    [Table("Errors")]
    public class ErrorLog
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public string StrackTrace { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
