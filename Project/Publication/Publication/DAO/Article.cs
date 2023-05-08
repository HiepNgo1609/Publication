using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class Article
    {
        private static Article instance;

        public static Article Instance 
        {
            get { if (instance == null) instance = new Article(); return Article.instance; } 
            private set => instance = value; 
        }

        private Article() { }

        public List<ArticleDTO> loadArticle()
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            DataTable data = DataProvider.Instance.ExecuteQuery("ART_GetAllArticle");

            foreach (DataRow item in data.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> loadArticleForReviewer(int idNhaKhoaHoc)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleListByIdNKH @idNhaKhoaHoc";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc});

            foreach (DataRow item in data.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> loadArticleByAuthor(int idNhaKhoaHoc)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleByAuthorId @idNhaKhoaHoc";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] {idNhaKhoaHoc});

            foreach (DataRow item in data.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;

        }

        public List<ArticleDTO> loadNullResultArticle()
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "SELECT * FROM dbo.Bai_bao WHERE ketQua IS NULL";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public ArticleDTO GetArticleByID(int id)
        {
            ArticleDTO article = null;

            string query = "SELECT * FROM dbo.Bai_bao WHERE id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                article = new ArticleDTO(item);

                return article;
            }

            return article;
        }

        public bool InsertArticle( string tieuDe, int maSo, DateTime? ngayGui, string cacTuKhoa, string tomtat, string fileBaiBao, string loaiBaiBao, string trangThai)
        {
            string query = string.Format("UPDATE dbo.Bai_bao SET tieuDe ='{0}', maSo = '{1}', ngayGui = '{2}', cacTuKhoa = '{3}', tomTat = '{4}', fileBaiBao = '{5}', loaiBaiBao = '{6}', trangThai = '{7}'",  tieuDe, maSo, ngayGui, cacTuKhoa, tomtat, fileBaiBao, loaiBaiBao, trangThai);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateArticle(int id, string tieuDe, int maSo, DateTime? ngayGui, string cacTuKhoa, string DOI, string tomtat, string fileBaiBao, string loaiBaiBao, string trangThai, string ketQua)
        {
            string query = string.Format("UPDATE dbo.Bai_bao SET tieuDe ='{1}', maSo = '{2}', ngayGui = '{3}', cacTuKhoa = '{4}', DOI = '{5}', tomTat = '{6}', fileBaiBao = '{7}', loaiBaiBao = '{8}', trangThai = '{9}', ketQua = '{10}' WHERE id = '{0}'", id, tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomtat, fileBaiBao, loaiBaiBao, trangThai, ketQua);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public bool UpdateArticleForAuthor(int id, string tieuDe, string cacTuKhoa, string tomTat)
        {
            string query = "ART_UpdateArticleInfo @id , @tieuDe , @tomTat , @cacTuKhoa";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { id, tieuDe, tomTat, cacTuKhoa });

            return result > 0;
        }

        public List<ArticleDTO> filterArticle(string loaiBaiBao, string trangthai)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "FilterByArticleInfo @loaiBaiBao , @trangthai";

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { loaiBaiBao, trangthai});

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticle2(string loaiBaiBao, string trangthai, string ngay)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            DateTime date = DateTime.Parse(ngay);

            string query = "FilterByArticleInfo @loaiBaiBao , @trangthai , @ngay";

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] {loaiBaiBao, trangthai, date });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticle3(string trangthai, string author)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "FilterByArticleInfo1 @trangthai , @author";

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { trangthai, author });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                Console.WriteLine(article);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticle4(string trangthai)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetNumberOfArticleByStatus @trangthai";

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { trangthai });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticleByAuthor(string trangThai, string ketQua, string date)
        {
            List<ArticleDTO> artList = new List<ArticleDTO>();

            DataTable result = null;

            string query;

            if (trangThai == "" && ketQua == "" && date != "")
            {
                DateTime ngay = DateTime.Parse(date);

                query = "AUT_FilterArticle @ngay";

                result = DataProvider.Instance.ExecuteQuery(query, new object[] { ngay});
            } else if (trangThai != "" && ketQua == "" && date != "")
            {
                DateTime ngay = DateTime.Parse(date);

                query = "AUT_FilterArticle @trangThai , @ngay";

                result = DataProvider.Instance.ExecuteQuery(query, new object[] { trangThai, ngay});
            } else if (trangThai != "" && ketQua == "" && date == "")
            {
                query = "AUT_FilterArticle @trangThai";

                result = DataProvider.Instance.ExecuteQuery(query, new object[] { trangThai});
            } else if (trangThai == "" && ketQua != "" && date == "")
            {
                query = "AUT_FilterArticle @ketQua";

                result = DataProvider.Instance.ExecuteQuery(query, new object[] { ketQua});
            }

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                artList.Add(article);
            }
            
            return artList;
        }

        public List<ArticleDTO> filterArticleByAuthor1(string date)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            DateTime ngay = DateTime.Parse(date);

            string query = string.Format("SELECT * FROM dbo.Bai_bao WHERE (ngayGui BETWEEN '{0}' AND GETDATE())", ngay);

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticleByAuthor2(string trangThai, string date)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            DateTime ngay = DateTime.Parse(date);

            string query = string.Format("SELECT * FROM dbo.Bai_bao WHERE (ngayGui BETWEEN '{0}' AND GETDATE()) AND trangthai = '{1}'", ngay, trangThai);

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticleByAuthor3(string trangThai)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = string.Format("SELECT * FROM dbo.Bai_bao WHERE trangthai = '{0}'", trangThai);

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> filterArticleByAuthor4(string ketQua)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = string.Format("SELECT * FROM dbo.Bai_bao WHERE ketQua = '{0}'", ketQua);

            //DataTable result = DataProvider.Instance.ExecuteNullQuery(query, new object[] { loaiBaiBao, trangThai, ngay, author });
            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> getArticleListByArticleType (int idNhaKhoaHoc, string loaiBaiBao)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleListByArtType @idNhaKhoaHoc , @loaiBaiBao";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, loaiBaiBao});

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> getArticleListByArticleTypeAndDate(int idNhaKhoaHoc, string loaiBaiBao, string date)
        {
            DateTime ngay = DateTime.Parse(date);

            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleListByArtTypeAndTime @idNhaKhoaHoc , @loaiBaiBao , @ngay";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, loaiBaiBao, ngay });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> getArticleListByAuthor(int idNhaKhoaHoc, string tenTacGia)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleListByAuthor @idNhaKhoaHoc , @tenTacGia";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, tenTacGia });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> getArticleListByAuthorAndTime(int idNhaKhoaHoc, string tenTacGia, string date)
        {
            DateTime ngay = DateTime.Parse(date);

            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_GetArticleListByAuthorAndTime @idNhaKhoaHoc , @tenTacGia , @ngay";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, tenTacGia, ngay });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> getArticleListResultInYear(int idNhaKhoaHoc, string date)
        {
            DateTime ngay = DateTime.Parse(date);

            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "AUT_GetArticleResultInYear @idNhaKhoaHoc , @ngay";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, ngay });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }

        public List<ArticleDTO> get3ArticlesAcceptamceResult(int idNhaKhoaHoc, string ketQua)
        {
            List<ArticleDTO> articleList = new List<ArticleDTO>();

            string query = "ART_get3ArticlesResult @idNhaKhoaHoc , @ketQua";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { idNhaKhoaHoc, ketQua });

            foreach (DataRow item in result.Rows)
            {
                ArticleDTO article = new ArticleDTO(item);

                articleList.Add(article);
            }

            return articleList;
        }
    }
}
