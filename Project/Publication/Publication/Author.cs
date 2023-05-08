using Publication.DAO;
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
    public partial class Author : Form
    {
        BindingSource autList = new BindingSource();

        public Author()
        {
            InitializeComponent();
            dgvAuthor.DataSource = autList;

            loadAuthor();

            addAuthorBinding();

            loadArticle();

            loadAuthorCBX();

            loadArticleAuthor();
        }

        void loadAuthor()
        {
            autList.DataSource = AuthorDAO.Instance.getAllAuthorWithoutNull();
        }

        void addAuthorBinding()
        {
            txbId.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "id", true, DataSourceUpdateMode.Never));
            txbFullName.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "hoVaTen", true, DataSourceUpdateMode.Never));
            txbPhone.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "dienThoai", true, DataSourceUpdateMode.Never));
            txbEmail.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "emailCaNhan", true, DataSourceUpdateMode.Never));
            txbAddress.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "diaChi", true, DataSourceUpdateMode.Never));
            txbJob.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "ngheNghiep", true, DataSourceUpdateMode.Never));
            txbComp.DataBindings.Add(new Binding("Text", dgvAuthor.DataSource, "coQuan", true, DataSourceUpdateMode.Never));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbId.Text);
            string fullName = txbFullName.Text;
            string phone = txbPhone.Text;
            string email = txbEmail.Text;
            string diaChi = txbAddress.Text;
            string ngheNghiep = txbJob.Text;
            string coQuan = txbComp.Text;

            if (AuthorDAO.Instance.updateAuthorInfor(id, fullName, phone, diaChi, ngheNghiep, email, coQuan))
            {
                MessageBox.Show("Cập nhật thông tin Tác giả thành công!");
                loadAuthor();
            } else
            {
                MessageBox.Show("Cập nhật thông tin Tác giả thất bại!");
            }
        }

        void loadArticle()
        {
            string query = "SELECT tieuDe FROM dbo.Bai_bao";

            cbLoaiBaiBaoFilter.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbLoaiBaiBaoFilter.DisplayMember = "tieuDe";
        }

        void loadAuthorCBX()
        {
            string query = "SELECT hoVaTen FROM dbo.Nha_khoa_hoc";

            cbxTacGia.DataSource = DataProvider.Instance.ExecuteQuery(query);

            cbxTacGia.DisplayMember = "hoVaTen";
        }

        void loadArticleAuthor()
        {
            dgvArtAutSet.DataSource = Article_AuthorDAO.Instance.getAll();
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            string tieuDe = cbLoaiBaiBaoFilter.Text;
            string author = cbxTacGia.Text;

            if (Article_AuthorDAO.Instance.SetNewArticleAuthor(tieuDe, author))
            {
                MessageBox.Show("Cập nhật tác giả cho bài báo thành công!");
                loadArticleAuthor();
            } else
            {
                MessageBox.Show("Cập nhật tác giả cho bài báo thất bại!");
            }
        }
    }
}
