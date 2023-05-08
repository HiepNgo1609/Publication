using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class Article_AuthorDAO
    {
        private static Article_AuthorDAO instance;


        public static Article_AuthorDAO Instance
        {
            get { if (instance == null) instance = new Article_AuthorDAO(); return instance; }
            private set { instance = value; }
        }

        public bool SetNewArticleAuthor(string tieuDe, string hoVaTen)
        {
            string query = "SetArticleAuthor @tieuDe , @hoVaTen";

            int result = DataProvider.Instance.ExecuteNonQuery(query, new object[] { tieuDe, hoVaTen });

            return result > 0;
        }

        public List<Article_AuthorDTO> getAll()
        {
            List<Article_AuthorDTO> qty = new List<Article_AuthorDTO>();

            string query = "GetAllArticleAuthor";


            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Article_AuthorDTO article = new Article_AuthorDTO(item);

                qty.Add(article);
            }

            return qty;
        }
    }
}
