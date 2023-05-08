using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DAO
{
    class DivisionDAO
    {
        private static DivisionDAO instance;

        public static DivisionDAO Instance
        {
            get { if (instance == null) instance = new DivisionDAO(); return instance; }
            private set { instance = value; }
        }

        public DivisionDAO() { }

        public bool addDivision(int idPhanBien, int idBaiBao)
        {
            string query = string.Format("INSERT INTO dbo.Phan_bien_bai_bao (idBaiBao, idPhanBien) VALUES ('{0}', '{1}')", idBaiBao, idPhanBien);
            
            int result = DataProvider.Instance.ExecuteNonQuery(query);
            
            return result > 0;
        }
    }
}
