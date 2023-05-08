using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class AuthorDTO
    {
        private int iD;

        private string hoVaTen;

        private string dienThoai;

        private string diaChi;

        private string ngheNghiep;

        private string emailCaNhan;

        private string coQuan;

        public int ID { get => iD; set => iD = value; }
        public string HoVaTen { get => hoVaTen; set => hoVaTen = value; }
        public string DienThoai { get => dienThoai; set => dienThoai = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public string NgheNghiep { get => ngheNghiep; set => ngheNghiep = value; }
        public string EmailCaNhan { get => emailCaNhan; set => emailCaNhan = value; }
        public string CoQuan { get => coQuan; set => coQuan = value; }

        public AuthorDTO() { }

        public AuthorDTO(int id, string hoVaTen, string dienThoai, string diaChi, string ngheNghiep, string emailCaNhan, string coQuan)
        {
            this.ID = id;
            this.HoVaTen = hoVaTen;
            this.DienThoai = dienThoai;
            this.DiaChi = diaChi;
            this.NgheNghiep = ngheNghiep;
            this.EmailCaNhan = emailCaNhan;
            this.CoQuan = coQuan;
        }

        public AuthorDTO(DataRow row)
        {
            this.ID = (int)row["id"];
            this.HoVaTen = row["hoVaTen"].ToString();
            this.DienThoai = row["dienThoai"].ToString();
            this.DiaChi = row["diaChi"].ToString();
            this.NgheNghiep = row["ngheNghiep"].ToString();
            this.EmailCaNhan = row["emailCaNhan"].ToString();
            this.CoQuan = row["coQuan"].ToString();
        }
    }
}
