using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class NhaCungCap
    {
        public string id { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập tên nhà cung cấp")]
        [Display(Name = "Tên nhà cung cấp")]
        public string tenNhaCungCap { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập địa chỉ")]
        [Display(Name = "Địa chỉ")]
        public string diaChi { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [Display(Name = "Email")]
        public string email { get; set; }
    }
}