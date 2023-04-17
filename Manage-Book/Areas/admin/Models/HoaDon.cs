using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class HoaDon
    {
        [Display(Name = "Mã hóa đơn")]
        public int id { get; set; }
        public int maKH { get; set; }

        [Display(Name = "Tên khách hàng")]
        public string tenKH  { get; set; }

        [Required]
        [Display(Name = "Ngày nhập")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd / MM / yyyy}")]
        public DateTime ngayNhap { get; set; }
    }
}