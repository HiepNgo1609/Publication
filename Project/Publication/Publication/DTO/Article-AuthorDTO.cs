using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class Article_AuthorDTO
    {
        private string hoVaTen;

        private string tieuDe;

        public string HoVaTen { get => hoVaTen; set => hoVaTen = value; }
        public string TieuDe { get => tieuDe; set => tieuDe = value; }

        public Article_AuthorDTO(string hoVaTen, string tieuDe)
        {
            this.HoVaTen = hoVaTen;
            this.TieuDe = tieuDe;
        }

        public Article_AuthorDTO() { }

        public Article_AuthorDTO(DataRow row)
        {
            this.HoVaTen = row["hoVaTen"].ToString();
            this.TieuDe = row["tieuDe"].ToString();
        }
    }
}
