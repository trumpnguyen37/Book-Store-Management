using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class DonHangNhap
    {
        [Display(Name = "Ngày nhập")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public string maNCC { get; set; }

        [Display(Name = "Sách")]
        public string tenSP { get; set; }
        public string maSP { get; set; }

        [Display(Name = "Số lượng")]
        public int soLuong { get; set; }

        [Display(Name = "Giá nhập")]
        public int giaNhap { get; set; }

    }
}