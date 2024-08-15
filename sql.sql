create database quanlyquay1
go
use quanlyquay1
go
create  table TaiKhoan(
maso int IDENTITY(1,1) not null primary key,
tentaikhoan varchar(30) unique not null,
matkhau varchar (30) not null,
email varchar(30) unique not null,
)
go
create table QuayKinhDoanh(
maquay char(10) not null primary key,
tenquay nvarchar(50) unique not null,
mathangchinh nvarchar(50) not null,
vitri nvarchar(50) unique  not null,
tinhtrang nvarchar(50)  not null,
)
go

create table ChuQuay(
machuquay char(10) not null primary key,
maquay char(10) unique not null foreign key references QuayKinhDoanh(maquay),
hovaten nvarchar(50) not null,
gioitinh nvarchar(10) ,
ngaysinh date not null,
diachi nvarchar(50),
sdt char(12) unique not null,
)
go
create table HangHoa (
mahanghoa char(10) not null primary key,
tenhang nvarchar(50) not null,
giaban float not null,
xuatxu nvarchar(50) not null,
maquay char(10) unique not null foreign key references QuayKinhDoanh(maquay),
)
go
create table NhapHang (
madonnhap char(10) not null primary key,
maquay char(10) not null foreign key references QuayKinhDoanh(maquay),
soluongnhap int not null,
ngaynhap date not null,
)
go

create table XuatHang (
madonxuat char(10) not null primary key,
maquay char(10) not null foreign key references QuayKinhDoanh(maquay),
soluongxuat int not null,
ngayxuat date not null,
) 

create table Kho(
makho int identity(1,1) not null primary key,
maquay char(10) not null foreign key references QuayKinhDoanh(maquay),
tonkho int not null,
)

go
CREATE TRIGGER update_Kho
ON NhapHang
AFTER INSERT
AS
BEGIN
    UPDATE Kho
    SET tonkho = tonkho + i.soluongnhap
    FROM Kho 
    INNER JOIN inserted i
    ON Kho.maquay = i.maquay;
END;

go
CREATE TRIGGER update_inventory_after_sale
ON XuatHang
AFTER INSERT
AS
BEGIN
    UPDATE Kho
    SET tonkho = tonkho - i.soluongxuat
    FROM Kho
    INNER JOIN inserted i
    ON Kho.maquay = i.maquay;
END;
go

CREATE TRIGGER insert_new_inventory
ON QuayKinhDoanh
AFTER INSERT
AS
BEGIN
    INSERT INTO Kho (maquay, tonkho)
    SELECT i.maquay, 0
    FROM inserted i;
END;
go
insert into TaiKhoan values 
('admin','123456','admin@gmail.com'),
('Long','123456','Long@gmail.com')

go
go
insert into QuayKinhDoanh values 
('001',N'Đồ khô',N'bò khô',N'số 10',N'Mở cửa'),
('002',N'Đồ cũ',N'laptop cũ',N'số 12',N'Mở cửa'),
('003',N'Gia dụng','nồi cơm',N'số 14',N'Mở cửa'),
('004',N'Vải các loại',N'vải',N'số 16',N'Mở cửa'),
('005',N'Đồ gỗ mĩ nghệ',N'tượng gỗ',N'số 18',N'Mở cửa'),
('006',N'Di động Việt',N'điện thoại',N'số 20',N'Mở cửa'),
('007',N'Đồ tây',N'vest',N'số 22',N'Đóng cửa'),
('008',N'watch TM',N'đồng hồ',N'số 24',N'Đóng cửa')
go
go
insert into ChuQuay(machuquay,maquay,hovaten,gioitinh,ngaysinh,diachi,sdt) values 
 ('CQ01','001',N'Trần Đức Văn',N'nam','11-10-1981',N'Nam Định','09532112'),
 ('CQ02','002',N'Phạm Linh',N'nữ','10-6-1990',N'Hà Nội','09912145'),
 ('CQ03','003',N'Trần Đức Anh',N'nam','12-10-1991',N'Hải Phòng','09910672'),
 ('CQ04','004',N'Nguyễn Chi',N'nữ','11-9-1989',N'Hà Nội','09914232'),
 ('CQ05','005',N'Nguyễn Đạt ',N'nam','10-3-1999',N'Nam Định','0995862'),
 ('CQ06','006',N'Vũ Đức',N'nam','5-30-1989',N'Hải Phòng','09963412'),
 ('CQ07','007',N'Phạm Nam',N'nam','9-10-1992',N'Hà Nội','09912165'),
 ('CQ08','008',N'Thế Lam',N'nữ','6-23-1995',N'Quảng Ninh','0991654')
 go
 go
 insert into HangHoa(mahanghoa,tenhang,giaban,xuatxu,maquay) values  
 ('R01',N'bò khô','5000',N'Việt Nam','001'),
 ('H02',N'laptop cũ','50000',N'Việt Nam','002'),
 ('CB02',N'nồi cơm','169000',N'Nhật Bản','003'),
 ('V01',N'vải','500000',N'Trung Quốc','004'),
 ('T01',N'tượng gỗ','5000000',N'Việt Nam','005'),
 ('DT01',N'điện thoại','5000000',N'Trung Quốc','006'),
 ('AV01',N'vest','6000000',N'Việt Nam','007'),
 ('DH01',N'đồng hồ','1500000',N'Nhật Bản','008')
 go
 go
  insert into NhapHang(madonnhap,maquay,soluongnhap,ngaynhap) values
 ('01','001','100','5-20-2024'),
 ('02','002','150','5-21-2024'),
 ('03','003','110','5-15-2024'),
 ('04','004','200','5-12-2024'),
 ('05','005','170','5-20-2024'),
 ('06','006','100','5-19-2024')
 go
 go
  insert into XuatHang(madonxuat,maquay,soluongxuat,ngayxuat) values
 ('X01','001','10','5-21-2024'),
 ('X02','002','50','5-21-2024'),
 ('X03','003','60','5-21-2024'),
 ('X04','004','50','5-21-2024'),
 ('X05','005','80','5-21-2024'),
 ('X06','006','20','5-21-2024')
 
 go

