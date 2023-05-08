using System;
using Publication.DAO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Publication.DTO;

namespace Publication
{
    public partial class AccountInfo : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; loadCurrentUserInfo(loginAccount); }
        }
        public AccountInfo(AccountDTO acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void loadCurrentUserInfo(AccountDTO acc)
        {
            AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(acc.Username);

            txbDisplayName.Text = loginAccount.DisplayName;
            txbUsername.Text = loginAccount.Username;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string displayName = txbDisplayName.Text;
            string password = txbNewPassword.Text;
            string confirm = txbConfirmPsw.Text;

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu mới phải trùng với xác nhận mật khẩu!");
            } else
            {
                if (AccountDAO.Instance.updateAccountByUserName(username, displayName, password))
                {
                    MessageBox.Show("Cập nhật thông tin tài khoản thành công!");
                    loadCurrentUserInfo(LoginAccount);
                } else
                {
                    MessageBox.Show("Có lỗi xảy ra khi cập nhật thông tin tài khoản!");
                }
            }
        }
    }
}
