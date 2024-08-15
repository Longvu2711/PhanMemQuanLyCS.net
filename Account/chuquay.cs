using System;
using System.Collections;
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
    public partial class chuquay : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da = null;
        DataTable dt = null;
        public chuquay()
        {
            InitializeComponent();
        }
        private void chuquay_Load(object sender, EventArgs e)
        {
            string query = "select machuquay as N'Mã chủ quầy', maquay as N'Mã quầy',hovaten as N'Họ và tên',gioitinh as N'Giới tính',ngaysinh as N'Ngày sinh',diachi as N'Địa chỉ', sdt as N'Điện thoại' from ChuQuay";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string machuquay = row.Cells["Mã chủ quầy"].Value.ToString();
                string maquay = row.Cells["Mã quầy"].Value.ToString();
                string hoten = row.Cells["Họ và tên"].Value.ToString();
                string sdt = row.Cells["Điện thoại"].Value.ToString();
                string diachi = row.Cells["Địa chỉ"].Value.ToString();
                string gioitinh = row.Cells["Giới tính"].Value.ToString();
                DateTime ngaysinh = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);

                txtDiaChi.Text = diachi;
                txtGioiTinh.Text = gioitinh;
                txtHoVaTen.Text = hoten;
                txtMaChuQuay.Text = machuquay;
                txtMaQuay.Text = maquay;
                txtSDT.Text = sdt;
                dtpNgaySinh.Value = ngaysinh;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string machuquay = txtMaChuQuay.Text;
            string maquay = txtMaQuay.Text;
            string hovaten = txtHoVaTen.Text;
            string gioitinh = txtGioiTinh.Text;
            string sdt = txtSDT.Text;
            string ngaysinh = txtGioiTinh.Text;
            string diachi = txtDiaChi.Text;
            try
            {
                string query = $"INSERT INTO ChuQuay (machuquay, maquay, hovaten, gioitinh, ngaysinh, diachi, sdt) VALUES (@machuquay, @maquay, @hovaten, @gioitinh, @ngaysinh, @diachi, @sdt)";
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {

                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@machuquay", txtMaChuQuay.Text);
                    command.Parameters.AddWithValue("@maquay", txtMaQuay.Text);
                    command.Parameters.AddWithValue("@hovaten", txtHoVaTen.Text);
                    command.Parameters.AddWithValue("@gioitinh", txtGioiTinh.Text);
                    command.Parameters.AddWithValue("@ngaysinh", dtpNgaySinh.Text);
                    command.Parameters.AddWithValue("@sdt", txtSDT.Text);
                    command.Parameters.AddWithValue("@diachi", txtDiaChi.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm thông tin thành công!");


                    con = new SqlConnection(sqlConnectionString);
                    query = "select machuquay as N'Mã chủ quầy', maquay as N'Mã quầy',hovaten as N'Họ và tên',gioitinh as N'Giới tính',ngaysinh as N'Ngày sinh',diachi as N'Địa chỉ', sdt as N'Điện thoại' from ChuQuay";

                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    con.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin.");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string machuquay = txtMaChuQuay.Text;
            string maquay = txtMaQuay.Text;
            string hovaten = txtHoVaTen.Text;
            string gioitinh = txtGioiTinh.Text;
            string sdt = txtSDT.Text;
            DateTime ngaysinh = dtpNgaySinh.Value;
            string diachi = txtDiaChi.Text;
            try
            {
                string query = "Update ChuQuay set maquay= @maquay, hovaten=@hovaten, gioitinh=@gioitinh, ngaysinh=@ngaysinh, diachi=@diachi, sdt=@sdt WHERE machuquay=@machuquay ";

                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@machuquay", machuquay);
                    command.Parameters.AddWithValue("@maquay", maquay);
                    command.Parameters.AddWithValue("@hovaten", hovaten);
                    command.Parameters.AddWithValue("@gioitinh", gioitinh);
                    command.Parameters.AddWithValue("@sdt", sdt);
                    command.Parameters.AddWithValue("@diachi", diachi);
                    command.Parameters.AddWithValue("@ngaysinh", ngaysinh);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Lưu thông tin thành công!");
                    con.Close();
                    con = new SqlConnection(sqlConnectionString);
                    query = "select machuquay as N'Mã chủ quầy', maquay as N'Mã quầy',hovaten as N'Họ và tên',gioitinh as N'Giới tính',ngaysinh as N'Ngày sinh',diachi as N'Địa chỉ', sdt as N'Điện thoại' from ChuQuay";

                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;//ChuQuay(machuquay, maquay, hovaten, gioitinh, ngaysinh, diachi, sdt)
                    con.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin.");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dataGridView1.CurrentRow.Index;
            string machuquay = dataGridView1.Rows[selectedRowIndex].Cells["Mã chủ quầy"].Value.ToString();
            try
            {
                string query = "DELETE FROM ChuQuay WHERE machuquay = @machuquay";

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@machuquay", machuquay);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Xóa hàng hóa thành công!");
                    query = "select machuquay as N'Mã chủ quầy', maquay as N'Mã quầy',hovaten as N'Họ và tên',gioitinh as N'Giới tính',ngaysinh as N'Ngày sinh',diachi as N'Địa chỉ', sdt as N'Điện thoại' from ChuQuay";
                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin.");

            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query = "select machuquay as N'Mã chủ quầy', maquay as N'Mã quầy',hovaten as N'Họ và tên',gioitinh as N'Giới tính',ngaysinh as N'Ngày sinh',diachi as N'Địa chỉ', sdt as N'Điện thoại' from ChuQuay";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dtpNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtMaChuQuay_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
