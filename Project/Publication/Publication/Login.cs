using Publication.DAO;
using Publication.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Publication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text;
            if (login(username, password) & loginAuthor(username, password) == 0)
            {
                AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(username);

                AppManager app = new AppManager(loginAccount);
                this.Hide();
                app.ShowDialog();
                this.Show();
            } else if (login(username, password) & loginAuthor(username, password) == 1)
            {
                AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(username);

                ReviewerView reviewer = new ReviewerView(loginAccount);

                this.Hide();
                reviewer.ShowDialog();
                this.Show();
            } else if (login(username, password) & loginAuthor(username, password) == 2)
            {
                AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(username);

                AuthorView authview = new AuthorView(loginAccount);

                this.Hide();
                authview.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }

        bool login(string username, string password)
        {
            return AccountDAO.Instance.login(username, password);
        }

        int loginAuthor(string username, string password)
        {
            return AccountDAO.Instance.loginAthor(username, password);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn thực sự muốn thoát?", "Thông báo!" ,MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
