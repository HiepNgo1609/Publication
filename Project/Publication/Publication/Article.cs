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
    public partial class ArticleManager : Form
    {
        BindingSource artList = new BindingSource();
        public ArticleManager()
        {
            InitializeComponent();

            dtgvArticle.DataSource = artList;

            dgvArticleSearch.DataSource = artList;

            loadArticle();

            addArticleBinding();

            loadArticleType();

            loadArticleStatus();

            loadArticleResult();

            loadAuthorList();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        void loadArticle()
        {
            List<ArticleDTO> articleList = Article.Instance.loadArticle();

            artList.DataSource = articleList;

        }

        void addArticleBinding()
        {
            txbID.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "id", true, DataSourceUpdateMode.Never));
            cbxLoaiBaiBao.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "LoaiBaiBao", true, DataSourceUpdateMode.Never));
            txbArticleTitle.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "TieuDe", true, DataSourceUpdateMode.Never));
            txbTomTat.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "TomTat", true, DataSourceUpdateMode.Never));
            txbTuKhoa.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "CacTuKhoa", true, DataSourceUpdateMode.Never));
            txbURLBaiBao.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "FileBaiBao", true, DataSourceUpdateMode.Never));
            txbMaSo.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "MaSo", true, DataSourceUpdateMode.Never));
            dtpNgayGui.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "NgayGui", true, DataSourceUpdateMode.Never));
            txbDOI.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "DOI", true, DataSourceUpdateMode.Never));
            cbxStatus.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "TrangThai", true, DataSourceUpdateMode.Never));
            cbxKetQua.DataBindings.Add(new Binding("Text", dtgvArticle.DataSource, "KetQua", true, DataSourceUpdateMode.Never));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbID.Text);
            string loaiBaiBao = cbxLoaiBaiBao.Text;
            string tieuDe = txbArticleTitle.Text;
            string tomTat = txbTomTat.Text;
            string cacTuKhoa = txbTuKhoa.Text;
            string fileBaiBao = txbURLBaiBao.Text;
            int maSo = Convert.ToInt32(txbMaSo.Text);
            DateTime? ngayGui = dtpNgayGui.Value;
            string DOI = txbDOI.Text;
            string trangThai = cbxStatus.Text;
            string ketQua = cbxResult.Text;
            if(Article.Instance.UpdateArticle(id, tieuDe, maSo, ngayGui, cacTuKhoa, DOI, tomTat, fileBaiBao, loaiBaiBao, trangThai, ketQua))
            {
                MessageBox.Show("Sửa bài báo thành công!");
                loadArticle();
            } else
            {
                MessageBox.Show("Có lỗi khi sửa bài báo!");
            }
        }

        void loadArticleType()
        {
            string query = "SELECT noidung FROM dbo.Loai_bai_bao";

            cbLoaiBaiBaoFilter.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbLoaiBaiBaoFilter.DisplayMember = "noidung";
        }

        void loadArticleStatus()
        {
            string query = "SELECT trangthai FROM dbo.Trang_thai_bai_bao";

            cbxTrangThai.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbxTrangThai.DisplayMember = "trangthai";
        }

        void loadArticleResult()
        {
            string query = "SELECT ketqua FROM dbo.Ket_qua_bai_bao";

            cbxKetQua.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbxKetQua.DisplayMember = "ketqua";
        }

        void loadAuthorList()
        {
            List<AuthorDTO> authorList = AuthorDAO.Instance.getAllAuthor();

            cbxTacGia.DataSource = authorList;

            cbxTacGia.DisplayMember = "hoVaTen";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string loaiBaiBao = cbLoaiBaiBaoFilter.Text;

            string trangThai = cbxTrangThai.Text;
            
            string tbngay = txbDate.Text;

            string author = cbxTacGia.Text;

            if (tbngay == "" && author == "" && loaiBaiBao != "" && trangThai != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticle(loaiBaiBao, trangThai);

            } else if (tbngay != "" && author == "" && loaiBaiBao != "" && trangThai != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticle2(loaiBaiBao, trangThai, tbngay);
            } else if (tbngay == "" && author != "" && loaiBaiBao == "" && trangThai != "")
            {
                dgvArticleSearch.Refresh();
                dgvArticleSearch.DataSource = Article.Instance.filterArticle3(trangThai, author);
            } else if (tbngay == "" && author == "" && loaiBaiBao == "" && trangThai != "")
            {
                dgvArticleSearch.DataSource = Article.Instance.filterArticle4(trangThai);
            }
            resetForm();
        }

        void resetForm()
        {
            if (txbDate.Text != "")
            {
                txbDate.Clear();
            }
            if (cbLoaiBaiBaoFilter.Text != "")
            {
                cbLoaiBaiBaoFilter.Text = "";
            }
            if (cbxTrangThai.Text != "")
            {
                cbxTrangThai.Text = "";
            }
            if (cbxTacGia.Text != "")
            {
                cbxTacGia.Text = "";
            }
            if (cbxKetQua.Text != "")
            {
                cbxKetQua.Text = "";
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string loaiBaiBao = cbxLoaiBaiBao.Text;
            string tieuDe = txbArticleTitle.Text;
            string tomTat = txbTomTat.Text;
            string cacTuKhoa = txbTuKhoa.Text;
            string fileBaiBao = txbURLBaiBao.Text;
            int maSo = Convert.ToInt32(txbMaSo.Text);
            DateTime? ngayGui = dtpNgayGui.Value;
            string trangThai = cbxStatus.Text;
            if (Article.Instance.InsertArticle(tieuDe, maSo, ngayGui, cacTuKhoa, tomTat, fileBaiBao, loaiBaiBao, trangThai))
            {
                MessageBox.Show("Thêm bài báo thành công!");
                loadArticle();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm bài báo!");
            }
        }
    }
}
