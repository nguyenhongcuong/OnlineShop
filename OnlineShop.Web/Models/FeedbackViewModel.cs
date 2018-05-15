using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.Models
{
    public class FeedbackViewModel
    {
        public int Id { get; set; }

        [StringLength(250, ErrorMessage ="Không được quá 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Tối đa 250 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [MaxLength(500,ErrorMessage = "Tối đa 500 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tin nhắn")]
        public string Message { get; set; }
        public DateTime CreatdDate { get; set; }
        public bool Status { get; set; }
    }
}