using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Account
{
    public partial class DangKi : Form
    {
        public DangKi()
        {
            InitializeComponent();
        }

        public bool checkAccount(string ac)
        {
            return Regex.IsMatch(ac, "^[a-zA-Z0-9]{2,32}$");
        }
        public bool checkEmail(string em)
        {
            return Regex.IsMatch(em, @"^([^\s@]+@[^\s@]+\.[^\s@]+)$");
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            string tentaikhoan = textBox_TaiKhoanDangKy.Text;
            string matkhau = textBox_MatKhauDangKi.Text;
            string xacnhanmatkhau = textBox_XacNhanMatKhau.Text;
            string email = textBox_EmailDangKy.Text;


            
            if (IsValidData(tentaikhoan, matkhau, xacnhanmatkhau, email))
            {
   

                string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
                using (SqlConnection conn = new SqlConnection(sqlConnectionString))
                {
                    conn.Open();
                    string checkAccountSql = "SELECT COUNT(*) FROM TaiKhoan WHERE tentaikhoan = @tentaikhoan";
                    SqlCommand checkCmd = new SqlCommand(checkAccountSql, conn);
                    checkCmd.Parameters.AddWithValue("@tentaikhoan", tentaikhoan);

                    int existingAccountCount = (int)checkCmd.ExecuteScalar();

                    if (existingAccountCount > 0)
                    {
                        MessageBox.Show($"Tài khoản '{tentaikhoan}' đã được đăng ký!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    string sql = "INSERT INTO TaiKhoan (tentaikhoan, matkhau, email) VALUES (@tentaikhoan, @matkhau, @email)";

                   
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tentaikhoan", tentaikhoan);
                    cmd.Parameters.AddWithValue("@matkhau", matkhau);
                    cmd.Parameters.AddWithValue("@email", email);

                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textBox_TaiKhoanDangKy.Text = "";
                        textBox_MatKhauDangKi.Text = "";
                        textBox_XacNhanMatKhau.Text = "";
                        textBox_EmailDangKy.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
          
        }

        private bool IsValidData(string tenTaiKhoan, string matKhau, string xacNhanMatKhau, string email)
        {
            bool isValid = true; 

            if (string.IsNullOrEmpty(tenTaiKhoan))
            {
                MessageBox.Show("Tên tài khoản không thể để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (string.IsNullOrEmpty(xacNhanMatKhau))
            {
                MessageBox.Show("Xác nhận mật khẩu không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (matKhau != xacNhanMatKhau)
            {
                MessageBox.Show("Mật khẩu không trùng khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }

            return isValid; 
        }

        private bool IsValidEmail(string email)
        {
         
            const string emailRegex = @"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

            private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void textBox_TaiKhoanDangKy_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox_TaiKhoanDangKy.Text = "";
            textBox_MatKhauDangKi.Text = "";
            textBox_XacNhanMatKhau.Text = "";
            textBox_EmailDangKy.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox_EmailDangKy_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
