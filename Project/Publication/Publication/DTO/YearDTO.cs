using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publication.DTO
{
    class YearDTO
    {
        private int quantity;

        private int year;

        public int Quantity { get => quantity; set => quantity = value; }
        public int Year { get => year; set => year = value; }

        public YearDTO(int quantity, int year)
        {
            this.Quantity = quantity;
            this.Year = year;
        }

        public YearDTO(DataRow row)
        {
            this.Quantity = (int)row["Quantity"];
            this.Year = (int)row["Year"];
        }
    }
}
