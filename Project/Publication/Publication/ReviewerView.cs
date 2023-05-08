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
    public partial class ReviewerView : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount { get => loginAccount; set => loginAccount = value; }

        BindingSource artList = new BindingSource();

        public ReviewerView(AccountDTO acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            dgvArticleSearch.DataSource = artList;

            loadArticleDivisioned(acc);

            loadArticle();

            loadTCDG();

            loadAuthor();

            loadArticleStatus();

            loadArticleResult();

            loadArticleType();

            addArticleBinding();
        }

        private void quảnLýToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReviewerViewManagement rvm = new ReviewerViewManagement();
            rvm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfo acInfo = new AccountInfo(LoginAccount);
            acInfo.ShowDialog();
        }

        private void thToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReviewerInformation authorInfo = new ReviewerInformation(LoginAccount);
            authorInfo.ShowDialog();
        }

        void loadArticle()
        {
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            List<ArticleDTO> articleList = Article.Instance.loadArticleForReviewer(idNhaKhoaHoc);

            artList.DataSource = articleList;

        }

        void loadArticleDivisioned(AccountDTO acc)
        {
            AccountDTO loginAccount = AccountDAO.Instance.getAccountByUserName(acc.Username);
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            dgvArticle.DataSource = ReviewerDAO.Instance.getArticleListByIdReview(idNhaKhoaHoc);
        }

        void loadTCDG()
        {
            string query = "SELECT * FROM dbo.Tieu_chi_danh_gia";

            cbTCDG.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbTCDG.DisplayMember = "noidung";
        }

        void loadAuthor()
        {
            string query = "SELECT * FROM dbo.Nha_khoa_hoc";

            cbNhaKhoaHoc.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbNhaKhoaHoc.DisplayMember = "hoVaTen";
        }

        void loadArticleStatus()
        {
            string query = "SELECT trangthai FROM dbo.Trang_thai_bai_bao";

            cbTrangThai.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbTrangThai.DisplayMember = "trangthai";
        }

        void loadArticleResult()
        {
            string query = "SELECT ketqua FROM dbo.Ket_qua_bai_bao";

            cbKetQua.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbKetQua.DisplayMember = "ketqua";
        }

        void loadArticleType()
        {
            string query = "SELECT noidung FROM dbo.Loai_bai_bao";

            cbSearchLoaiBaiBao.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbSearchLoaiBaiBao.DisplayMember = "noidung";
        }

        void addArticleBinding()
        {
            txbId.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbIDArt.DataBindings.Add(new Binding("Text", dgvArticleSearch.DataSource, "id", true, DataSourceUpdateMode.Never));
        }

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            int idBaiBao = Convert.ToInt32(txbId.Text);
            string tieuChiDanhGia = cbTCDG.Text;
            string diem = txbDiem.Text;
            string noidungDG = rtxRvContent.Text;
            string noidungMDG = rtxMDG.Text;
            string ghiChuBBT = rtxNoteForBBT.Text;
            string ghiChuTG = rtxNoteForAuthor.Text;

            if (ReviewerDAO.Instance.SetReviewContent(idBaiBao, idNhaKhoaHoc, ghiChuTG, ghiChuBBT, diem,  noidungDG, noidungMDG, tieuChiDanhGia))
            {
                MessageBox.Show("Cập nhật phản biện cho bài báo thành công!");
                resetForm();
            } else
            {
                MessageBox.Show("Cập nhật phản biện cho bài báo thất bại!");
            }
        }

        void resetForm()
        {
            if (cbTCDG.Text != "")
            {
                cbTCDG.Text = "";
            }
            if (txbDiem.Text != "")
            {
                txbDiem.Text = "";
            }
            if (rtxRvContent.Text != "")
            {
                rtxRvContent.Text = "";
            }
            if (rtxMDG.Text != "")
            {
                rtxMDG.Text = "";
            }
            if (rtxNoteForBBT.Text != "")
            {
                rtxNoteForBBT.Text = "";
            }
            if (rtxNoteForAuthor.Text != "")
            {
                rtxNoteForAuthor.Text = "";
            }
            if (cbNhaKhoaHoc.Text != "")
            {
                cbNhaKhoaHoc.Text = "";
            }
            if (cbSearchLoaiBaiBao.Text != "")
            {
                cbSearchLoaiBaiBao.Text = "";
            }
            if (cbTrangThai.Text != "")
            {
                cbTrangThai.Text = "";
            }
            if (cbKetQua.Text != "")
            {
                cbKetQua.Text = "";
            }
            if (txbDateSearch.Text != "")
            {
                txbDateSearch.Text = "";
            }
        }

        private void btnAvgQty_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            string author = cbNhaKhoaHoc.Text;
            string loaiBaiBao = cbSearchLoaiBaiBao.Text;
            string trangThai = cbTrangThai.Text;
            string ketQua = cbKetQua.Text;
            string date = txbDateSearch.Text;
            if (loaiBaiBao != "" && trangThai == "" && ketQua == "" && date == "") {
                dgvArticleSearch.DataSource = Article.Instance.getArticleListByArticleType(idNhaKhoaHoc, loaiBaiBao);
                resetForm();
            } else if (loaiBaiBao != "" && trangThai == "" && ketQua == "" && date != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.getArticleListByArticleTypeAndDate(idNhaKhoaHoc, loaiBaiBao, date);
                resetForm();
            } else if (author != "" && date == "" && loaiBaiBao == "" && trangThai == "" && ketQua == "")
            {
                dgvArticleSearch.DataSource = Article.Instance.getArticleListByAuthor(idNhaKhoaHoc, author);
                resetForm();
            } else if (author != "" && date != "" && loaiBaiBao == "" && trangThai == "" && ketQua == "")
            {
                dgvArticleSearch.DataSource = Article.Instance.getArticleListByAuthorAndTime(idNhaKhoaHoc, author, date);
                resetForm();
            } else if (loaiBaiBao == "" && trangThai == "" && ketQua == "" && date != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.getArticleListResultInYear(idNhaKhoaHoc, date);
                resetForm();
            } else if (loaiBaiBao == "" && trangThai == "" && ketQua != "" && date == "")
            {
                dgvArticleSearch.DataSource = Article.Instance.get3ArticlesAcceptamceResult(idNhaKhoaHoc, ketQua);
                resetForm();
            }
        }

        private void btnAvgQty_Click_1(object sender, EventArgs e)
        {
            int idNhaKhoaHoc = loginAccount.IdNhaKhoaHoc;

            string date = txbDateSearch.Text;
            string ketQua = cbKetQua.Text;

            if (ketQua == "" && date != "")
            {
                dgvSub.DataSource = YearDAO.Instance.getSumArticleReviewed(idNhaKhoaHoc, date);
                resetForm();
            } 

        }

        
    }
}
