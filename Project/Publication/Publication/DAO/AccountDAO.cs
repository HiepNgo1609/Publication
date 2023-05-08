using Publication.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance 
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set { instance = value; }
        }

        private AccountDAO() { }

        public bool login(string username, string password)
        {
            string query = "USP_Login @username , @password";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username , password });

            return result.Rows.Count > 0;
        }

        public int loginAthor(string username, string password)
        {
            string query = "USP_Login @username , @password";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username , password });

            AccountDTO acc = null;

            foreach (DataRow item in result.Rows)
            {
                acc = new AccountDTO(item);
            }

            return acc.Role;
        }

        public AccountDTO getAccountByUserName(string username)
        {
            string query = "SELECT * FROM dbo.Account WHERE username = '" + username + "'";

            DataTable result = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in result.Rows)
            {
                return new AccountDTO(item);
            }

            return null;
        }

        public bool updateAccountByUserName(string username, string displayName, string password)
        {
            string query;
            if (password == "")
            {
                query = string.Format("UPDATE dbo.Account SET displayName = '{1}' WHERE username = '{0}'", username, displayName);
            } else
            {
                query = string.Format("UPDATE dbo.Account SET displayName = '{1}', password = '{2}' WHERE username = '{0}'", username, displayName, password);
            }

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
