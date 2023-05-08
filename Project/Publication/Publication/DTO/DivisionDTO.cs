using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class DivisionDTO
    {
        private int iD;

        private int idPhanBien;

        private int idBaiBao;

        public int ID { get => iD; set => iD = value; }
        public int IdPhanBien { get => idPhanBien; set => idPhanBien = value; }
        public int IdBaiBao { get => idBaiBao; set => idBaiBao = value; }

        public DivisionDTO(int id, int idPhanBien, int idBaiBao)
        {
            this.ID = id;
            this.IdPhanBien = idPhanBien;
            this.IdBaiBao = idBaiBao;
        }

        public DivisionDTO (DataRow row)
        {
            this.ID = (int)row["id"];
            this.IdPhanBien = (int)row["idPhanBien"];
            this.IdBaiBao = (int)row["idBaiBao"];
        }
    }
}
