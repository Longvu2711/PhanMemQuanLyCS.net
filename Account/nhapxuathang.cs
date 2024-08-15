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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Account
{
    public partial class nhapxuathang : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da = null;
        DataTable dt = null;

        public nhapxuathang()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string madonnhap = row.Cells["Mã đơn nhập"].Value.ToString();
                string maquay = row.Cells["Mã quầy"].Value.ToString();
                string soluongnhap = row.Cells["Số lượng"].Value.ToString();
                DateTime ngaynhap = Convert.ToDateTime(row.Cells["Ngày nhập"].Value);

                txtMaDonNhap.Text = madonnhap;
                txtMaQuay.Text = maquay;
                txtSoLuongNhap.Text = soluongnhap;
                dtpNgayNhap.Value = ngaynhap;

                string selectTonKhoQuery = "SELECT tonkho FROM Kho WHERE maquay = @maquay";

                try
                {
                    using (SqlConnection con = new SqlConnection(sqlConnectionString))
                    {
                        con.Open();

                        SqlCommand selectTonKhoCommand = new SqlCommand(selectTonKhoQuery, con);
                        selectTonKhoCommand.Parameters.AddWithValue("@maquay", maquay);

                        object result = selectTonKhoCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int tonKho = Convert.ToInt32(result);
                            txtTonKho.Text = tonKho.ToString();
                        }
                        else
                        {
                            txtTonKho.Text = "0"; // Giá trị mặc định khi không có tồn kho
                        }

                        con.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                string madonxuat = row.Cells["Mã đơn xuất"].Value.ToString();
                string maquay2 = row.Cells["Mã quầy"].Value.ToString();
                string soluongxuat = row.Cells["Số lượng"].Value.ToString();
                DateTime ngayxuat = Convert.ToDateTime(row.Cells["Ngày xuất"].Value);
                txtMaDonXuat.Text = madonxuat;
                txtMaQuay2.Text = maquay2;
                txtSoLuongXuat.Text = soluongxuat;
                dtpNgayXuat.Value = ngayxuat;
                string selectTonKhoQuery = "SELECT tonkho FROM Kho WHERE maquay = @maquay";

                try
                {
                    using (SqlConnection con = new SqlConnection(sqlConnectionString))
                    {
                        con.Open();

                        SqlCommand selectTonKhoCommand = new SqlCommand(selectTonKhoQuery, con);
                        selectTonKhoCommand.Parameters.AddWithValue("@maquay", maquay2);

                        object result = selectTonKhoCommand.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            int tonKho = Convert.ToInt32(result);
                            txtTonKho.Text = tonKho.ToString();
                        }
                        else
                        {
                            txtTonKho.Text = "0"; // Giá trị mặc định khi không có tồn kho
                        }

                        con.Close();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }

            }
        }

        private void nhapxuathang_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConnectionString);
            string query = "select n.madonnhap as N'Mã đơn nhập', n.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',n.soluongnhap as N'Số lượng',n.ngaynhap as N'Ngày nhập' from NhapHang n join HangHoa h on h.maquay=n.maquay";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            string query2 = "select x.madonxuat as N'Mã đơn xuất', x.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',x.soluongxuat as N'Số lượng',x.ngayxuat as N'Ngày xuất' from XuatHang x join HangHoa h on h.maquay=x.maquay";
            da = new SqlDataAdapter(query2, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }    

        private void button1_Click(object sender, EventArgs e)
        {
            string maDonNhap = txtMaDonNhap.Text;
            int soLuongNhap = int.Parse(txtSoLuongNhap.Text);
            string maquay = txtMaQuay.Text;
            DateTime ngayNhap = dtpNgayNhap.Value;

            string insertDonNhapQuery = "INSERT INTO NhapHang (madonnhap, maquay, soluongnhap, ngaynhap) " +
                                        "VALUES (@madonnhap, @maquay, @soluongnhap, @ngaynhap)";

            try
            {
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();

                    SqlCommand insertDonNhapCommand = new SqlCommand(insertDonNhapQuery, con);
                    insertDonNhapCommand.Parameters.AddWithValue("@madonnhap", maDonNhap);
                    insertDonNhapCommand.Parameters.AddWithValue("@maquay", maquay);
                    insertDonNhapCommand.Parameters.AddWithValue("@soluongnhap", soLuongNhap);
                    insertDonNhapCommand.Parameters.AddWithValue("@ngaynhap", ngayNhap);

                    insertDonNhapCommand.ExecuteNonQuery();

                    MessageBox.Show("Tạo đơn nhập hàng thành công!");

                    con.Close();
                    string query = "select n.madonnhap as N'Mã đơn nhập', n.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',n.soluongnhap as N'Số lượng',n.ngaynhap as N'Ngày nhập' from NhapHang n join HangHoa h on h.maquay=n.maquay";
                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string maDonXuat = txtMaDonXuat.Text;
            int soLuongXuat = int.Parse(txtSoLuongXuat.Text);
            string maquay = txtMaQuay2.Text;
            DateTime ngayXuat = dtpNgayXuat.Value;
            int tonKho = int.Parse(txtTonKho.Text);

            string insertDonNhapQuery = "INSERT INTO XuatHang (madonxuat, maquay, soluongxuat, ngayxuat) " +
                                        "VALUES (@madonxuat, @maquay, @soluongxuat, @ngayxuat)";

            try
            {
                if (tonKho < soLuongXuat)
                {
                    MessageBox.Show("Số hàng xuất không được vượt quá trong kho");
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(sqlConnectionString))
                    {

                        con.Open();

                        SqlCommand insertDonNhapCommand = new SqlCommand(insertDonNhapQuery, con);
                        insertDonNhapCommand.Parameters.AddWithValue("@madonxuat", maDonXuat);
                        insertDonNhapCommand.Parameters.AddWithValue("@maquay", maquay);
                        insertDonNhapCommand.Parameters.AddWithValue("@soluongxuat", soLuongXuat);
                        insertDonNhapCommand.Parameters.AddWithValue("@ngayxuat", ngayXuat);

                        insertDonNhapCommand.ExecuteNonQuery();

                        MessageBox.Show("Tạo đơn xuất hàng thành công!");

                        con.Close();
                        string query = "select x.madonxuat as N'Mã đơn xuất', x.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',x.soluongxuat as N'Số lượng',x.ngayxuat as N'Ngày xuất' from XuatHang x join HangHoa h on h.maquay=x.maquay";
                        Console.WriteLine(query);
                        da = new SqlDataAdapter(query, con);
                        dt = new DataTable();
                        dt.Clear();
                        da.Fill(dt);
                        dataGridView2.DataSource = dt;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTonKho_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                SqlConnection con = new SqlConnection(sqlConnectionString);
                string query = "SELECT n.ngaynhap as N'ngày nhập', n.maquay as N'Mã quầy',h.tenhang as N'Tên hàng hóa', SUM(n.soluongnhap) AS N'Tổng số lượng nhập'\r\nFROM NhapHang n\r\njoin HangHoa h on h.maquay = n.maquay\r\nGROUP BY n.ngaynhap, n.maquay,h.tenhang\r\n";
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            else
            {
                SqlConnection con = new SqlConnection(sqlConnectionString);
                string query = "select n.madonnhap as N'Mã đơn nhập', n.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',n.soluongnhap as N'Số lượng',n.ngaynhap as N'Ngày nhập' from NhapHang n join HangHoa h on h.maquay=n.maquay";
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                SqlConnection con = new SqlConnection(sqlConnectionString);
                string query = "SELECT x.ngayxuat as N'Ngày xuất', x.maquay as N'Mã quầy',h.tenhang as N'Tên hàng', SUM(x.soluongxuat) AS N'Tống số lượng xuất'\r\nFROM XuatHang x\r\njoin HangHoa h on h.maquay = x.maquay\r\nGROUP BY x.ngayxuat, x.maquay,h.tenhang";
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
            else
            {
                SqlConnection con = new SqlConnection(sqlConnectionString);
                string query = "select n.madonnhap as N'Mã đơn nhập', n.maquay as N'Mã quầy',h.tenhang as N'Tên Hàng',n.soluongnhap as N'Số lượng',n.ngaynhap as N'Ngày nhập' from NhapHang n join HangHoa h on h.maquay=n.maquay";
                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView2.DataSource = dt;
            }
        }

        private void txtMaDonXuat_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
