using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class YearDAO
    {
        private static YearDAO instance;

        public static YearDAO Instance
        {
            get { if (instance == null) instance = new YearDAO(); return instance; }
            private set => instance = value;
        }

        private YearDAO() { }

        public List<YearDTO> getAverageNumberPerYear(string date, string loaiBaiBao, string trangThai)
        {
            DateTime ngay = DateTime.Parse(date);

            List<YearDTO> qty = new List<YearDTO>();

            string query = "ART_GetSumArticlePosted @ngay , @loaiBaiBao , @trangThai";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { ngay, loaiBaiBao, trangThai });

            foreach (DataRow item in data.Rows)
            {
                YearDTO article = new YearDTO(item);

                qty.Add(article);
            }

            return qty;
        }

        public List<YearDTO> getSumArticleReviewed(int idNhaKhoaHoc, string date)
        {
            DateTime ngay = DateTime.Parse(date);

            List<YearDTO> qty = new List<YearDTO>();

            string query = "ART_GetSumArticleReviewed @idNhaKhoaHoc , @ngay";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, ngay});

            foreach (DataRow item in data.Rows)
            {
                YearDTO article = new YearDTO(item);

                qty.Add(article);
            }

            return qty;
        }
    }
}
