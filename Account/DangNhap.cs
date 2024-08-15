using Account;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class DangNhap : Form

    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void button_Thoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_TaiKhoan_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox_an_Click(object sender, EventArgs e)
        {
            if (textBox_MatKhau.PasswordChar == '*')
            {
                pictureBox_hien.BringToFront();
                textBox_MatKhau.PasswordChar = '\0';
            }
        }

        private void pictureBox_hien_Click(object sender, EventArgs e)
        {
            if (textBox_MatKhau.PasswordChar == '\0')
            {
                pictureBox_an.BringToFront();
                textBox_MatKhau.PasswordChar = '*';
            }
        }

        private void linkLabel_QuenMatKhau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuenMatKhau quenMatKhau = new QuenMatKhau();
            quenMatKhau.ShowDialog();
        }

        private void linkLabel_DangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DangKi dangKi = new DangKi();
            dangKi.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tentaikhoan = textBox_TaiKhoan.Text;
            string matkhau = textBox_MatKhau.Text;

            

            if (tentaikhoan.Trim() == "") { MessageBox.Show("Vui lòng nhập tên tài khoản!"); }
            else if (matkhau.Trim() == "") { MessageBox.Show("Vui lòng nhập mật khẩu!"); }
            else
            {
                string query = "SELECT COUNT(*) FROM TaiKhoan WHERE tentaikhoan = @tentaikhoan AND matkhau = @matKhau";
                string query1 = string.Format("SELECT COUNT(*) FROM TaiKhoan WHERE tentaikhoan = '{0}' AND matkhau = '{1}'", tentaikhoan, matkhau);
                string query2 = "SELECT maso FROM Taikhoan WHERE tentaikhoan = '" + tentaikhoan.Trim() + "' ";
                Acc acc = new Acc();

                if (acc.isCheckLogin(query1) && acc.getDataFromQuery(query2) > 0)
                {

                    


                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    int maso = acc.getDataFromQuery(query2);
                    if (maso <= 5)
                    {
                        trangchu Trangchu = new trangchu();
                        Trangchu.ShowDialog();
                    }
                    else
                    {
                        ThongTinCaNhan thongTinCaNhan = new ThongTinCaNhan(tentaikhoan.Trim());
                        thongTinCaNhan.ShowDialog();
                    }

                  

                }
                else
                {
                    MessageBox.Show("Tên tài khoản và mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }



        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }
    }
}
