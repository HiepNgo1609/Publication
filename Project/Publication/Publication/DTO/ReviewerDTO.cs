using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class ReviewerDTO
    {
        private int iD;

        public int ID { get => iD; set => iD = value; }

        private string hoVaTen;
        public string HoVaTen { get => hoVaTen; set => hoVaTen = value; }

        private string diaChi;
        public string DiaChi { get => diaChi; set => diaChi = value; }

        private string dienThoai;
        public string DienThoai { get => dienThoai; set => dienThoai = value; }

        private string ngheNghiep;
        public string NgheNghiep { get => ngheNghiep; set => ngheNghiep = value; }

        private string emailCaNhan;
        public string EmailCaNhan { get => emailCaNhan; set => emailCaNhan = value; }

        private string coQuan;
        public string CoQuan { get => coQuan; set => coQuan = value; }

        private string trinhDo;
        public string TrinhDo { get => trinhDo; set => trinhDo = value; }

        private DateTime? ngayCongTac;
        public DateTime? NgayCongTac { get => ngayCongTac; set => ngayCongTac = value; }

        private string chuyenMon;
        public string ChuyenMon { get => chuyenMon; set => chuyenMon = value; }

        private int soLuongPhanBien;
        public int SoLuongPhanBien { get => soLuongPhanBien; set => soLuongPhanBien = value; }

        private string emailCoQuan;
        public string EmailCoQuan { get => emailCoQuan; set => emailCoQuan = value; }

        private int idNhaKhoaHoc;
        public int IdNhaKhoaHoc { get => idNhaKhoaHoc; set => idNhaKhoaHoc = value; }

        public ReviewerDTO(int id, string hovaten, string diachi, string dienthoai, string nghenghiep, string emailCaNhan, string coquan, string trinhdo, DateTime? ngaycongtac, string chuyenmon, int soluongphanbien, string emailcoquan, int idNhaKhoaHoc)
        {
            this.ID = id;
            this.HoVaTen = hovaten;
            this.DiaChi = diachi;
            this.DienThoai = dienthoai;
            this.NgheNghiep = nghenghiep;
            this.EmailCaNhan = emailCaNhan;
            this.CoQuan = coquan;
            this.TrinhDo = trinhdo;
            this.NgayCongTac = ngaycongtac;
            this.ChuyenMon = chuyenmon;
            this.soLuongPhanBien = soluongphanbien;
            this.EmailCoQuan = emailcoquan;
        }

        public ReviewerDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.HoVaTen = row["hoVaTen"].ToString();
            this.DiaChi = row["diaChi"].ToString();
            this.DienThoai = row["dienThoai"].ToString();
            this.NgheNghiep = row["ngheNghiep"].ToString();
            this.EmailCaNhan = row["emailCaNhan"].ToString();
            this.CoQuan = row["coQuan"].ToString();
            this.TrinhDo = row["trinhDo"].ToString();
            this.NgayCongTac = (DateTime?)row["ngayCongTac"];
            this.ChuyenMon = row["chuyenMon"].ToString();
            this.SoLuongPhanBien = (int)row["soLuongPhanBien"];
            this.EmailCoQuan = row["emailCoQuan"].ToString();
        }

    }
}
