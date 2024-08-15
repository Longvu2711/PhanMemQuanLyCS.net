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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Account
{
    public partial class chitietchuquay : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da = null;
        DataTable dt = null;
        public chitietchuquay()
        {
            InitializeComponent();
        }

        private void chitietchuquay_Load(object sender, EventArgs e)
        {
            string query = "SELECT q.maquay as N'Mã quầy', q.tenquay as N'Tên quầy',q.tinhtrang as N'Tình trạng quầy', q.vitri as N'Vị trí',q.mathangchinh as N'Mặt hàng chính',h.mahanghoa as N'Mã hàng' ,h.xuatxu as N'Xuất xứ',h.giaban as N'Giá bán',c.hovaten as N'Chủ quầy',c.machuquay as N'Mã chủ quầy',c.gioitinh as N'Giới tính' ,c.diachi as N'Địa chỉ chủ quầy',c.ngaysinh as N'Ngày sinh',c.sdt as N'Số điện thoại'\r\nFROM QuayKinhDoanh q\r\nJOIN ChuQuay c ON q.maquay = c.maquay\r\nJOIN HangHoa h ON q.maquay = h.maquay";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            string timkiem = txtTimKiem.Text;
            string thuoctinh = comboBoxThuocTinh.SelectedItem.ToString();
            string query = "SELECT q.maquay as N'Mã quầy', q.tenquay as N'Tên quầy', q.tinhtrang as N'Tình trạng quầy', q.vitri as N'Vị trí', q.mathangchinh as N'Mặt hàng chính', h.mahanghoa as N'Mã hàng', h.xuatxu as N'Xuất xứ', h.giaban as N'Giá bán', c.hovaten as N'Chủ quầy', c.machuquay as N'Mã chủ quầy', c.gioitinh as N'Giới tính', c.diachi as N'Địa chỉ chủ quầy', c.ngaysinh as N'Ngày sinh', c.sdt as N'Số điện thoại' " +
                     "FROM QuayKinhDoanh q " +
                     "JOIN ChuQuay c ON q.maquay = c.maquay " +
                     "JOIN HangHoa h ON q.maquay = h.maquay ";

            switch (thuoctinh)
            {
                case "Họ tên chủ quầy":
                    query += "WHERE c.hovaten LIKE @timkiem";
                    break;
                case "Số điện thoại chủ quầy":
                    query += "WHERE c.sdt LIKE @timkiem";
                    break;
                case "Mã chủ quầy":
                    query += "WHERE c.machuquay LIKE @timkiem";
                    break;
                case "Địa chỉ chủ quầy":
                    query += "WHERE c.diachi LIKE @timkiem";
                    break;
                case "Mã quầy":
                    query += "WHERE q.maquay LIKE @timkiem";
                    break;
                case "Tên quầy":
                    query += "WHERE q.tenquay LIKE @timkiem";
                    break;
                case "Mặt hàng chính":
                    query += "WHERE q.mathangchinh LIKE @timkiem";
                    break;
                case "Xuất xứ":
                    query += "WHERE h.xuatxu LIKE @timkiem";
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

        private void button2_Click(object sender, EventArgs e)
        {
            string query = "SELECT q.maquay as N'Mã quầy', q.tenquay as N'Tên quầy',q.tinhtrang as N'Tình trạng quầy', q.vitri as N'Vị trí',q.mathangchinh as N'Mặt hàng chính',h.mahanghoa as N'Mã hàng' ,h.xuatxu as N'Xuất xứ',h.giaban as N'Giá bán',c.hovaten as N'Chủ quầy',c.machuquay as N'Mã chủ quầy',c.gioitinh as N'Giới tính' ,c.diachi as N'Địa chỉ chủ quầy',c.ngaysinh as N'Ngày sinh',c.sdt as N'Số điện thoại'\r\nFROM QuayKinhDoanh q\r\nJOIN ChuQuay c ON q.maquay = c.maquay\r\nJOIN HangHoa h ON q.maquay = h.maquay";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            txtTimKiem.Text = "";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
