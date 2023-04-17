using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class ChiTietNhap
    {
        public string id { get; set; }

        public string maNhap { get; set; }

        [Display(Name = "Sách")]
        public string tenSP { get; set; }
        public string maSP { get; set; }

        [Display(Name = "Số lượng")]
        public int soLuong { get; set; }

        [Display(Name = "Giá nhập")]
        public int giaNhap { get; set; }

        public int thanhTien { get; set; }

        [Display(Name = "Mã danh mục")]
        public string maDM { get; set; }
    }
}