using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormBT
{
    public class SinhVien
    {
        public string hoTen { get; set; }
        public string maLop { get; set; }

        public SinhVien(string ht, string ml)
        {
            this.hoTen = ht;
            this.maLop = ml;
        }

        public SinhVien()
        {
            
        }
    }
}
