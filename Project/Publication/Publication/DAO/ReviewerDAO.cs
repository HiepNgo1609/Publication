using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class ReviewerDAO
    {
        private static ReviewerDAO instance;


        public static ReviewerDAO Instance
        {
            get { if (instance == null) instance = new ReviewerDAO(); return instance; }
            private set { instance = value; }
        }

        public ReviewerDAO() { }

        public List<ReviewerDTO> loadReviewerSelect()
        {
            List<ReviewerDTO> rwList = new List<ReviewerDTO>();

            string query = "SELECT * FROM dbo.Phan_bien_Info";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ReviewerDTO reviewer = new ReviewerDTO(item);

                rwList.Add(reviewer);
            }
            return rwList;
        }

        public ReviewerDTO GetReviewerByID(int id)
        {
            ReviewerDTO reviewer = null;

            string query = "SELECT * FROM dbo.Phan_bien_Info WHERE id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                reviewer = new ReviewerDTO(item);

                return reviewer;
            }

            return reviewer;
        }


        public bool updateReviewerInfo(int id, string hoVaTen, string diaChi, string dienThoai, string ngheNghiep, string emailCaNhan, string coQuan, string trinhDo, string chuyenMon, string emailCoQuan)
        {
            string query = "RV_UpdateInfo @id , @hoVaTen , @diaChi , @dienThoai , @ngheNghiep , @emailCaNhan , @coQuan , @trinhDo , @chuyenMon , @emailCoQuan";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, hoVaTen, diaChi, dienThoai, ngheNghiep, emailCaNhan, coQuan, trinhDo, chuyenMon, emailCoQuan });

            return result > 0;
        }

        public List<ArticleDTO> getArticleListByIdReview(int id)
        {
            List<ArticleDTO> artList = new List<ArticleDTO>();

            string query = "RV_GetArticleDivisioned @idNhaKhoaHoc";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id});

            foreach (DataRow item in data.Rows)
            {
                ArticleDTO art = new ArticleDTO(item);

                artList.Add(art);
            }
            return artList;
        }

        public bool SetReviewContent (int idBaiBao, int idNKH, string ghiChuTG, string ghiChuBBT, string diem, string noiDung, string mucDanhGia, string tieuChiDG)
        {
            string query = "RV_SetReviewForArticle @idBaiBao , @idNhaKhoaHoc , @ghiChuTacGia , @ghiChuBBT , @diem , @noiDung , @mucDanhGia , @tieuChiDanhGia ";
            
            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { idBaiBao, idNKH, ghiChuTG, ghiChuBBT, diem, noiDung, mucDanhGia, tieuChiDG });

            return result > 0;
        }
    }
}
