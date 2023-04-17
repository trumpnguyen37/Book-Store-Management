using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class ChiTietHoaDon
    {
        public int id { get; set; }

        [Display(Name = "Mã hóa đơn")]
        public int maHD { get; set; }
        public int maSP { get; set; }

        [Display(Name = "Tên sách")]
        public string tenSP { get; set; }
        [Display(Name = "Số lượng")]
        public int soLuong { get; set; }
        [Display(Name = "Giá bán")]
        public int giaBan { get; set; }
        [Display(Name = "Thành tiền")]
        public int thanhTien { get; set; }

    }
}