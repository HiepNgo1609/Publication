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
    public partial class AuthorView : Form
    {
        BindingSource artList = new BindingSource();

        BindingSource subList = new BindingSource();

        private AccountDTO loginAccount;

        public AccountDTO LoginAccount { get => loginAccount; set => loginAccount = value; }

        public AuthorView(AccountDTO acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            dgvArticleSearch.DataSource = artList;

            dgvAuthor.DataSource = subList;

            loadArticleByAuthorId();

            loadArticle();

            loadArticleStatus();

            loadArticleResult();

            loadArticleType();

            addArticleBinding();

            addArticleSearchBinding();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountInfo acInfo = new AccountInfo(LoginAccount);
            acInfo.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AuthorInformation authorInfo = new AuthorInformation(LoginAccount);
            authorInfo.ShowDialog();
        }

        void loadArticleByAuthorId()
        {
            int idNhaKhoaHoc = LoginAccount.IdNhaKhoaHoc;

            List<ArticleDTO> artList = Article.Instance.loadArticleByAuthor(idNhaKhoaHoc);

            dgvArticle.DataSource = artList;

        }

        void loadArticle()
        {
            List<ArticleDTO> articleList = Article.Instance.loadArticle();

            artList.DataSource = articleList;

        }

        void addArticleBinding()
        {
            txbId.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbTitle.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "TieuDe", true, DataSourceUpdateMode.Never));
            txbTomTat.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "TomTat", true, DataSourceUpdateMode.Never));
            txbTuKhoa.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "CacTuKhoa", true, DataSourceUpdateMode.Never));
            txbURLBaiBao.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "FileBaiBao", true, DataSourceUpdateMode.Never));
            txbStatus.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "TrangThai", true, DataSourceUpdateMode.Never));
            txbDate.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "NgayGui", true, DataSourceUpdateMode.Never));
            txbDOI.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "DOI", true, DataSourceUpdateMode.Never));
            txbKetQua.DataBindings.Add(new Binding("Text", dgvArticle.DataSource, "KetQua", true, DataSourceUpdateMode.Never));
        }

        void addArticleSearchBinding()
        {
            txbIDArt.DataBindings.Add(new Binding("Text", dgvArticleSearch.DataSource, "id", true, DataSourceUpdateMode.Never));
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbId.Text);
            string tieuDe = txbTitle.Text;
            string tomTat = txbTomTat.Text;
            string tuKhoa = txbTuKhoa.Text;

            if(Article.Instance.UpdateArticleForAuthor(id, tieuDe, tuKhoa, tomTat))
            {
                MessageBox.Show("Cập nhật thông tin bài báo thành công!");
                loadArticleByAuthorId();
            } else
            {
                MessageBox.Show("Có lỗi khi cập nhật thông tin bài báo!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDArt.Text);

            List<AuthorDTO> authorList = AuthorDAO.Instance.getAuthorInfoByArticleId(id);

            dgvAuthor.DataSource = authorList;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string trangThai = cbTrangThai.Text;
            string ketQua = cbKetQua.Text;
            string date = txbDateSearch.Text;

            if (trangThai == "" && ketQua == "" && date != "") {

                dgvArticleSearch.DataSource = Article.Instance.filterArticleByAuthor1(date);
                resetForm();
            } else if (trangThai != "" && ketQua == "" && date != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticleByAuthor2(trangThai, date);
                resetForm();
            } else if (trangThai != "" && ketQua == "" && date == "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticleByAuthor3(trangThai);
                resetForm();
            } else if (trangThai == "" && ketQua != "" && date == "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticleByAuthor4(ketQua);
                resetForm();
            }
        }
        private void btnAvgQty_Click(object sender, EventArgs e)
        {
            string date = txbDateSearch.Text;
            string loaiBaiBao = cbSearchLoaiBaiBao.Text;
            string trangThai = cbTrangThai.Text;

            if (loaiBaiBao == "" && trangThai == "")
            {
                DateTime ngay = DateTime.Parse(date);

                string query = string.Format("SELECT * FROM dbo.Bai_bao WHERE ngayGui BETWEEN '{0}' AND GETDATE()", ngay);

                DataTable num = DataProvider.Instance.ExecuteQuery(query);

                int i = 0;

                foreach (DataRow item in num.Rows)
                {
                    i++;
                }

                MessageBox.Show(i.ToString());
            } else if (date != "" && trangThai != "" && loaiBaiBao != "")
            {
                dgvAuthor.DataSource = YearDAO.Instance.getAverageNumberPerYear(date, loaiBaiBao, trangThai);
                resetForm();
            }

        }

        void resetForm()
        {
            if (txbDateSearch.Text != "")
            {
                txbDateSearch.Clear();
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
        }

    }
}
