-- SQL for QuanLySinhVien database

Use QuanLySinhVien
go
-- Tao Bang Mon Hoc --
Create Table MonHoc
(
	MaMH char(5) primary key,
	TenMH nvarchar(30) not null,
	SoTrinh int not null check ( (SoTrinh>0)and (SoTrinh<7) )
)

Alter table MonHoc
	alter column TenMH nvarchar(50) not null

--- Tao Bang He Dao Tao ---
Create Table HeDT
(
	MaHeDT char(5) primary key,
	TenHeDT nvarchar(40) not null
)

--- Tao Bang Khoa Hoc ---
Create Table KhoaHoc
(
	MaKhoaHoc char(5) primary key,
	TenKhoaHoc nvarchar(20) not null
)
--- Tao Bang Khoa --
Create Table Khoa
(
	MaKhoa char(5) primary key,
	TenKhoa nvarchar(30) not null,
	DiaChi nvarchar(100) not null,
	DienThoai varchar(20) not null
)
-- Tao Bang Lop ---
Create Table Lop
(
	MaLop char(5) primary key,
	TenLop nvarchar(30) not null,
	MaKhoa char(5) foreign key references Khoa (MaKhoa),
	MaHeDT char(5) foreign key references HeDT (MaHeDT),
	MaKhoaHoc char(5) foreign key references KhoaHoc (MaKhoaHoc),
)
--- Tao Bang Sinh Vien ---
Create Table SinhVien
(
	MaSV char(15) primary key,
	TenSV nvarchar(20) ,
	GioiTinh bit ,
	NgaySinh datetime ,
	QueQuan nvarchar(50) ,
	MaLop char(5) foreign key references Lop(MaLop)
)

alter table SinhVien
	alter column GioiTinh nvarchar(10)

--- Tao Bang Diem ---
Create Table Diem
(
	MaSV char(15) foreign key references SinhVien(MaSV),
	MaMH char(5) foreign key references MonHoc (MaMH),
	HocKy int check(HocKy>0) not null,
	DiemLan1 int ,
	DiemLan2 int
)

---Nhap Du Lieu Cho Bang He Dao Tao --
insert into HeDT values('A01',N'Ðại Học')
insert into HeDT values('B01',N'Cao Ðẳng')
insert into HeDT values('C01',N'Trung Cấp')
insert into HeDT values('D01',N'Định hướng nghề nghiệp')

Select * from HeDT

-- Nhap Du Lieu Bang Ma Khoa Hoc ---
insert into KhoaHoc values('K1',N'Ðại học khóa 1')
insert into KhoaHoc values('K2',N'Ðại học khóa 2')
insert into KhoaHoc values('K3',N'Ðại học khóa 3')
insert into KhoaHoc values('K9',N'Ðại học khóa 4')
insert into KhoaHoc values('K10',N'Ðại học khóa 5')
insert into KhoaHoc values('K11',N'Ðại học khóa 6')

Select * from KhoaHoc

-- Nhap Du Lieu bang Khoa --
insert into Khoa values('CNTT',N'Công nghệ thông tin',N'Tầng 4 nhà B','043767890')
insert into Khoa values('CK',N'Cơ Khí',N'Tầng 5 nhà B','043764444')
insert into Khoa values('DT',N'Ðiện tử',N'Tằng 6 nhà B','043767777')
insert into Khoa values('KT',N'Kinh Tế',N'Tầng 2 nhà C','043769999')

Select * from Khoa

--- Nhap Du Lieu Cho Bang Lop --
insert into Lop values('MT1',N'Máy Tính 1','CNTT','A01','K2')
insert into Lop values('MT2',N'Máy Tính 2','CNTT','A01','K2')
insert into Lop values('MT3',N'Máy Tính 3','CNTT','A01','K2')
insert into Lop values('MT4',N'Máy Tính 4','CNTT','A01','K2')
insert into Lop values('KT1',N'Kinh tế 1','KT','A01','K2')

select * from Lop

-- Nhap Du Lieu Bang Sinh Vien --
insert into SinhVien values('0200000001',N'Đào Trường An',N'Nam','08/27/1989','Hai Duong','MT3')
insert into SinhVien values('0200000002',N'Nguyễn Văn Sỹ',N'Nam','2/08/1989','Nam Dinh','MT1')
insert into SinhVien values('0200000003',N'Phạm Thị Lam',N'Nam','7/04/1989','Ninh Binh','MT2')
insert into SinhVien values('0200000004',N'Đào Văn Út',N'Nữ','7/08/1989','Nam Đinh','MT3')
insert into SinhVien values('0200000005',N'Hoàng Văn Bé',N'Nam','7/08/1989','Ha Noi','MT3')
insert into SinhVien values('0200000006',N'Đào Bá Vân',N'Nam','7/08/1989','Ha Noi','MT3')
insert into SinhVien values('0200000007',N'Trần Trọng Vinh',N'Nam','7/08/1989','Hai Duong','MT2')
insert into SinhVien values('0200000007',N'Nguyễn Bá Hoàng',N'Nam','7/08/1989','Hai Duong','MT2')
insert into SinhVien values('0200000008',N'Lê Văn Bá',N'Nam','7/08/1989','Ha Nam','MT2')
insert into SinhVien values('0200000009',N'Phan Thị Ngọc Linh',N'Nữ','7/08/1989','Bac Giang','MT4')
insert into SinhVien values('0200000010',N'Vũ Văn Thưởng',N'Nữ','7/08/1989','Ha Noi','MT4')
insert into SinhVien values('0200000011',N'Lê Thị Út',N'Nam','7/08/1989','Hai Duong','MT4')
insert into SinhVien values('0200000012',N'Đào Bá Khải',N'Nam','7/08/1989','Nam Dinh','MT1')
insert into SinhVien values('0200000013',N'Trần Thị Thanh Tâm',N'Nam','7/08/1989','Nam Dinh','KT1')

select * from SinhVien

-- Nhap Du Lieu Bang Mon Hoc --
insert into MonHoc values('SQL',N'SQL',5)
insert into MonHoc values('JV',N'Java',6)
insert into MonHoc values('CNPM',N'Công nghệ phần mềm',4)
insert into MonHoc values('PTHT',N'Phân tích thiết kế và hệ thống',4)
insert into MonHoc values('Mang',N'Mạng máy tính',5)

select * from MonHoc

-- Nhap Du Lieu Bang Diem --
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000013','SQL',5,7)
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000012','SQL',5,6)
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000010','CNPM',5,8)
insert into Diem values('0200000002','SQL',5,4,6)
insert into Diem values('0200000003','Mang',5,4,5)
insert into Diem values('0200000004','JV',5,4,4)
insert into Diem values('0200000005','JV',5,4,6)
insert into Diem values('0200000006','PTHT',4,2,5)
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000013','SQL',4,9)
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000013','SQL',4,8)
insert into Diem values('0200000005','Mang',5,3,4)
insert into Diem values('0200000010','Mang',5,4,4)
insert into Diem(MaSV,MaMH,HocKy,DiemLan1) values('0200000003','Mang',5,8)

select * from Diem
