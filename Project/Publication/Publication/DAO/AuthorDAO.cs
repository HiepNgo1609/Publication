using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class AuthorDAO
    {
        private static AuthorDAO instance;

        public static AuthorDAO Instance
        {
            get { if (instance == null) instance = new AuthorDAO(); return instance; }
            private set { instance = value; }
        }

        public List<AuthorDTO> getAllAuthorWithoutNull()
        {
            List<AuthorDTO> authorList = new List<AuthorDTO>();

            string query = "SELECT * FROM dbo.Nha_khoa_hoc";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                AuthorDTO author = new AuthorDTO(item);

                authorList.Add(author);
            }

            return authorList;
        }

        public List<AuthorDTO> getAllAuthor()
        {
            List<AuthorDTO> authorList = new List<AuthorDTO>();

            AuthorDTO nullAuthor = new AuthorDTO();

            authorList.Add(nullAuthor);

            string query = "SELECT * FROM dbo.Nha_khoa_hoc";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                AuthorDTO author = new AuthorDTO(item);

                authorList.Add(author);
            }

            return authorList;
        }

        public AuthorDTO getAuthorById(int id)
        {
            AuthorDTO author = null;

            string query = "SELECT * FROM dbo.Nha_khoa_hoc WHERE id = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach(DataRow item in data.Rows)
            {
                author = new AuthorDTO(item);

                return author;
            }
            return author;
        }

        public bool updateAuthorInfor(int id, string hoVaTen, string dienThoai, string diaChi, string ngheNghiep, string emailCaNhan, string coQuan)
        {
            string query = string.Format("UPDATE dbo.Nha_khoa_hoc SET hoVaTen = '{1}', dienThoai = '{2}', diaChi = '{3}', ngheNghiep = '{4}', emailCaNhan = '{5}', coQuan = '{6}' WHERE id = '{0}'", id, hoVaTen, dienThoai, diaChi, ngheNghiep, emailCaNhan, coQuan);

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }

        public List<AuthorDTO> getAuthorInfoByArticleId(int id)
        {
            List<AuthorDTO> authorList = new List<AuthorDTO>();

            string query = "AUT_GetAuthorByArticleID @idBaiBao";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { id });

            foreach (DataRow item in data.Rows)
            {
                AuthorDTO author = new AuthorDTO(item);

                authorList.Add(author);
            }

            return authorList;
        }
    }
}
