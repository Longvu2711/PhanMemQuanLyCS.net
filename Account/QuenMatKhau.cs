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

namespace Account
{
    public partial class QuenMatKhau : Form
    {
        public QuenMatKhau()
        {
            InitializeComponent();
            textBox_TaiKhoanDangKy.Enabled = false;
            textBox_MatKhauDangKy.Enabled = false;
        }

        Acc acc = new Acc();
        private void button_LayLaiTaiKhoan_Click(object sender, EventArgs e)
        {
            string email = textBox_EmailDangKy.Text;
            if (email.Trim() == "") { MessageBox.Show("Vui lòng nhập email đăng ký!"); }
            else
            {
                string query = "Select * from TaiKhoan WHERE email = '" + email + "' ";
                var listTK = acc.TaiKhoans(query);
                if (listTK.Count() != 0)
                {
                    label_Email.ForeColor = Color.Blue;
                    textBox_TaiKhoanDangKy.Text =  listTK[0].TenTaikhoan;
                    textBox_MatKhauDangKy.Text =  listTK[0].MatKhau;
                }
                else
                {

                    label_Email.ForeColor = Color.Red;
                    MessageBox.Show($"Email '{email}' chưa được đăng ký!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label_Email.ForeColor = Color.Black;
            textBox_EmailDangKy.Text = "";
            textBox_TaiKhoanDangKy.Text = "";
            textBox_MatKhauDangKy.Text = "";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

