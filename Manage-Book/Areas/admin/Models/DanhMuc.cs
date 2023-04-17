using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CuoiKi.Areas.admin.Models
{
    public class DanhMuc
    {
        public string id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập tên danh mục")]
        [Display(Name = "Danh mục")]
        public string tenDanhMuc { get; set; }
    }
}