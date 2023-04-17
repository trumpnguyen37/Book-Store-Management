var nhaphang = {
    init: function () {
        nhaphang.loadDanhMuc();
        nhaphang.chonEvent();
    },
    chonEvent: function () {
        $('#ddlDanhMuc').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                nhaphang.loadSanPham(parseInt(id));
            }
            else {
                $('#ddlSanPham').html('');
            }
        });
    },
    loadDanhMuc: function() {
        $.ajax({
            url: '/admin/NhapHang/LoadDanhMuc',
            type: "POST",
            dataType: "json",
            success: function(response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn loại sản phẩm--</option>';
                    var data = response.data;
                    $.each(data, function(i, item) {

                        html += '<option value="' + item.id + '">' + item.tenDanhMuc +
                            '</option>'
                    });

                    $('#ddlDanhMuc').html(html);

                }
            }
        })
    },
    loadSanPham: function(id) {
        $.ajax({
            url: '/admin/NhapHang/LoadSanPham',
            type: "POST",
            data: { maDM: id },
            dataType: "json",
            success: function(response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn sản phẩm--</option>';
                    var data = response.data;
                    $.each(data, function(i, item) {

                        html += '<option value="' + item.id + '">' + item.tenSanPham +
                            '</option>'
                    });

                    $('#ddlSanPham').html(html);

                }
            }
        })
    }
}
nhaphang.init();