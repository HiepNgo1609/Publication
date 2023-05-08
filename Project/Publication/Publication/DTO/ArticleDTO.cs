using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class ArticleDTO
    {
        private int iD;

        public int ID { get => iD; set => iD = value; }

        private string tieuDe;
        public string TieuDe { get => tieuDe; set => tieuDe = value; }

        private int maSo;
        public int MaSo { get => maSo; set => maSo = value; }

        private DateTime? ngayGui;
        public DateTime? NgayGui { get => ngayGui; set => ngayGui = value; }

        private string cacTuKhoa;
        public string CacTuKhoa { get => cacTuKhoa; set => cacTuKhoa = value; }

        private string dOI;
        public string DOI { get => dOI; set => dOI = value; }

        private string tomTat;
        public string TomTat { get => tomTat; set => tomTat = value; }

        private string fileBaiBao;
        public string FileBaiBao { get => fileBaiBao; set => fileBaiBao = value; }

        private string loaiBaiBao;
        public string LoaiBaiBao { get => loaiBaiBao; set => loaiBaiBao = value; }

        private string trangThai;
        public string TrangThai { get => trangThai; set => trangThai = value; }

        private string ketQua;
        public string KetQua { get => ketQua; set => ketQua = value; }

        public ArticleDTO(int id, string tieude, int maso, DateTime? ngaygui, string cactukhoa, string doi, string tomtat, string filebaibao, string loaibaibao, string trangthai, string ketqua)
        {
            this.ID = id;
            this.TieuDe = tieude;
            this.MaSo = maso;
            this.NgayGui = ngaygui;
            this.CacTuKhoa = cactukhoa;
            this.DOI = doi;
            this.TomTat = tomtat;
            this.FileBaiBao = filebaibao;
            this.LoaiBaiBao = loaibaibao;
            this.TrangThai = trangthai;
            this.KetQua = ketqua;
        }

        public ArticleDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.TieuDe = row["tieuDe"].ToString();
            this.MaSo = (int)row["maSo"];
            this.NgayGui = (DateTime?)row["ngayGui"];
            this.CacTuKhoa = row["cacTuKhoa"].ToString();
            this.DOI = row["DOI"].ToString();
            this.TomTat = row["tomTat"].ToString();
            this.FileBaiBao = row["fileBaiBao"].ToString();
            this.LoaiBaiBao = row["loaiBaiBao"].ToString();
            this.TrangThai = row["trangThai"].ToString();
            this.KetQua = row["ketQua"].ToString();
        }
    }
}
