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
    public partial class Review : Form
    {
        BindingSource divisionList = new BindingSource();
        public Review()
        {
            InitializeComponent();

            dgvPhanBienBaiBao.DataSource = divisionList;

            loadReviewerList();

            loadDivisionList();

            loadArticleSelect();

            loadReviewerSelect();

            addReviewerBinding();

            addDivisionBinding();
        }

        void loadReviewerList()
        {
            string query = "SELECT * FROM dbo.Phan_bien_Info";
            dgvReviewer.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void loadDivisionList()
        {
            string query = "SELECT * FROM dbo.Phan_bien_bai_bao";

            divisionList.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        void loadArticleSelect()
        {
            List<ArticleDTO> artList = Article.Instance.loadNullResultArticle();

            cbBaiBao.DataSource = artList;

            cbBaiBao.DisplayMember = "tieuDe";

        }

        void loadReviewerSelect()
        {
            List<ReviewerDTO> rvList = ReviewerDAO.Instance.loadReviewerSelect();
            cbPhanBien.DataSource = rvList;
            cbPhanBien.DisplayMember = "hoVaTen";
        }

        void addReviewerBinding()
        {
            txbFullName.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "hoVaTen", true, DataSourceUpdateMode.Never));
            txbTrinhDo.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "trinhDo", true, DataSourceUpdateMode.Never));
            txbChuyenMon.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "chuyenMon", true, DataSourceUpdateMode.Never));
            txbQtyPhanBien.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "soLuongPhanBien", true, DataSourceUpdateMode.Never));
            dtpNgayCongTac.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "ngayCongTac", true, DataSourceUpdateMode.Never));
            txbEmailComp.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "emailCoQuan", true, DataSourceUpdateMode.Never));
            txbAddress.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "diaChi", true, DataSourceUpdateMode.Never));
            txbPhone.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "dienThoai", true, DataSourceUpdateMode.Never));
            txbJob.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "ngheNghiep", true, DataSourceUpdateMode.Never));
            txbCompany.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "coQuan", true, DataSourceUpdateMode.Never));
            txbPEmail.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "emailCaNhan", true, DataSourceUpdateMode.Never));
            txbPBID.DataBindings.Add(new Binding("Text", dgvReviewer.DataSource, "id", true, DataSourceUpdateMode.Never));
        }

        private void btnPhanCong_Click(object sender, EventArgs e)
        {
            int idBaiBao = (cbBaiBao.SelectedItem as ArticleDTO).ID;

            int idPhanBien = (cbPhanBien.SelectedItem as ReviewerDTO).ID;

            if(DivisionDAO.Instance.addDivision(idPhanBien, idBaiBao))
            {
                MessageBox.Show("Phân công phản biện thành công!");

                loadDivisionList();
            } else
            {
                MessageBox.Show("Có lỗi khi phân công phản biện!");
            }

        }

        void addDivisionBinding()
        {
            txbId.DataBindings.Add(new Binding("Text", dgvPhanBienBaiBao.DataSource, "id", true, DataSourceUpdateMode.Never));
        }

        private void txbId_TextChanged(object sender, EventArgs e)
        {
            
            int idPhanBien = (int)dgvPhanBienBaiBao.SelectedCells[0].OwningRow.Cells["idPhanBien"].Value;

            int idBaiBao = (int)dgvPhanBienBaiBao.SelectedCells[0].OwningRow.Cells["idBaiBao"].Value;


            ReviewerDTO review = ReviewerDAO.Instance.GetReviewerByID(idPhanBien);

            ArticleDTO article = Article.Instance.GetArticleByID(idBaiBao);

            cbPhanBien.Text = review.HoVaTen;

            cbBaiBao.Text = article.TieuDe;
        }

        
    }
}
