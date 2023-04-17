create database QuanLyChoThueThietBiXayDung


go
use  QuanLyChoThueThietBiXayDung

go
create table Quyen(
	maQuyen Varchar(11)  primary key not null,
	tenQuyen Nvarchar(100) not null
)

create table TaiKhoan(
	maTK Varchar(11) primary key not null,
	tenTaiKhoan Varchar(100) not null,
	matKhau Varchar(100) not null,
	maQuyen Varchar(11) foreign key references Quyen(maQuyen) not null
	)


create table KhachHang (
  maKH Varchar(11) primary key not null,
  ten Nvarchar(100)  not null,
  diaChi Nvarchar(100)  not null,
  soDienThoai Varchar(50) not null,
  gioiTinh Nvarchar(50) not null,
  email Varchar(100) not null,
  CMND Varchar(50) not null,
  maTK Varchar(11) foreign key references TaiKhoan(maTK) not null
) 

create table NhanVien(
	maNV Varchar(11) primary key not null,
	ten Nvarchar(100)  not null,
	luong Varchar(100) not null,
	diaChi Nvarchar(100)  not null,
	soDienThoai Varchar(50) not null,
	gioiTinh Nvarchar(50) not null,
	email Varchar(100) not null,
	CMND Varchar(50) not null,
	maTK Varchar(11) foreign key references TaiKhoan(maTK) not null		
)

create table DonThue(
	maDT Varchar(11) primary key not null,
	maKH Varchar(11) foreign key references KhachHang(maKH) not null,
	maNV Varchar(11) foreign key references NhanVien(maNV) not null,
	ngayNhap date not null,
	trangThai Nvarchar(50)  not null
)

create table DanhMuc(
	maDM Varchar(11) primary key not null,
	tenDanhMuc Nvarchar(100)  not null,
	trangThai  Nvarchar(50) not null
) 

create table SanPham(
	maSP Varchar(11) primary key not null,
	tenSanPham  Nvarchar(100) not null,
	maDM Varchar(11) foreign key references DanhMuc(maDM) not null,
	gia  Varchar(50)  not null,
	soLuong  Varchar(100) not null,
	donVi  Nvarchar(100) not null,
	tinhTrang  Nvarchar(100) not null,
	hinhAnh  Varchar(100) not null,
	mota Nvarchar(100) null
)

create table ChiTietHoaDon(
	maCTHD Varchar(11) primary key not null,
	maDT Varchar(11) foreign key references DonThue(maDT) not null,
	maSP Varchar(11) foreign key references SanPham(maSP) not null,
	soLuong nvarchar(255) not null,
	giaBan nvarchar(255) not null
)

create table NhaCungCap (
	maNCC Varchar(11) primary key not null,
	tenNhaCungCap  Nvarchar(100) not null,
	soDienThoai  Varchar(50) not null,
	diaChi Nvarchar(100) not null,
	email Varchar(100) null
)

create table NhapHang(
	maNH Varchar(11) primary key not null,
	ngayNhap date not null,
	maNCC Varchar(11) foreign key references NhaCungCap(maNCC) not null
)

create table ChiTietNhap(
	maCTN Varchar(11) primary key not null,
	maNhap Varchar(11) foreign key references NhapHang(maNH) not null,
	maSP Varchar(11) foreign key references SanPham(maSP) not null,
	maNV Varchar(11) foreign key references NhanVien(maNV) not null,
	soLuong Nvarchar(100)  not null,
	giaNhap  Varchar(50) not null
)


go

insert into Quyen(tenQuyen)
values ('Admin'),
	   (N'Nhân viên'),
	   (N'Khách hàng')

insert into NguoiDung(ten,diaChi,SDT,gioiTinh,email,CMND,tenTaiKhoan,matKhau,maQuyen,trangThai)

values ( N'Nguyễn Văn Trung', N'Quảng Bình', '0373532115','Nam', 'trung@gmail.com','04420200468', 'admin', '123456', '1', '1'),
	   ( N'Bùi Viết Trường', N'Đà Nẵng', '0977038241','Nam', 'truong@gmail.com','044202048101', 'truong', '123456', '2', '1'),
	   ( N'Hoàng Thái Thắng', N'Quảng Trị', '0367842145','Nam','thang@gmail.com', '02320200468', 'thang', '123456', '3', '1'),
	   ( N'Nguyễn Thị Nga', N'Quảng Bình', '0905532487',N'Nữ','nga@gmail.com', '04820200120', 'nga', '123456', '3', '1')

insert into HoaDon(maKH,ngayNhap,trangThai)
values ('1','12/6/2022','1'),
	   ('2','12/6/2022','1')

insert into DanhMuc(tenDanhMuc,trangThai)
values (N'Sách cho trẻ em','1'),
	   (N'Sách tham khảo','1'),
	   (N'Văn học','1'),
	   (N'Kinh Tế','1'),
	   (N'Khác','1')

select * from SanPham

update SanPham
set  tenSanPham= N'Dế mèn không thích phiêu lưu ký' where id = '1'

insert into SanPham(mota,tenSanPham,maDM,gia,soLuong,tinhTrang,hinhAnh)
values ('1',N'Dế mèn phiêu lưu ký','1',100000,'100','1','vanhoc.jpg'),
	   ('1',N'Dế mèn phiêu lưu ký','1',100000,'100','1','demen.jpg'),
	   ('2',N'Sách giải văn học','2',100000,'100','1','vanhoc.jpg'),
	   ('3',N'Nhà giả kim','3',100000,'100','1','ngk.jpg'),
	   ('3',N'Lịch sử kinh tế học','4',100000,'100','1','kinhte.jpg'),
	   ('4',N'5000 điều kỳ thú','5',100000,'100','1','khac1.jpg')

insert into ChiTietHoaDon(maHD,maSP,soLuong,giaBan)
values ('1','1','2',120000),
	   ('2','2','2',200000)

insert into NhaCungCap(tenNhaCungCap,SDT,diaChi,email)
values (N'Kho sách Quảng Bình','0373532115',N'Quảng Bình','qbbook@gmail.com'),
	   (N'Kho sách Đà Nẵng','0373532123',N'Đà Nẵng','dnbook@gmail.com')

insert into NhapHang(ngayNhap,maNCC)
values ('12/5/2022','1'),
	   ('12/5/2022','2')

insert into ChiTietNhap(maNhap,maSP,soLuong,giaNhap)
values ('1','1','50',50000),
	   ('2','2','50',50000),
	   ('1','3','50',50000),
	   ('2','4','50',50000),
	   ('1','5','50',50000)


