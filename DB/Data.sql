CREATE DATABASE Publication
GO

USE Publication
GO


-- BAI BAO TABLE

CREATE TABLE Bai_bao
(
	id INT IDENTITY PRIMARY KEY,
	tieuDe NVARCHAR(100) NOT NULL,
	maSo INT NOT NULL unique,
	ngayGui DATE NOT NULL DEFAULT GETDATE(),
	cacTuKhoa NVARCHAR(100) NOT NULL,
	DOI NVARCHAR(100),
	tomTat NVARCHAR(1000) NOT NULL,
	fileBaiBao NVARCHAR(500) NOT NULL,
	loaiBaiBao NVARCHAR(50) NOT NULL,
	trangthai NVARCHAR(50) NOT NULL,
	ketQua NVARCHAR(10)
)
GO



-- SACH TABLE

CREATE TABLE Sach
(
	id INT IDENTITY PRIMARY KEY,
	tenSach NVARCHAR(50) NOT NULL,
	tenTacGia NVARCHAR(50) NOT NULL,
	nhaXuatBan NVARCHAR(50) NOT NULL,
	tongSoTrang INT NOT NULL,
	ISBN NVARCHAR(50) NOT NULL unique
)
GO

-- NHA KHOA HOC TABLE

CREATE TABLE Nha_khoa_hoc
(
	id INT IDENTITY PRIMARY KEY,
	hoVaTen NVARCHAR(100) NOT NULL,
	dienThoai INT NOT NULL unique,
	diaChi NVARCHAR(100) NOT NULL,
	ngheNghiep NVARCHAR(100) NOT NULL,
	emailCaNhan NVARCHAR(100) NOT NULL unique,
	coQuan NVARCHAR(100)
)
GO


--ACCOUNT TABLE

CREATE TABLE Account
(
	username NVARCHAR(100) PRIMARY KEY NOT NULL,
	displayName NVARCHAR(100) NOT NULL,
	password NVARCHAR(1000) NOT NULL,
	idNhaKhoaHoc INT,
	role INT NOT NULL -- 0: Ban Bien Tap; 1: Phan Bien; 2: Tac Gia

	FOREIGN KEY (idNhaKhoaHoc) REFERENCES dbo.Nha_khoa_hoc(id)
)
GO

-- PHAN BIEN TABLE

CREATE TABLE Phan_bien
(
	id INT IDENTITY PRIMARY KEY,
	trinhDo NVARCHAR(100) NOT NULL,
	ngayCongTac DATE NOT NULL DEFAULT GETDATE(),
	chuyenMon NVARCHAR(100) NOT NULL,
	soLuongPhanBien INT NOT NULL DEFAULT 0,
	emailCoQuan NVARCHAR(100) unique,
	idNhaKhoaHoc INT NOT NULL

	FOREIGN KEY (idNhaKhoaHoc) REFERENCES dbo.Nha_khoa_hoc(id)
)
GO

-- BAN BIEN TAP TABLE

CREATE TABLE Ban_bien_tap
(
	id INT IDENTITY PRIMARY KEY,
	hoVaTen NVARCHAR(100) NOT NULL
)
GO

-- BAI BAO - TAC GIA TABLE
CREATE TABLE Bai_bao_Tac_gia
(
	id INT IDENTITY PRIMARY KEY,
	idNhaKhoaHoc INT NOT NULL,
	idBaiBao INT NOT NULL

	FOREIGN KEY (idNhaKhoaHoc) REFERENCES dbo.Nha_khoa_hoc(id),
	FOREIGN KEY (idBaiBao) REFERENCES dbo.Bai_bao(id)
)
GO

-- PHAN BIEN - BAI BAO TABLE
CREATE TABLE Phan_bien_bai_bao
(
	id INT IDENTITY PRIMARY KEY,
	idPhanBien INT NOT NULL,
	idBaiBao INT NOT NULL

	FOREIGN KEY (idPhanBien) REFERENCES dbo.Phan_bien(id),
	FOREIGN KEY (idBaiBao) REFERENCES dbo.Bai_bao(id)
)
GO

-- PHAN BIEN SACH TABLE
CREATE TABLE Phan_bien_sach
(
	id INT IDENTITY PRIMARY KEY,
	soTrangToiDa INT NOT NULL,
	idSach INT NOT NULL,
	idBaiBao INT NOT NULL

	FOREIGN KEY (idSach) REFERENCES dbo.Sach(id),
	FOREIGN KEY (idBaiBao) REFERENCES dbo.Bai_bao(id)
)
GO

-- NGHIEN CUU TABLE
CREATE TABLE Nghien_cuu
(
	id INT IDENTITY PRIMARY KEY,
	soTrangToiDa INT NOT NULL,
	idBaiBao INT NOT NULL

	FOREIGN KEY (idBaiBao) REFERENCES dbo.Bai_bao(id)
)
GO

-- TONG QUAN TABLE
CREATE TABLE Tong_quan
(
	id INT IDENTITY PRIMARY KEY,
	soTrangToiDa INT NOT NULL,
	idBaiBao INT NOT NULL

	FOREIGN KEY (idBaiBao) REFERENCES dbo.Bai_bao(id)
)
GO

-- GHI CHU TABLE
CREATE TABLE Ghi_chu
(
	id INT IDENTITY PRIMARY KEY,
	ghiChuChoTacGia NVARCHAR(500) NOT NULL,
	ghiChuChoBienTap NVARCHAR(500) NOT NULL

)
GO

-- TIEU CHI DANH GIA TABLE
CREATE TABLE Tieu_chi_danh_gia
(
	id INT IDENTITY PRIMARY KEY,
	ghiChuBBT NVARCHAR(1000),
	ngayTao DATE NOT NULL DEFAULT GETDATE(),
	noidung NVARCHAR(100) NOT NULL,
	bienTap INT

	FOREIGN KEY (bienTap) REFERENCES dbo.Ban_bien_tap(id)
)
GO

-- DANH GIA TABLE
CREATE TABLE Danh_gia_bai_Bao
(
	id INT IDENTITY PRIMARY KEY,
	ghiChuChoTacGia TEXT,
	ghiChuChoBBT TEXT,
	mucDanhGia NVARCHAR(1000) NOT NULL,
	diem NVARCHAR(10) NOT NULL,
	noiDung TEXT NOT NULL,
	tieuChiDanhGia INT NOT NULL,
	idPhanBienBaiBao INT NOT NULL

	FOREIGN KEY (idPhanBienBaiBao) REFERENCES dbo.Phan_bien_bai_bao(id),
	FOREIGN KEY (tieuChiDanhGia) REFERENCES dbo.Tieu_chi_danh_gia(id)
)
GO

SELECT * FROM dbo.Danh_gia

-- Phan bien Infor view
ALTER VIEW Phan_bien_Info AS
SELECT dbo.Nha_khoa_hoc.id, hoVaTen, diaChi, dienThoai, ngheNghiep, emailCaNhan, coQuan, trinhDo, ngayCongTac, chuyenMon, soLuongPhanBien, emailCoQuan 
FROM dbo.Nha_khoa_hoc JOIN dbo.Phan_bien ON dbo.Nha_khoa_hoc.id = dbo.Phan_bien.idNhaKhoaHoc
GO

SELECT * FROM dbo.Phan_bien_Info

-- Loai bai bao View
CREATE TABLE Loai_bai_bao 
(
	noidung NVARCHAR(50)
)
GO

CREATE TABLE Trang_thai_bai_bao
(
	trangthai NVARCHAR(50)
)
GO

CREATE TABLE Ket_qua_bai_bao
(
	ketqua NVARCHAR(50)
)
GO



------------------
---INSERT DATA----
------------------
INSERT INTO dbo.Sach (tenTacGia, tongSoTrang, nhaXuatBan, ISBN, tenSach) VALUES
('Steve McConnell', 960, 'Microsoft Press', 0735619670,'Clean Code'),
('Andrew Hunt', 320, 'Addison-Wesley Professional', 9780201616224, 'The Paragmatic Programmer'),
('Eric Freeman & Elisabeth Robson', 694, 'Addison-Wesley Professional', 9780596007126, 'Head First Design Patterns');
GO

INSERT INTO dbo.Bai_bao (tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomTat, fileBaiBao, loaiBaiBao, trangthai, ketQua)
VALUES ('A Hybrid Approach to Vietnamese Word Segmentation', 1, '2018-11-08', 'Word Segmentation, Vietnamese Word Segmentation', 'ASC147159', 'Abstract�Word segmentation is the very first task for Vietnamese language processing. Word-segmented text is the input of almost other NLP tasks. This task faces some challenges due to specific characteristics of the language. As in many other Asian languages such as Japanese, Korean and Chinese, white spaces in Vietnamese are not always used as word separators and a word may contain one or more syllables', 'https://www.researchgate.net/profile/Tuan-Phong-Nguyen/publication/311980397_A_hybrid_approach_to_Vi', 'Nghien-cuu', 'Da-dang', 'Acceptance'),
('Nghien cuu su chap nhan sach dien tu - Ebook cua sinh vien tai Viet Nam', 2, '2019-07-09', 'Ebook, Internet, Sach dien tu, sach in, doc sach dien tu, loi ich sach dien tu', NULL, 'M?c ?�ch c?a nghi�n c?u n�y l� kh�m ph� t�nh h�nh s? d?ng s�ch ?i?n t? c?a sinh vi�n t?i Vi?t Nam. Hai ph??ng ph�p nghi�n c?u ???c s? d?ng ch�nh l� nghi�n c?u t�i li?u v� ?i?u tra kh?o s�t. ??i t??ng ???c kh?o s�t l� c�c sinh vi�n ?ang h?c t?p tr�n ??a b�n th�nh ph? H� N?i n?m h?c 2018-2019.', 'https://hvnh.edu.vn/medias/tapchi/vi/05.2019/system/archivedate/B%C3%A0i%20c%E1%BB%A7a%20TS%20Ch%E1%', 'Nghien-cuu', 'Hoan-tat-phan-bien', 'Acceptance'),
('TONG QUAN VE UNG DUNG PHUONG PHAP PHAN TICH THU BAC TRONG QUAN LY CHUOI CUNG UNG', 3, '2020-11-01', 'Phuong phap phan tich thu bac, quan ly chuoi cung ung, tong quan', NULL, 'B�i b�o c�o gi?i thi?u t?ng quan v? vi?c ?ng d?ng ph??ng ph�p ph�n t�ch th? b?c (Analytic Hierarchy Process � AHP) trong qu?n l� chu?i cung ?ng.', 'https://d1wqtxts1xzle7.cloudfront.net/33293778/trongtruong_so21a_22-with-cover-page-v2.pdf?Expires=1', 'Tong-quan', 'Phan-bien', NULL),
('Phan bien Sach Clean Code', 4, '2021-05-01', 'Clean Code', NULL, 'Even bad code can function. But if code isn�t clean, it can bring a development organization to its knees. Every year, countless hours and significant resources are lost because of poorly written code. But it doesn�t have to be that way', 'https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882', 'Phan-bien-sach', 'Chua-phan-cong', NULL),
('Phan bien Sach The Paragmatic Programmer', 5, '2021-05-15', 'The praragmatic programmer', NULL, 'Programmers are craftspeople trained to use a certain set of tools (editors, object managers, version trackers) to generate a certain kind of product (programs) that will operate in some environment (operating systems on hardware assemblies)', 'https://www.amazon.com/Pragmatic-Programmer-Journeyman-Master/dp/020161622X/ref=sr_1_2?crid=3OMSWCSCP1148&keywords=the+pragmatic+programmer&qid=1637467635&s=books&sprefix=The+paragmatic+pro%2Cstripbooks-intl-ship%2C398&sr=1-2', 'Phan-bien-sach', 'Chua-phan-cong', NULL),
('Phan bien Sach Head First Design Patterns', 6, '2021-09-28', 'Design patterns', NULL, 'Eric Freeman recently ended nearly a decade as a media company executive, having held the position of CTO of Disney Online & Disney.com at The Walt Disney Company. Eric is now devoting his time to WickedlySmart.com and lives with his wife and young daughter in Austin, TX. He holds a Ph.D. in Computer Science from Yale University.', 'https://www.amazon.com/Head-First-Design-Patterns-Brain-Friendly/dp/0596007124/ref=sr_1_2?crid=25JH8FE3Z01CK&keywords=head+first+design+patterns&qid=1637467715&s=books&sprefix=head+first+design%2Cstripbooks-intl-ship%2C377&sr=1-2', 'Phan-bien-sach', 'Chua-phan-cong', NULL);
GO

INSERT INTO dbo.Phan_bien_sach (idSach, idBaiBao, soTrangToiDa) VALUES
(1, 4, 3), (2, 5, 5), (3, 6, 6);

INSERT INTO dbo.Nha_khoa_hoc (hoVaTen, dienThoai, diaChi, ngheNghiep, emailCaNhan, coQuan)
VALUES ('Ha_Quang_Thuy', '0243754704', '144 Xuan Thuy Str, Cau Giay Dist, Ha Noi, Viet Nam', 'Lecturer', 'quangthuy@gmail.com', 'VNU - University of Engineering and Technology'),
('Le_Anh_Cuong', '0243577233', '01 Ton That Thuyet, Cau Giay, Ha Noi, Viet Nam', 'Lecturer', 'anhcuong@gmail.com', 'Ton Duc Thang University'),
('Chu_Ba_Quyet', '0456456456', 'Ha Noi, Viet Nam', 'Lecturer', 'baquyet@gmail.com', 'Thuong Mai University'),
('Hoang_Cao_Cuong', '0789789789', 'Ha Noi, Viet Nam', 'Lecturer', 'caocuong@gmail.com', 'Thuong Mai University'),
('Nguyen_Lan_Trung', '0147147147', 'Quan 8, HCM, Viet Nam', 'Lecturer', 'lantrung@gmail.com', 'VNU - University of Languages & International Studies'),
('Ly_Van_Quang', '0258258258', 'Quan 7', 'Ky Su', 'vangquang@gmail.com', 'Sino Pacific'),
('Pham_Van_Giang', '0369369369', 'Tan Binh', 'Nghien Cuu Sinh', 'vangiang@gmail.com', 'Dai Hoc Bach Khoa'),
('Nguyen_Tuan_Phong', '0245698734', '144 Xuan Thuy Str, Cau Giay Dist, Hanoi, Vietnam', 'Lecturer', 'tuanphong@gmail.com', 'VNU - University of Engineering and Technology'),
('Ngo Cong Hiep', '0937705040', '312 Lac Long Quan, Quan 11, TP HCM', 'Bien Tap Vien', 'hiepngo@gmail.com','Tap chi Nhom 6');

GO


INSERT INTO dbo.Phan_bien (trinhDo, ngayCongTac, chuyenMon, soLuongPhanBien, emailCoQuan, idNhaKhoaHoc)
VALUES ('Ph.D', '2018-01-11', 'Department of Database and Information Systems', 1, 'tuanphong.phanbien@nhom6.com', 8),
('Assoc. Prof. Ph.D', '2019-07-17', 'The Data Science and Knowledge Technology Laborato', 1, 'quangthuy.phanbien@nhom6.com', 1),
('Ph.D', '2020-05-18', 'Information Technology', 5, 'anhcuong.phanbien@nhom6.com', 2)

GO

INSERT INTO dbo.Bai_bao_Tac_gia (idNhaKhoaHoc, idBaiBao) VALUES
(2,1),(8,2),(7,2),(6,2),(6,3),(3,3),(4,3), (5,4), (7,4), (4,5), (6,5), (7,6), (1,6);
GO

INSERT INTO dbo.Phan_bien_bai_bao (idBaiBao, idPhanBien) VALUES
(1, 1), (1, 3), (2, 2), (2, 3), (1,3), (2,3), (3,3);
GO


INSERT INTO dbo.Ghi_chu (ghiChuChoTacGia, ghiChuChoBienTap) VALUES
('Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 1', 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 1'),
('Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 3', 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 3');
GO


INSERT INTO dbo.Ban_bien_tap (hoVaTen) VALUES ('Ngo Cong Hiep'), ('Tran Viet Trung');
GO

INSERT INTO dbo.Tieu_chi_danh_gia (ghiChuBBT, ngayTao, noidung, bienTap)
VALUES ('Tieu chi danh gia 1', '2021-11-20', 'Noi dung', 1),
('Tieu chi danh gia 2', '2021-11-20', 'Hinh thuc', 1),
('Tieu chi danh gia 3', '2021-11-20', 'Phuong phap nghien cuu', 2);
GO

INSERT INTO dbo.Danh_gia (idGhiChu, idPhanBienBaiBao, noiDung, tieuChiDanhGia, diem, mucDanhGia) VALUES
(1, 1, 'Day la noi dung phan bien ve mat noi dung cua phan bien 1 ve bai bao 1', 1, '8', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 1 theo tieu chi 1'),
(1, 1, 'Day la noi dung phan bien ve mat noi dung cua phan bien 1 ve bai bao 1', 3, '7', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 1 theo tieu chi 3'),
(2, 2, 'Day la noi dung phan bien ve mat noi dung cua phan bien 3 ve bai bao 1', 2, '7', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 3 theo tieu chi 2');
GO

INSERT INTO dbo.Danh_gia_bai_Bao (ghiChuChoTacGia, ghiChuChoBBT, idPhanBienBaiBao, noiDung, tieuChiDanhGia, diem, mucDanhGia) VALUES
('Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 1', 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 1', 1, 'Day la noi dung phan bien ve mat noi dung cua phan bien 1 ve bai bao 1', 1, '8', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 1 theo tieu chi 1'),
('Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 1', 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 1', 1, 'Day la noi dung phan bien ve mat noi dung cua phan bien 1 ve bai bao 1', 3, '7', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 1 theo tieu chi 3'),
('Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 3', 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 3', 2, 'Day la noi dung phan bien ve mat noi dung cua phan bien 3 ve bai bao 1', 2, '7', 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 3 theo tieu chi 2');
GO

INSERT INTO dbo.Tong_quan (soTrangToiDa, idBaiBao)
VALUES (11, 3)
GO

INSERT INTO dbo.Nghien_cuu (soTrangToiDa, idBaiBao)
VALUES (6, 1),
(13,2)

GO

INSERT INTO dbo.Account (username, displayName, password, idNhaKhoaHoc, role)
VALUES ('hiep', 'Ngo Cong Hiep', '123456', 9, 0),
('tuanphong', 'Nguyen Tuan Phong', '123456', 8, 1),
('quangthuy', 'Ha Quang Thuy', '123456', 1, 1),
('vangiang', 'Pham Van Giang', '123456', 7, 2),
('lantrung', 'Nguyen Lan Trung', '123456', 5, 2),
('cuong', 'Le Anh Cuong', '123456', 2, 1)

GO


SELECT * FROM dbo.Account

INSERT INTO dbo.Loai_bai_bao (noidung) VALUES (NULL), ('Nghien-cuu'), ('Tong-quan'), ('Phan-bien-sach');

INSERT INTO dbo.Trang_thai_bai_bao (trangthai) VALUES (NULL), ('Chua-phan-cong'), ('Phan-bien'), ('Phan-hoi-phan-bien'), ('Hoan-tat-phan-bien'), ('Xuat-ban'), ('Da-dang');


INSERT INTO dbo.Ket_qua_bai_bao (ketqua) VALUES (NULL), ('Rejection'), ('Minor Revision'), ('Major Revision'), ('Acceptance');

------------------
---PROCEDURE----
------------------

CREATE PROC USP_Login
@username nvarchar(100), @password nvarchar(100) 
AS
BEGIN
	SELECT * FROM dbo.Account WHERE username = @username AND password = @password
END
GO

CREATE PROC ART_GetAllArticle
AS SELECT * FROM dbo.Bai_bao
GO

ALTER PROC ART_GetArticleByAuthorId
@idNhaKhoaHoc int
AS
BEGIN
	SELECT dbo.Bai_bao.id, 
	dbo.Bai_bao.tieuDe, 
	dbo.Bai_bao.maSo, 
	dbo.Bai_bao.ngayGui, 
	dbo.Bai_bao.cacTuKhoa, 
	dbo.Bai_bao.DOI, 
	dbo.Bai_bao.tomTat, 
	dbo.Bai_bao.fileBaiBao, 
	dbo.Bai_bao.loaiBaiBao, 
	dbo.Bai_bao.trangthai, 
	dbo.Bai_bao.ketQua 
	FROM dbo.Bai_bao
	INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
	WHERE dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = @idNhaKhoaHoc
END
GO

EXEC ART_GetArticleByAuthorId @idNhaKhoaHoc = 2
SELECT * FROM dbo.Bai_bao

CREATE PROC ART_SetResult
@id int, @trangThai nvarchar(50)
AS
BEGIN
	UPDATE dbo.Bai_bao SET trangThai = @trangThai
	WHERE id = @id
END
GO

CREATE PROC ART_UpdateArticleInfo
@id int, @tieuDe nvarchar(100), @tomTat nvarchar(100), @cacTuKhoa nvarchar(100)
AS
BEGIN
	UPDATE dbo.Bai_bao
	SET tieuDe = @tieuDe, tomTat = @tomTat, cacTuKhoa = @cacTuKhoa
	WHERE id = @id
END
GO

CREATE PROC SetArticleAuthor
@tieuDe nvarchar(100), @hoVaTen nvarchar(50)
AS
BEGIN
	DECLARE @idBaiBao int
	SELECT @idBaiBao = id FROM dbo.Bai_bao WHERE tieuDe = @tieuDe
	DECLARE @idNhaKhoaHoc int
	SELECT @idNhaKhoaHoc = id FROM dbo.Nha_khoa_hoc WHERE hoVaTen = @hoVaTen

	INSERT INTO dbo.Bai_bao_Tac_gia (idBaiBao, idNhaKhoaHoc) VALUES (@idBaiBao, @idNhaKhoaHoc)

END
GO

CREATE PROC GetAllArticleAuthor
AS
BEGIN
	SELECT tieuDe, hoVaTen FROM dbo.Bai_bao INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
	INNER JOIN dbo.Nha_khoa_hoc ON dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = dbo.Nha_khoa_hoc.id
END
GO

CREATE PROC AUT_GetAuthorByArticleID
@idBaiBao int
AS
BEGIN
	SELECT dbo.Nha_khoa_hoc.id, 
	dbo.Nha_khoa_hoc.hoVaTen, 
	dbo.Nha_khoa_hoc.dienThoai, 
	dbo.Nha_khoa_hoc.diaChi, 
	dbo.Nha_khoa_hoc.ngheNghiep, 
	dbo.Nha_khoa_hoc.emailCaNhan, 
	dbo.Nha_khoa_hoc.coQuan
	FROM dbo.Nha_khoa_hoc
	INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Nha_khoa_hoc.id = dbo.Bai_bao_Tac_gia.idNhaKhoaHoc
	WHERE dbo.Bai_bao_Tac_gia.idBaiBao = @idBaiBao
END
GO

EXEC AUT_GetAuthorByArticleID @idBaiBao = 1

CREATE PROC RV_UpdateInfo
@id int, 
@hoVaTen nvarchar(50), 
@diaChi nvarchar(100), 
@dienThoai nvarchar(10), 
@ngheNghiep nvarchar(50), 
@emailCaNhan nvarchar(50), 
@coQuan nvarchar(50), 
@trinhDo nvarchar(50), 
@chuyenMon nvarchar(50), 
@emailCoQuan nvarchar(50)
AS
BEGIN
	UPDATE dbo.Nha_khoa_hoc
	SET hoVaTen = @hoVaTen,
	diaChi = @diaChi,
	dienThoai = @dienThoai,
	ngheNghiep = @ngheNghiep,
	emailCaNhan = @emailCaNhan,
	coQuan = @coQuan
	WHERE id = @id

	UPDATE dbo.Phan_bien
	SET trinhDo = @trinhDo, chuyenMon = @chuyenMon
	WHERE emailCoQuan = @emailCoQuan
END
GO

--CREATE PROC RV_AddDivision
--@idPhanBien int, @idBaiBao int
--AS
--BEGIN 
	--INSERT INTO dbo.Phan_bien_bai_bao (idBaiBao, idPhanBien) VALUES ()
--END
GO

--i.5 - i-12
ALTER PROC FilterByArticleInfo
@loaiBaiBao NVARCHAR(50) = NULL, @trangThai NVARCHAR(50) = NULL, @ngay DATE = NULL, @author NVARCHAR(50) = NULL
AS
BEGIN
	IF (@ngay IS NULL AND @author IS NULL)
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE loaiBaiBao = @loaiBaiBao AND trangthai = @trangThai
	END
	ELSE IF (@author IS NULL)
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE loaiBaiBao = @loaiBaiBao AND trangthai = @trangThai AND (ngayGui BETWEEN @ngay AND GETDATE())
	END
	ELSE IF (@loaiBaiBao IS NULL AND @ngay IS NULL)
	BEGIN
		SELECT dbo.Bai_bao.id, 
		tieuDe, 
		maSo, 
		ngayGui, 
		cacTuKhoa, 
		DOI, 
		tomTat, 
		fileBaiBao, 
		loaiBaiBao, 
		trangthai, 
		ketQua
		FROM dbo.Bai_bao 
		JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao 
		JOIN dbo.Nha_khoa_hoc ON dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = dbo.Nha_khoa_hoc.id 
		WHERE hoVaTen = @author AND trangthai = @trangThai
	END
END
GO

CREATE PROC FilterByArticleInfo1
@trangThai NVARCHAR(50), @author NVARCHAR(50)
AS
BEGIN
	SELECT dbo.Bai_bao.id, 
		tieuDe, 
		maSo, 
		ngayGui, 
		cacTuKhoa, 
		DOI, 
		tomTat, 
		fileBaiBao, 
		loaiBaiBao, 
		trangthai, 
		ketQua
		FROM dbo.Bai_bao 
		JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao 
		JOIN dbo.Nha_khoa_hoc ON dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = dbo.Nha_khoa_hoc.id 
		WHERE hoVaTen = @author AND trangthai = @trangThai
END
GO

EXEC FilterByArticleInfo1 @trangThai = 'Xuat-ban', @author = 'Pham_Van_Giang'

SELECT * FROM dbo.Bai_bao

--i.5 -> i.9
ALTER PROC AUT_FilterArticle
@trangThai nvarchar(50) = NULL, @ketQua nvarchar(50) = NULL, @ngay DATE = NULL
AS
BEGIN 
	IF (@ketQua IS NULL AND @ngay IS NULL)
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE trangthai = @trangThai
	END
	ELSE IF (@trangThai IS NULL AND @ngay IS NULL)
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE ketQua = @ketQua
	END
	ELSE IF (@trangThai IS NULL AND @ketQua IS NULL)
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE (ngayGui BETWEEN @ngay AND GETDATE())
	END
	ELSE IF (@ketQua IS NULL) 
	BEGIN
		SELECT * FROM dbo.Bai_bao WHERE trangthai = @trangThai AND (ngayGui BETWEEN @ngay AND GETDATE())
	END
END
GO

EXEC AUT_FilterArticle @ngay = '5/5/2019 12:00:00AM'
EXEC AUT_FilterArticle @ketQua = 'Acceptance'

EXEC FilterByArticleInfo @loaiBaiBao = 'Nghien-cuu', @trangThai = 'Xuat-ban'
EXEC FilterByArticleInfo @loaiBaiBao = 'Nghien-cuu', @trangThai = 'Da-dang', @ngay = NULL, @author = NULL
EXEC FilterByArticleInfo @trangThai = 'Da-dang', @author = 'Le_Anh_Cuong'

--i.5, i.6
CREATE PROC ART_GetArticleByTypeAndStatus
@loaiBaiBao nvarchar(50), @trangThai nvarchar(50)
AS 
BEGIN 
	SELECT * FROM dbo.Bai_bao WHERE loaiBaiBao = @loaiBaiBao AND trangthai = @trangThai
END
GO

--i.7
CREATE PROC ART_GetArticleByTypeAndDate
@loaiBaiBao nvarchar(50), @ngay DATE
AS
BEGIN
	SELECT * 
	FROM dbo.Bai_bao 
	WHERE loaiBaiBao = @loaiBaiBao AND 
		trangthai = 'Da dang' AND 
		(ngayGui BETWEEN @ngay AND GETDATE())
END
GO

--i.8, i.9
CREATE PROC ART_GetArticleListByAuthorAndStatus
@idTacGia int, @trangthai nvarchar(50)
AS
BEGIN
	SELECT * 
	FROM dbo.Bai_bao INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
	WHERE trangthai = @trangthai AND idNhaKhoaHoc = @idTacGia
END
GO

--i.10,11,12
ALTER PROC ART_GetNumberOfArticleByStatus
@trangthai nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.Bai_bao
	WHERE trangthai = @trangthai
END
GO

--iii.6
CREATE PROC ART_GetArticleListInYear
@year int
AS 
BEGIN
	SELECT * FROM dbo.Bai_bao
	WHERE YEAR(ngayGui) = @year
END
GO

--iii.7
CREATE PROC ART_GetArticlePostedListInYear
@year int, @trangthai nvarchar(50)
AS 
BEGIN
	SELECT * FROM dbo.Bai_bao
	WHERE YEAR(ngayGui) = @year AND trangthai = @trangthai
END
GO

--iii.8
CREATE PROC ART_GetArticleListByStatus
@trangthai nvarchar(50)
AS
BEGIN
	SELECT * FROM dbo.Bai_bao
	WHERE trangthai = @trangthai
END
GO

--iii.9
CREATE PROC ART_GetRejectionArticle
AS
BEGIN
	SELECT * FROM dbo.Bai_bao WHERE ketQua = 'rejection'
END
GO


--iii.10,11,12
ALTER PROC ART_GetSumArticlePosted
@ngay DATE = NULL, @loaibaibao nvarchar(50) = NULL, @trangThai nvarchar(50) = NULL
AS
BEGIN
	IF (@loaibaibao IS NULL AND @trangThai IS NULL)
	BEGIN
		SELECT COUNT(*) FROM dbo.Bai_bao
		WHERE ngayGui BETWEEN @ngay AND GETDATE()
	END
	ELSE
	BEGIN
		SELECT COUNT(*) AS Quantity, YEAR(ngayGui) AS Year FROM dbo.Bai_bao
		WHERE (ngayGui BETWEEN @ngay AND GETDATE()) AND loaiBaiBao = @loaibaibao AND trangthai = @trangThai
		GROUP BY YEAR(ngayGui)
	END
	
END
GO

CREATE PROC ART_GetSumArticleReviewed
@idNhaKhoaHoc int, @ngay DATE 
AS
BEGIN
	DECLARE @idPhanBien int
	SELECT @idPhanBien = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT COUNT(*) AS Quantity, YEAR(ngayGui) AS Year FROM dbo.Bai_bao
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE (ngayGui BETWEEN @ngay AND GETDATE()) AND idPhanBien = @idPhanBien AND (trangthai = 'Hoan-tat-phan-bien' OR trangthai = 'Xuat-ban' OR trangthai = 'Da-dang')
	GROUP BY YEAR(ngayGui)

	
END
GO


CREATE PROC ART_get3ArticlesResult
@idNhaKhoaHoc int, @ketQua nvarchar(50)
AS
BEGIN
	DECLARE @idPhanBien int
	SELECT @idPhanBien = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT TOP 3 dbo.Bai_bao.id, 
	tieuDe,
	maSo, 
	ngayGui, 
	cacTuKhoa, 
	DOI, 
	tomTat, 
	fileBaiBao, 
	loaiBaiBao, 
	trangthai, 
	ketQua  
	FROM dbo.Bai_bao
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE ketQua = @ketQua AND idPhanBien = @idPhanBien

END
GO

EXEC ART_get3ArticlesResult @idNhaKhoaHoc = 2, @ketQua = 'Acceptance'

ALTER PROC RV_GetArticleDivisioned
@idNhaKhoaHoc int
AS
BEGIN 
	DECLARE @idPhanBien int
	SELECT @idPhanBien = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT dbo.Bai_bao.id, tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomTat, fileBaiBao, loaiBaiBao, trangthai, ketQua 
	FROM dbo.Bai_bao
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE idPhanBien = @idPhanBien AND dbo.Bai_bao.trangthai = 'Phan-bien'
END
GO
--PROC CAP NHAT PHAN BIEN CHO BAI BAO
CREATE PROC RV_SetReviewForArticle
@idBaiBao int, 
@idNhaKhoaHoc int, 
@ghiChuTacGia text, 
@ghiChuBBT text, 
@diem nvarchar(10), 
@noiDung text, 
@mucDanhGia nvarchar(1000), 
@tieuChiDanhGia nvarchar(50)
AS
BEGIN
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc
	DECLARE @idPBBB int
	SELECT @idPBBB = id FROM dbo.Phan_bien_bai_bao WHERE idBaiBao = @idBaiBao AND idPhanBien = @idPB
	DECLARE @tcdg int
	SELECT @tcdg = id FROM dbo.Tieu_chi_danh_gia WHERE noidung = @tieuChiDanhGia

	INSERT INTO dbo.Danh_gia_bai_Bao (ghiChuChoBBT, ghiChuChoTacGia, idPhanBienBaiBao, noiDung, tieuChiDanhGia, mucDanhGia, diem) VALUES
	(@ghiChuBBT, @ghiChuTacGia, @idPBBB, @noiDung, @tcdg, @mucDanhGia, @diem)

END
GO

-- PROC LAY RA DANH SACH BAI BAO CUA PHAN BIEN
CREATE PROC ART_GetArticleListByIdNKH
@idNhaKhoaHoc int
AS
BEGIN
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT dbo.Bai_bao.id, 
	tieuDe, 
	maSo, 
	ngayGui, 
	cacTuKhoa, 
	DOI, 
	tomTat, 
	fileBaiBao, 
	loaiBaiBao, 
	trangthai, 
	ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE idPhanBien = @idPB
END
GO

-- FILTER -- LAY DANH SACH BAI BAO CAC LOAI RV DANG PHAN BIEN
ALTER PROC ART_GetArticleListByArtType
@idNhaKhoaHoc int, @loaiBaiBao nvarchar(50)
AS
BEGIN
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT dbo.Bai_bao.id, 
	tieuDe,
	maSo, 
	ngayGui,
	cacTuKhoa, 
	DOI, 
	tomTat, 
	fileBaiBao, 
	loaiBaiBao, 
	trangthai, 
	ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE idPhanBien = @idPB AND loaiBaiBao = @loaiBaiBao AND trangthai = 'Phan-bien'
END
GO

SELECT * FROM dbo.Phan_bien_bai_bao

SELECT * FROM dbo.Bai_bao_Tac_gia

CREATE PROC ART_GetArticleListByArtTypeAndTime
@idNhaKhoaHoc int, @loaiBaiBao nvarchar(50), @ngay DATE
AS
BEGIN
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT dbo.Bai_bao.id, tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomTat, fileBaiBao, loaiBaiBao, trangthai, ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE idPhanBien = @idPB AND 
	loaiBaiBao = @loaiBaiBao AND 
	(trangthai = 'Da-dang' OR trangthai = 'Hoan-tat-phan-bien' OR trangthai = 'Xuat-ban') AND 
	(ngayGui BETWEEN @ngay AND GETDATE())
END
GO

ALTER PROC ART_GetArticleListByAuthor
@idNhaKhoaHoc int, @tenTacGia nvarchar(50)
AS
BEGIN 
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	DECLARE @idTacGia int
	SELECT @idTacGia = id FROM dbo.Nha_khoa_hoc WHERE hoVaTen = @tenTacGia

	SELECT dbo.Bai_bao.id, tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomTat, fileBaiBao, loaiBaiBao, trangthai, ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
	WHERE dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = @idTacGia AND 
	dbo.Phan_bien_bai_bao.idPhanBien = @idPB AND 
	(trangthai = 'Phan-bien' OR trangthai = 'Phan-hoi-phan-bien')
END
GO

CREATE PROC ART_GetArticleListByAuthorAndTime
@idNhaKhoaHoc int, @tenTacGia nvarchar(50), @ngay DATE
AS
BEGIN 
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	DECLARE @idTacGia int
	SELECT @idTacGia = id FROM dbo.Nha_khoa_hoc WHERE hoVaTen = @tenTacGia

	SELECT dbo.Bai_bao.id, 
	tieuDe, 
	maSo, 
	ngayGui, 
	cacTuKhoa,
	DOI, 
	tomTat, 
	fileBaiBao, 
	loaiBaiBao, 
	trangthai,
	ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
	WHERE dbo.Bai_bao_Tac_gia.idNhaKhoaHoc = @idTacGia AND 
	dbo.Phan_bien_bai_bao.idPhanBien = @idPB AND 
	(trangthai = 'Hoan-tat-phan-bien' OR trangthai = 'Da-dang' OR trangthai = 'Xuat-ban') AND 
	(ngayGui BETWEEN @ngay AND GETDATE())
END
GO

ALTER PROC AUT_GetArticleResultInYear
@idNhaKhoaHoc int, @ngay DATE
AS
BEGIN
	DECLARE @idPB int
	SELECT @idPB = id FROM dbo.Phan_bien WHERE idNhaKhoaHoc = @idNhaKhoaHoc

	SELECT dbo.Bai_bao.id, 
	tieuDe,
	maSo, 
	ngayGui, 
	cacTuKhoa, 
	DOI, 
	tomTat, 
	fileBaiBao, 
	loaiBaiBao, 
	trangthai, 
	ketQua
	FROM dbo.Bai_bao 
	INNER JOIN dbo.Phan_bien_bai_bao ON dbo.Bai_bao.id = dbo.Phan_bien_bai_bao.idBaiBao
	WHERE idPhanBien = @idPB AND (ngayGui BETWEEN @ngay AND GETDATE())
END
GO

EXEC AUT_GetArticleResultInYear @idNhaKhoaHoc = 2, @ngay = '2019-11-24'

EXEC ART_GetArticleListByAuthorAndTime @idNhaKhoaHoc = 2, @tenTacGia = 'Pham_Van_Giang', @ngay = '2015-05-05'


EXEC RV_SetReviewForArticle @idBaiBao = 1, @idNhaKhoaHoc = 2, @ghiChuTacGia = 'Day la ghi chu phan bien bai bao so 1 cho tac gia cua phan bien 3', @ghiChuBBT = 'Day la ghi chu bai bao so 1 cho ban bien tap cua phan bien 3', @diem = '8', @noiDung = 'Day la noi dung phan bien ve mat noi dung cua phan bien 3 ve bai bao 1', @mucDanhGia = 'Day se la mo ta tai sao lai dat muc diem la 8 cua phan bien 3 theo tieu chi 2', @tieuChiDanhGia = 'Noi dung'

EXEC RV_GetArticleDivisioned @idNhaKhoaHoc = 2


SELECT * FROM dbo.Phan_bien_bai_bao


EXEC ART_GetSumArticlePosted @loaiBaiBao = 'Nghien-cuu', @ngay = '2015-11-07', @trangThai = 'Da-dang';
GO

EXEC ART_SetResult @id = 1, @trangThai = 'Phan bien';
GO

---------------
-----TRIGGER------
---------------
CREATE TRIGGER Division
ON dbo.Phan_bien_bai_bao FOR INSERT
AS
BEGIN
	DECLARE @idPhanBien INT
	SELECT @idPhanBien = idPhanBien FROM inserted
	DECLARE @idBaiBao INT
	SELECT @idBaiBao = idBaiBao FROM inserted
	
	UPDATE dbo.Phan_bien
	SET soLuongPhanBien = soLuongPhanBien + 1
	WHERE id = @idPhanBien

	UPDATE dbo.Bai_bao
	SET trangthai = 'Phan bien'
	WHERE dbo.Bai_bao.id = @idBaiBao
END
GO



SELECT * FROM dbo.Phan_bien_Info



---------------
-----TEST------

SELECT * FROM dbo.Bai_bao WHERE ngayGui BETWEEN '2021-11-01' AND GETDATE();
SELECT * FROM dbo.Bai_bao_Tac_gia;

SELECT * 
FROM dbo.Bai_bao INNER JOIN dbo.Bai_bao_Tac_gia ON dbo.Bai_bao.id = dbo.Bai_bao_Tac_gia.idBaiBao
WHERE trangthai = 'Phan bien' AND idNhaKhoaHoc = 3;



SELECT * FROM dbo.Bai_bao WHERE ngayGui BETWEEN '5/5/2015 12:00:00 AM' AND GETDATE()


UPDATE dbo.Nha_khoa_hoc
SET dienThoai = '0245698735'
WHERE id = 8

SELECT * FROM dbo.Phan_bien_Info

SELECT * FROM dbo.Nha_khoa_hoc

SELECT * FROM dbo.Bai_bao

SELECT * FROM dbo.Phan_bien_bai_bao

