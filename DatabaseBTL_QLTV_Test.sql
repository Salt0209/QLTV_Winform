go
use master
go
if exists(select name from sysdatabases where name='QLTV_test')
drop Database QLTV_test
go
Create Database QLTV_test
go
use QLTV_test
go
CREATE TABLE tblTheLoai
(
	sMaloai NVARCHAR(50) PRIMARY KEY NOT NULL,
	sTenloai NVARCHAR(50)
)
go
CREATE TABLE tblNXB
(
	sMaNXB NVARCHAR(50) PRIMARY KEY NOT NULL,
	sTenNXB NVARCHAR(50),
	dDiachi NVARCHAR(50),
	sEmail NVARCHAR(50)
)
go
CREATE TABLE tblTacGia
(
	sMatacgia NVARCHAR(50) PRIMARY KEY NOT NULL,
	sTentacgia NVARCHAR(50),
	dNgaysinh DATETIME,
	sNoiCT NVARCHAR(50)
)
go
CREATE TABLE tblSach
(
	sMasach NVARCHAR(50) PRIMARY KEY NOT NULL,  --khóa chính
	sTensach NVARCHAR(50),
	sTacgia NVARCHAR(50),
	sMaNXB NVARCHAR(50),
	iNamXB INT,
	sTinhtrangsach NVARCHAR(50),
	sMaloai NVARCHAR(50) NOT NULL
)
go
CREATE TABLE tblCTSach
(
	sMasach NVARCHAR(50) NOT NULL,
	sMatacgia NVARCHAR(50) NOT NULL
)
go
CREATE TABLE tblSinhVien
(
	sMaSV NVARCHAR(50) PRIMARY KEY NOT NULL, --khóa chính
	sTenSV NVARCHAR(50),
	dNgaysinh DATETIME,
	sGioitinh NVARCHAR(50),
	sSDT NVARCHAR(50) UNIQUE,
	sEmail NVARCHAR(50) UNIQUE,
	sCMND NVARCHAR(50) UNIQUE
)
go
CREATE TABLE tblNhanVien
(
	sMaNV NVARCHAR(50) PRIMARY KEY NOT NULL, --khóa chính
	sTenNV NVARCHAR(50),
	dNgaysinh DATETIME,
	sGioitinh NVARCHAR(50),
	sSDT NVARCHAR(50),
	dNgayvaolam DATETIME,
	sCMND NVARCHAR(50) NOT NULL --khóa chính
)
go
CREATE TABLE tblMuonTra
(
	sMaMT NVARCHAR(50) PRIMARY KEY NOT NULL,
	sMaSV NVARCHAR(50) NOT NULL,
	sMaNV NVARCHAR(50) NOT NULL,
	dNgaymuon DATETIME
)
go
CREATE TABLE tblCTMuonTra
(
	sMaMT NVARCHAR(50) NOT NULL,
	sMasach NVARCHAR(50) NOT NULL,
	dNgaytra DATETIME
)
go
CREATE TABLE tblDangNhap
(
	sMaNV NVARCHAR(50) NOT NULL, --tên đăng nhập
	sMatkhau NVARCHAR(50) NOT NULL	-- mật khẩu
)
go

-----------------------------------------------------------------------------------
ALTER TABLE tblSach
ADD CONSTRAINT FK_maloai FOREIGN KEY(sMaloai) REFERENCES tblTheLoai(sMaloai)

ALTER TABLE tblSach
ADD CONSTRAINT FK_matacgia FOREIGN KEY(sTacgia) REFERENCES tblTacGia(sMatacgia)

ALTER TABLE tblSach
ADD CONSTRAINT FK_manxb FOREIGN KEY(sMaNXB) REFERENCES tblNXB(sMaNXB)

ALTER TABLE tblCTSach
ADD CONSTRAINT FK_masachh FOREIGN KEY(sMasach) REFERENCES tblSach(sMasach)

ALTER TABLE tblCTSach
ADD CONSTRAINT FK_matacgiaa FOREIGN KEY(sMatacgia) REFERENCES tblTacGia(sMatacgia)

ALTER TABLE tblCTSach
ADD CONSTRAINT FK_sach_tacgia PRIMARY KEY(sMasach,sMatacgia)

ALTER TABLE tblDangNhap
ADD CONSTRAINT FK_mNV PRIMARY KEY(sMaNV)

ALTER TABLE tblMuonTra
ADD CONSTRAINT FK_masvv FOREIGN KEY(sMaSV) REFERENCES tblSinhVien(sMaSV)

ALTER TABLE tblMuonTra
ADD CONSTRAINT FK_manvvv FOREIGN KEY(sMaNV) REFERENCES tblNhanVien(sMaNV)

ALTER TABLE tblCTMuonTra
ADD CONSTRAINT FK_mamt FOREIGN KEY(sMaMT) REFERENCES tblMuonTra(sMaMT)

ALTER TABLE tblCTMuonTra
ADD CONSTRAINT FK_mas FOREIGN KEY(sMasach) REFERENCES tblSach(sMasach)

ALTER TABLE tblCTMuonTra
ADD CONSTRAINT FK_masach_mamt PRIMARY KEY(sMasach,sMaMT)

-----------------------------------------------------------------------------
go
INSERT INTO tblTheLoai
VALUES ('TT',N'Truyện tranh'),
	('KH',N'Khoa học'),
	('TL',N'Tâm lý'),
	('GT',N'Giả tưởng'),
	('HS',N'Hình sự')

go
INSERT INTO tblNXB
VALUES('KD',N'Kim Đồng',N'Kim Văn, Hoàng Mai, Hà Nội','nxbkd@gmail.com'),
	('TT',N'Tuổi Trẻ',N'Nguyễn Trãi, Thanh Xuân, Hà Nội','nxbtt@gmail.com'),
	('GDDT',N'Giáo dục & đào tạo',N'Tạ Quang Bửu, Bách Khoa, Hà Nội','nxbgddt@gmail.com')

go
INSERT INTO tblTacGia
VALUES('PBC',N'Phan Đăng Thu','1949/04/16',N'Tự do'),
	('NDC',N'Nguyễn Đình Tưởng','1955/04/16',N'NXB Kim Đồng'),
	('ND',N'Nguyễn Du','1964/04/16',N'Tự do'),
	('NB',N'Nguyễn Bính','1956/04/16',N'NXB Tuổi Trẻ'),
	('PDH',N'Phan Đăng Hải','1980/04/16',N'NXB Kim Đồng')

go
INSERT INTO tblSach
VALUES('DSCVS',N'Du hành các vì sao','NB','GDDT','1990',N'Mới','GT'),
	('KGA',N'Kẻ gây án','NDC','KD','1986',N'Mới','HS'),
	('NS',N'Nam sử','ND','GDDT','1997',N'Mới','GT'),
	('KPDD',N'Khám phá đại dương','PDH','KD','2000',N'Mới','KH'),
	('CA',N'Chạy án','NDC','GDDT','1991',N'Mới','HS'),
	('DTMC',N'Đứa trẻ mồ côi','NDC','GDDT','1999',N'Mới','TL'),
	('TVTTS',N'Trở về thời tiền sử','PBC','KD','1991',N'Cũ','KH'),
	('CQS',N'Chiến quốc sử','PDH','KD','1997',N'Cũ','KH'),
	('NKVGC',N'Những kẻ vô gia cư','PBC','TT','1996',N'Mới','TL'),
	('CN',N'Conan','PDH','TT','1977',N'Mới','TT')

go
INSERT INTO tblCTSach
VALUES('CA','NDC'),
	('CN','PDH'),
	('CQS','PDH'),
	('DSCVS','NB'),
	('DTMC','NDC'),
	('KGA','NDC'),
	('KPDD','PDH'),
	('NKVGC','PBC'),
	('NS','ND'),
	('TVTTS','PBC')

go
INSERT INTO tblSinhVien
VALUES('15A10010173',N'Nguyễn Hoàng Nam','1997/02/03',N'Nam','0962091604','namhoangnguyen@gmail.com','026099003245'),
	('15A10010471',N'Phạm Hải Đăng','1997/08/15',N'Nam','0982165102','hdpham@gmail.com','026099001230'),
	('16A10010123',N'Nguyễn Thanh Tùng','1998/01/01',N'Nam','0915325456','thanhtungnguyen1998@gmail.com','026099000158'),
	('17A10010001',N'Đặng Phương Lan','1999/04/16',N'Nữ','0363568425','dplan@gmail.com','026099003514'),
	('17A10010147',N'Trần Trung Dũng','1999/02/17',N'Nam','0987985984','trtrdung@gmail.com','026099000315'),
	('17A10010316',N'Nguyễn Trọng Toàn','1999/05/05',N'Nam','0935465823','xuantubii16.tc@gmail.com','026099000179'),
	('17A10010345',N'Trần Thái Tùng','1999/02/20',N'Nam','0912136760','tranthanhtung@gmail.com','026099000546'),
	('17A10010465',N'Lưu Thị Lan Anh','1999/06/26',N'Nữ','0966333000','lananhluu@gmail.com','026099000147'),
	('18A10010258',N'Nguyễn Toàn Thắng','2000/10/30',N'Nam','0964551225','toanthang@gmail.com','026099001568'),
	('19A10010023',N'Nguyễn Thị Thùy Linh','2001/02/03',N'Nữ','0984654123','thuylinhnguyenthi@gmail.com','026099000150')

go
INSERT INTO tblNhanVien(sMaNV,sTenNV,dNgaysinh,sGioitinh,sSDT,dNgayvaolam,sCMND)
VALUES('NV01',N'Nguyễn Hoàng Hải','1991/01/16',N'Nam','0961256145','2005/04/16','026099014589'),
	('NV02',N'Đặng Thị Hà','1989/02/10',N'Nữ','0914587120','2010/03/10','026099014568'),
	('NV03',N'Phan Văn Cương','1985/03/26',N'Nam','0912345684','2015/07/12','026099014789'),
	('NV04',N'Nguyễn Thu Phương','1990/07/17',N'Nữ','0964125065','2016/06/26','026099036985'),
	('NV05',N'Phùng Thu Hà','1995/08/19',N'Nữ','0904230145','2017/01/01','026099015975')

go
INSERT INTO tblDangNhap(sMaNV,sMatkhau)
VALUES('NV01','026099014589'),
	('NV02','026099014568'),
	('NV03','026099014789'),
	('NV04','026099036985'),
	('NV05','026099015975'),
	('1','1');

go
INSERT INTO tblMuonTra
VALUES('MT01','15A10010173','NV01','2019/05/06'),
	('MT02','15A10010471','NV02','2018/04/16'),
	('MT03','16A10010123','NV03','2019/01/13'),
	('MT04','17A10010001','NV02','2019/06/06'),
	('MT05','17A10010147','NV04','2017/03/20'),
	('MT06','17A10010316','NV05','2018/05/25'),
	('MT07','17A10010316','NV02','2019/08/30'),
	('MT08','19A10010023','NV03','2019/10/27'),
	('MT09','18A10010258','NV01','2019/09/06'),
	('MT10','19A10010023','NV02','2019/08/10')

go
INSERT INTO tblCTMuonTra
VALUES('MT01','CA','2019-05-16'),
	('MT02','CN','2018-04-26'),
	('MT03','CQS','2019-01-10'),
	('MT04','DSCVS','2019-06-09'),
	('MT05','DTMC','2017-03-25'),
	('MT06','NKVGC','2018-05-30'),
	('MT07','KGA','2019-09-01'),
	('MT08','KPDD','2019-10-30'),
	('MT09','DTMC','2019-09-10'),
	('MT10','NS','2019-08-15'),
	('MT02','CQS','2018-04-20'),
	('MT04','KPDD','2019-06-16'),
	('MT05','NKVGC','2017-03-25'),
	('MT07','CA','2019-09-05'),
	('MT09','NS','2019-09-26')

----------------------------------------------
go
create proc sp_MuonTraSach
@masv nvarchar(50),@thang int,@nam int
as
begin
	select a.sMaMT,a.dNgaymuon,a.sMaSV,c.sTenSV,d.sTenNV,b.dNgaytra
	from tblMuonTra a inner join tblCTMuonTra b on a.sMaMT=b.sMaMT
		inner join tblSinhVien c on a.sMaSV = c.sMaSV
		inner join tblNhanVien d on a.sMaNV = d.sMaNV
	where a.sMaSV = @masv and MONTH(a.dNgaymuon)=@thang and YEAR(a.dNgaymuon)=@nam
end

go
create proc sp_ThongKeSLSachDuocMuon
@theloai nvarchar(50)
as
begin
	select TOP(5) b.sTensach, COUNT(d.sMaMT) AS [So_luot_duoc_muon]
	from tblTheLoai a inner join tblSach b on a.sMaloai=b.sMaloai
	inner join tblCTMuonTra c on b.sMasach=c.sMasach
	inner join tblMuonTra d on d.sMaMT=c.sMaMT
	where a.sMaloai = @theloai
	group by b.sTensach
	order by COUNT(d.sMaMT) desc
end
--select sMaNV as [Ma NV],sTenNV as [Ho ten],dNgaysinh as [Ngay sinh],sGioitinh as [Gioi tinh],sSDT as [SDT],dNgayvaolam as [Ngay vao lam],sCMND as [CMND] from tblNhanVien
go
create PROC sp_ThemNV
(@maNV NVARCHAR(50),@tenNV NVARCHAR(50),@ngaysinh DATETIME,@gioitinh NVARCHAR(50),@sdt NVARCHAR(50),
@ngayvaolam DATETIME,@cmnd NVARCHAR(50))
AS
BEGIN
		INSERT INTO tblNhanVien(sMaNV,sTenNV,dNgaysinh,sGioitinh,sSdt,dNgayvaolam,sCMND)
		VALUES(@MaNV,@TenNV,@Ngaysinh,@Gioitinh,@Sdt,@Ngayvaolam,@CMND)

		INSERT INTO tblDangNhap(sMaNV,sMatkhau)
		VALUES(@MaNV,@CMND)
END

go
CREATE PROC sp_SuaNV
(@MaNV NVARCHAR(50),@TenNV NVARCHAR(50),@Ngaysinh DATETIME,@Gioitinh NVARCHAR(50),@Sdt NVARCHAR(50),
@Ngayvaolam DATETIME,@CMND NVARCHAR(50))
AS
BEGIN
	BEGIN
	UPDATE tblNhanVien
	SET sTenNV = @TenNV,dNgaysinh = @Ngaysinh,sGioitinh = @Gioitinh,sSdt = @Sdt,dNgayvaolam = @Ngayvaolam,sCMND = @CMND
	WHERE sMaNV = @MaNV
	END
	
	BEGIN
	UPDATE tblDangNhap
	SET sMatkhau = @CMND
	WHERE sMaNV = @MaNV
	END
END

go
CREATE PROC sp_XoaNV(@MaNV NVARCHAR(50))
AS
BEGIN
	BEGIN
	DELETE FROM tblDangNhap
	WHERE sMaNV = @MaNV
	END
	
	BEGIN
	DELETE FROM tblNhanVien
	WHERE sMaNV = @MaNV
	END
END

--select sMaSV as [Ma SV],sTenSV as [Ho ten],dNgaysinh as [Ngay sinh],sGioitinh as [Gioi tinh],sSDT as [SDT],sEmail as [Email],sCMND as [CMND] from tblSinhVien
go
CREATE PROC sp_ThemSV
(@MaSV NVARCHAR(50),@TenSV NVARCHAR(50),@Ngaysinh DATETIME,@Gioitinh NVARCHAR(50),@Sdt NVARCHAR(50),
@Email NVARCHAR(50),@CMND NVARCHAR(50))
AS
BEGIN
	INSERT INTO tblSinhVien(sMaSV,sTenSV,dNgaysinh,sGioitinh,sSdt,sEmail,sCMND)
	VALUES(@MaSV,@TenSV,@Ngaysinh,@Gioitinh,@Sdt,@Email,@CMND)
END
go
CREATE PROC sp_SuaSV
(@MaSV NVARCHAR(50),@TenSV NVARCHAR(50),@Ngaysinh DATETIME,@Gioitinh NVARCHAR(50),@Sdt NVARCHAR(50),
@Email NVARCHAR(50),@CMND NVARCHAR(50))
AS
BEGIN
	UPDATE tblSinhVien
	SET sTenSV = @TenSV,dNgaysinh = @Ngaysinh,sGioitinh = @Gioitinh,sSdt = @Sdt,sEmail = @Email,sCMND = @CMND
	WHERE sMaSV = @MaSV
END
go
create proc sp_XoaSV
@MaSV nvarchar(50)
as
begin
	delete from tblSinhVien
	where sMaSV = @MaSV
end

select sMaloai as [Ma loai],sTenloai as [Ten loai sach] from tblTheLoai

go
CREATE PROC sp_SuaTL(@Matheloai NVARCHAR(50),@Tentheloai NVARCHAR(50))
AS
BEGIN
	UPDATE tblTheLoai
	SET sTenloai = @Tentheloai
	WHERE sMaloai = @Matheloai
END
go

--Proc them the loai
CREATE PROC sp_ThemTL(@Matheloai NVARCHAR(50),@Tentheloai NVARCHAR(50))
AS
BEGIN
	INSERT INTO tblTheLoai(sMaloai,sTenloai)
	VALUES(@Matheloai,@Tentheloai)
END
go
--Proc xoa the loai
CREATE PROC sp_XoaTL(@Matheloai NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblSach
	WHERE sMaloai = @Matheloai

	DELETE FROM tblTheLoai
	WHERE sMaloai = @Matheloai
END
go
--select sMaNXB as [Ma NXB],sTenNXB as [Ten NXB],dDiachi as [Dia chi],sEmail as [Email] from tblNXB
-------------------------------------------------------------------------------------------------------------------
go
CREATE PROC sp_ThemNXB(@MaNXB NVARCHAR(50),@TenNXB NVARCHAR(50),@Diachi NVARCHAR(50),@Email NVARCHAR(50))
AS
BEGIN
	INSERT INTO tblNXB(sMaNXB,sTenNXB,dDiachi,sEmail)
	VALUES(@MaNXB,@TenNXB,@Diachi,@Email)
END
go
--Proc sua NXB
CREATE PROC sp_SuaNXB(@MaNXB NVARCHAR(50),@TenNXB NVARCHAR(50),@Diachi NVARCHAR(50),@Email NVARCHAR(50))
AS
BEGIN
	UPDATE tblNXB
	SET sTenNXB = @TenNXB,dDiachi = @Diachi,sEmail = @Email
	WHERE sMaNXB = @MaNXB
END
go
--Proc xoa NXB
CREATE PROC sp_XoaNXB(@MaNXB NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblSach
	WHERE sMaNXB = @MaNXB

	DELETE FROM tblNXB
	WHERE sMaNXB = @MaNXB
END
----------------------------------------------------------------------------------------------------------------
--select sMatacgia as [Ma tac gia],sTentacgia as [Ten tac gia],dNgaysinh as [Ngay sinh],sNoiCT as [Noi cong tac] from tblTacGia
go
CREATE PROC sp_ThemTG(@MaTG NVARCHAR(50),@TenTG NVARCHAR(50),@Ngaysinh DATETIME,@NoiCT NVARCHAR(50))
AS
BEGIN
	INSERT INTO tblTacGia(sMatacgia,sTentacgia,dNgaysinh,sNoiCT)
	VALUES(@MaTG,@TenTG,@Ngaysinh,@NoiCT)
END
go
--Proc sua tac gia
CREATE PROC sp_SuaTG(@MaTG NVARCHAR(50),@TenTG NVARCHAR(50),@Ngaysinh DATETIME,@NoiCT NVARCHAR(50))
AS
BEGIN
	UPDATE tblTacGia
	SET sTentacgia = @TenTG,dNgaysinh = @Ngaysinh,sNoiCT = @NoiCT
	WHERE sMatacgia = @MaTG
END
go
--Proc xoa tac gia
CREATE PROC sp_XoaTG(@MaTG NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblSach
	WHERE sTacgia = @MaTG

	DELETE FROM tblTacGia
	WHERE sMatacgia = @MaTG
END
-----------------------------------------------------------------------------------------------------------------
--select sMasach as [Ma sach],sTensach as [Ten sach],sTacgia as [Tac gia],sMaNXB as [Ma NXB],iNamXB as [Nam xuat ban],sTinhtrangsach as [Tinh trang],sMaloai as [Ma loai] from tblSach


go
CREATE PROC sp_ThemSach(@Masach NVARCHAR(50),@Tensach NVARCHAR(50),@Matacgia NVARCHAR(50),@MaNXB NVARCHAR(50),@NamXB INT,
@Tinhtrang NVARCHAR(50),@Maloai NVARCHAR(50))
AS
BEGIN
	INSERT INTO tblSach(sMasach,sTensach,sTacgia,sMaNXB,iNamXB,sTinhtrangsach,sMaloai)
	VALUES(@Masach,@Tensach,@Matacgia,@MaNXB,@NamXB,@Tinhtrang,@Maloai)
END
go


--Proc sua sach
CREATE PROC sp_SuaSach(@Masach NVARCHAR(50),@Tensach NVARCHAR(50),@Matacgia NVARCHAR(50),@MaNXB NVARCHAR(50),@NamXB INT,
@Tinhtrang NVARCHAR(50),@Maloai NVARCHAR(50))
AS
BEGIN
	UPDATE tblSach
	SET sTensach = @Tensach,sTacgia = @Matacgia,sMaNXB = @MaNXB,iNamXB = @NamXB,sTinhtrangsach = @Tinhtrang,sMaloai = @Maloai
	WHERE sMasach = @Masach
END
go
--Proc xoa sach
CREATE PROC sp_XoaSach(@Masach NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblSach
	WHERE sMasach = @Masach
END
-------------------------------------------------------------------------------------------------------------------------------
go
CREATE PROC prThemMuonTra(@MaMT NVARCHAR(50),@MaSV NVARCHAR(50),@MaNV NVARCHAR(50),@Ngaymuon DATETIME)
AS
BEGIN
	INSERT INTO tblMuonTra(sMaMT,sMaSV,sMaNV,dNgaymuon)
	VALUES(@MaMT,@MaSV,@MaNV,@Ngaymuon)
END

go
--Proc sua muon tra
CREATE PROC prSuaMuonTra(@MaMT NVARCHAR(50),@MaSV NVARCHAR(50),@MaNV NVARCHAR(50),@Ngaymuon DATETIME)
AS
BEGIN
	UPDATE tblMuonTra
	SET sMaSV = @MaSV,sMaNV = @MaNV,dNgaymuon = @Ngaymuon
	WHERE sMaMT = @MaMT
END
go
--Proc xoa muon tra
CREATE PROC prXoaMuonTra(@MaMT NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblCTMuonTra
	WHERE sMaMT = @MaMT

	DELETE FROM tblMuonTra
	WHERE sMaMT = @MaMT
END
go
CREATE VIEW vwDSMuonTra
AS
	SELECT sMaMT AS [Mã mượn trả],tblSinhVien.sMaSV AS [Mã sinh viên],sTenSV AS [Tên sinh viên],tblNhanVien.sMaNV AS [Mã nhân viên],sTenNV AS [Tên nhân viên],dNgaymuon AS [Ngày mượn]
	FROM tblMuonTra,tblNhanVien,tblSinhVien
	WHERE tblMuonTra.sMaSV = tblSinhVien.sMaSV AND tblMuonTra.sMaNV = tblNhanVien.sMaNV

go
CREATE PROC prThemCTMT(@MaMT NVARCHAR(50),@Masach NVARCHAR(50),@Ngaytra DATETIME)
AS
BEGIN
	INSERT INTO tblCTMuonTra(sMaMT,sMasach,dNgaytra)
	VALUES(@MaMT,@Masach,@Ngaytra)
END
go
--Proc sua chi tiet muon tra
CREATE PROC prSuaCTMT(@MaMT NVARCHAR(50),@Masach1 NVARCHAR(50),@Masach2 NVARCHAR(50),@Ngaytra DATETIME)
AS
BEGIN
	UPDATE tblCTMuonTra
	SET sMasach = @Masach2,dNgaytra = @Ngaytra
	WHERE sMaMT = @MaMT AND sMasach = @Masach1
END
go
--Proc xoa chi tiet muon tra
CREATE PROC prXoaCTMT(@MaMT NVARCHAR(50),@Masach NVARCHAR(50))
AS
BEGIN
	DELETE FROM tblCTMuonTra
	WHERE sMasach = @Masach AND sMaMT = @MaMT
END