using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Publication.DAO;
using Publication.DTO;

namespace Publication
{
    public partial class AppManager : Form
    {
        private AccountDTO loginAccount;

        public AccountDTO LoginAccount { get => loginAccount; set => loginAccount = value; }
        public AppManager(AccountDTO acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            loadAccountList();

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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bàiBáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArticleManager art = new ArticleManager();
            art.ShowDialog();
        }

        private void tácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Author auth = new Author();
            auth.ShowDialog();
        }

        private void phảnBiệnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Review re = new Review();
            re.ShowDialog();
        }

        void loadAccountList()
        {
            string query = "SELECT * FROM dbo.Account";

            dataListAccount.DataSource = DataProvider.Instance.ExecuteQuery(query);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
