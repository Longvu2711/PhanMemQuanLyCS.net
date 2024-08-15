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
    public partial class hanghoa : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da = null;
        DataTable dt = null;
        public hanghoa()
        {
            InitializeComponent();
        }
        private void hanghoa_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConnectionString);
            string query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



  



        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string maquay = row.Cells["Mã quầy"].Value.ToString();
                string mahanghoa = row.Cells["Mã hàng hóa"].Value.ToString();
                string tenhang = row.Cells["Tên hàng"].Value.ToString();
                decimal giaban = Convert.ToDecimal(row.Cells["Giá bán"].Value);
                string xuatxu = row.Cells["Xuất xứ"].Value.ToString();

                txtGiaBan.Text = giaban.ToString();
                txtMaHangHoa.Text = mahanghoa;
                txtMaQuay.Text = maquay;
                txtTenHang.Text = tenhang;
                txtXuatXu.Text = xuatxu;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string mahanghoa = txtMaHangHoa.Text;
            string tenhang = txtTenHang.Text;
            string giaban = txtGiaBan.Text;
            string xuatxu = txtXuatXu.Text;
            try
            {
                string query = $"INSERT INTO HangHoa (mahanghoa, tenhang, giaban, xuatxu, maquay) VALUES (@mahanghoa, @tenhang, @giaban, @xuatxu,@maquay)";
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {

                    con.Open();

                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@mahanghoa", txtMaHangHoa.Text);
                    command.Parameters.AddWithValue("@tenhang", txtTenHang.Text);
                    command.Parameters.AddWithValue("@giaban", decimal.Parse(txtGiaBan.Text));
                    command.Parameters.AddWithValue("@xuatxu", txtXuatXu.Text);
                    command.Parameters.AddWithValue("@maquay", txtMaQuay.Text);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Thêm hàng hóa thành công!");
                    query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa";
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
            SqlConnection con = new SqlConnection(sqlConnectionString);
            string query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string timkiem = txtTimKiem.Text;
            string thuoctinh = comboBoxThuocTinh.SelectedItem.ToString();
            string query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa ";


            switch (thuoctinh)
            {
                case "Mã quầy":
                    query += "WHERE maquay LIKE @timkiem";
                    break;
                case "Mã hàng":
                    query += "WHERE mahang LIKE @timkiem";
                    break;
                case "Tên hàng":
                    query += "WHERE tenhang LIKE @timkiem";
                    break;
                case "Xuất xứ":
                    query += "WHERE tinhtrang LIKE @timkiem";
                    break;

            }

            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@timkiem", "%" + timkiem + "%");

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }



        private void button5_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = dataGridView1.CurrentRow.Index;
            string mahanghoa = dataGridView1.Rows[selectedRowIndex].Cells["Mã hàng hóa"].Value.ToString();
            try
            {
                string query = "DELETE FROM HangHoa WHERE mahanghoa = @mahanghoa";

                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@mahanghoa", mahanghoa);

                    command.ExecuteNonQuery();

                    MessageBox.Show("Xóa hàng hóa thành công!");
                    query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa";
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

        private void button4_Click(object sender, EventArgs e)
        {
            string mahanghoa = txtMaHangHoa.Text;
            string tenhang = txtTenHang.Text;
            string giaban = txtGiaBan.Text;
            string xuatxu = txtXuatXu.Text;
            try
            {
                string query = "Update HangHoa set tenhang= @tenhang, giaban=@giaban,xuatxu= @xuatxu WHERE mahanghoa= @mahanghoa";

                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@tenhang", tenhang);
                    command.Parameters.AddWithValue("@giaban", giaban);
                    command.Parameters.AddWithValue("@xuatxu", xuatxu);
                    command.Parameters.AddWithValue("@mahanghoa", mahanghoa);

                    connection.Open();
                    command.ExecuteNonQuery();

                    MessageBox.Show("Lưu thông tin thành công!");
                    //con.Close();
                    con = new SqlConnection(sqlConnectionString);
                    query = "SELECT mahanghoa AS N'Mã hàng hóa',tenhang as N'Tên hàng',giaban as N'Giá bán', xuatxu as N'Xuất xứ',maquay as N'Mã quầy' From HangHoa";
                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                    MessageBox.Show("Lưu thành công");

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin.");

            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
