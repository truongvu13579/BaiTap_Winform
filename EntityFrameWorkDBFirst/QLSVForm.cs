using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrameWorkDBFirst
{
    public partial class QLSVForm : Form
    {
        QLSinhVienEntities db = new QLSinhVienEntities();
        public QLSVForm()
        {
            InitializeComponent();
        }
        
        private void QLSVForm_Load(object sender, EventArgs e)
        {
            var result = db.SinhViens.Where(s => s.ID > 0).ToList();

            foreach (var sv in result)
            {
                ListViewItem item = new ListViewItem(sv.ID.ToString());
                lvSinhVien.Items.Add(item);
                item.SubItems.Add(sv.HoTen);
                item.SubItems.Add(sv.Lop.TenLop);
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            cboLop.Text = "";
            txtID.Text = "";
            txtHoTen.Text = "";
        }
        
        private void btnTaiLai_Click(object sender, EventArgs e)
        {
            lvSinhVien.Items.Clear();
            QLSVForm_Load(sender, e);
        }
        
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(txtHoTen.Text != "" && cboLop.Text != "")
            {
                var temp = db.Lops.Where(x => x.TenLop == cboLop.Text);
                string idLop = "";
                foreach (var lop in temp)
                {
                    idLop = lop.ID.ToString();
                    break;
                }
                if (string.IsNullOrEmpty(idLop))
                {
                    MessageBox.Show("Vui lòng nhập đúng thông tin lớp!", "Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }
                SinhVien sv = new SinhVien() { HoTen = txtHoTen.Text, MaLop = Int32.Parse(idLop)};
                db.SinhViens.Add(sv);
                db.SaveChanges();
                btnTaiLai_Click(sender,e);
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin trước khi thêm.");
                btnTaiLai_Click(sender,e);
            }
        }
        

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && txtSearch.Text == "")
            {
                btnTaiLai_Click(sender, e);
            }
            else if (e.KeyData == Keys.Enter)
            {
                this.lvSinhVien.Items.Clear();
                var results = db.SinhViens
                    .Where(sv => sv.HoTen.ToString() == txtSearch.Text).ToList();
                foreach (var sv in results)
                {
                    ListViewItem item = new ListViewItem(sv.ID.ToString());
                    lvSinhVien.Items.Add(item);
                    item.SubItems.Add(sv.HoTen);
                    item.SubItems.Add(sv.Lop.TenLop);
                }
            }
        }
        
        private void lvSinhVien_Click(object sender, EventArgs e)
        {
            ListViewItem item = lvSinhVien.SelectedItems[0];
            txtID.Text = item.Text;
            txtHoTen.Text = item.SubItems[1].Text;
            cboLop.Text = item.SubItems[2].Text;
        }
    }
}
