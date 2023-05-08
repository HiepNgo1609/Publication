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
    public partial class ReviewerInformation : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; loadCurrentUserInfo(loginAccount); }
        }

        public ReviewerInformation(AccountDTO acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void loadCurrentUserInfo(AccountDTO acc)
        {
            AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(acc.Username);

            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            ReviewerDTO reviewerInfo = ReviewerDAO.Instance.GetReviewerByID(idNhaKhoaHoc);

            txbId.Text = reviewerInfo.ID.ToString();
            txbFullName.Text = reviewerInfo.HoVaTen;
            txbPhone.Text = reviewerInfo.DienThoai;
            txbAddress.Text = reviewerInfo.DiaChi;
            txbCompany.Text = reviewerInfo.CoQuan;
            txbJob.Text = reviewerInfo.NgheNghiep;
            txbPEmail.Text = reviewerInfo.EmailCaNhan;
            txbLv.Text = reviewerInfo.TrinhDo;
            txbMjr.Text = reviewerInfo.ChuyenMon;
            txbDate.Text = reviewerInfo.NgayCongTac.ToString();
            txbQtyReview.Text = reviewerInfo.SoLuongPhanBien.ToString();
            txbEmailCom.Text = reviewerInfo.EmailCoQuan;
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
            string trinhdo = txbLv.Text;
            string chuyenmon = txbMjr.Text;
            string ngayCongTac = txbDate.Text;
            string emailCom = txbEmailCom.Text;

            if (ReviewerDAO.Instance.updateReviewerInfo(id, hoTen, address, phone, job, email, company, trinhdo, chuyenmon, emailCom))
            {
                MessageBox.Show("Cập nhật thông tin cá nhân thành công!");
            } else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin cá nhân!");
            }
        }

        
    }
}
