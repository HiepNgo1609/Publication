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
    public partial class AuthorInformation : Form
    {

        private AccountDTO loginAccount;

        public AccountDTO LoginAccount { get => loginAccount;
            set { loginAccount = value; loadCurrentUserInfo(loginAccount); }
        }

        public AuthorInformation(AccountDTO acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }


        void loadCurrentUserInfo(AccountDTO acc)
        {
            AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(acc.Username);
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            AuthorDTO authorInfo = AuthorDAO.Instance.getAuthorById(idNhaKhoaHoc);
            txbId.Text = authorInfo.ID.ToString();
            txbFullName.Text = authorInfo.HoVaTen;
            txbPhone.Text = authorInfo.DienThoai;
            txbAddress.Text = authorInfo.DiaChi;
            txbCompany.Text = authorInfo.CoQuan;
            txbJob.Text = authorInfo.NgheNghiep;
            txbPEmail.Text = authorInfo.EmailCaNhan;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbId.Text);
            string hoTen = txbFullName.Text;
            string phone = txbPhone.Text;
            string address = txbAddress.Text;
            string job = txbJob.Text;
            string email = txbPEmail.Text;
            string company = txbCompany.Text;
            if (AuthorDAO.Instance.updateAuthorInfor(id, hoTen, phone, address, job, email, company))
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                loadCurrentUserInfo(LoginAccount);
            } else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
