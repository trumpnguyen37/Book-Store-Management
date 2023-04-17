using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class NhapHang
    {
        [Display(Name = "Mã đơn")]
        public string id { get; set; }
        

        [Required(ErrorMessage = "Yêu cầu chọn ngày")]
        [Display(Name = "Ngày nhập")]
        public string maNCC { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        [Display(Name = "Ngày nhập hàng")]
        public DateTime ngayNhap { get; set; }

        

    }
}