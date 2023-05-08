using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    public class AccountDTO
    { 
        public AccountDTO(string username, string displayname, string password, int idNhaKhoaHoc, int role)
        {
            this.Username = username;
            this.DisplayName = displayname;
            this.Password = password;
            this.IdNhaKhoaHoc = idNhaKhoaHoc;
            this.Role = role;
        }

        public AccountDTO(string username, string displayname, string password, int role)
        {
            this.Username = username;
            this.DisplayName = displayname;
            this.Password = password;
            this.Role = role;
        }

        public AccountDTO()
        {
        }

        public AccountDTO(DataRow row)
        {
            this.Username = row["username"].ToString();

            this.DisplayName = row["displayName"].ToString();

            this.Password = row["password"].ToString();

            this.IdNhaKhoaHoc = (int)row["idNhaKhoaHoc"];

            this.Role = (int)row["role"];
        }

        private string username;
        public string Username { get => username; set => username = value; }

        private string displayName;
        public string DisplayName { get => displayName; set => displayName = value; }

        private string password;
        public string Password { get => password; set => password = value; }

        private int idNhaKhoaHoc;

        private int role;
        public int Role { get => role; set => role = value; }
        public int IdNhaKhoaHoc { get => idNhaKhoaHoc; set => idNhaKhoaHoc = value; }
    }
}
