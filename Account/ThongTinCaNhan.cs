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

    
    public partial class ThongTinCaNhan : Form
    {
        public string account_username;
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        SqlDataAdapter da = null;
        DataTable dt = null;
        public ThongTinCaNhan(string username = "")
        {
            account_username = username;
            InitializeComponent();
        }

        private void thongtincanhan_Load(object sender, EventArgs e)
        {
            string query = "SELECT q.maquay as N'Mã quầy', q.tenquay as N'Tên quầy',q.tinhtrang as N'Tình trạng quầy', q.vitri as N'Vị trí',q.mathangchinh as N'Mặt hàng chính',h.mahanghoa as N'Mã hàng' ,h.xuatxu as N'Xuất xứ',h.giaban as N'Giá bán',c.hovaten as N'Chủ quầy',c.machuquay as N'Mã chủ quầy',c.gioitinh as N'Giới tính' ,c.diachi as N'Địa chỉ chủ quầy',c.ngaysinh as N'Ngày sinh',c.sdt as N'Số điện thoại'\r\nFROM QuayKinhDoanh q\r\nJOIN ChuQuay c ON q.maquay = c.maquay\r\nJOIN HangHoa h ON q.maquay = h.maquay WHERE c.machuquay = '" + account_username + "';";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }


        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
            DangNhap dangNhap = new DangNhap();
            dangNhap.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string machuquay = row.Cells["Mã chủ quầy"].Value.ToString();
                string maquay = row.Cells["Mã quầy"].Value.ToString();
                string hoten = row.Cells["Chủ quầy"].Value.ToString();
                string sdt = row.Cells["Số điện thoại"].Value.ToString();
                string diachi = row.Cells["Địa chỉ chủ quầy"].Value.ToString();
                string gioitinh = row.Cells["Giới tính"].Value.ToString();
                string giaban = row.Cells["Giá bán"].Value.ToString() ;

                string tenquay = row.Cells["Tên Quầy"].Value.ToString();
                string tinhtrangquay = row.Cells["Tình trạng quầy"].Value.ToString();
                string vitri = row.Cells["Vị trí"].Value.ToString();
                string mathangchinh = row.Cells["Mặt hàng chính"].Value.ToString();
                string mahang = row.Cells["Mã hàng"].Value.ToString();
                string xuatxu = row.Cells["Xuất xứ"].Value.ToString();
                DateTime ngaysinh = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);

                txtDiaChi.Text = diachi;
                txtGioiTinh.Text = gioitinh;
                txtHoVaTen.Text = hoten;
                txtMaChuQuay.Text = machuquay;
                txtMaQuay.Text = maquay;
                txtSDT.Text = sdt;
                dtpNgaySinh.Value = ngaysinh;
                txtGiaBan.Text = giaban;

                txtTenQuay.Text = tenquay;
                txtTinhTrangQuay.Text = tinhtrangquay;
                txtViTri.Text = vitri;
                txtMatHangChinh.Text = mathangchinh;
                txtMaHang.Text = mahang;
                txtXuatXu.Text = xuatxu;


            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            string machuquay = txtMaChuQuay.Text;
            string tenquay = txtTenQuay.Text;
            string tinhtrangquay = txtTinhTrangQuay.Text;
            string vitri = txtViTri.Text;
            string mathangchinh = txtMatHangChinh.Text;
            string mahang = txtMaHang.Text;
            string xuatxu = txtXuatXu.Text;
            string giaban = txtGiaBan.Text;
            string hoten = txtHoVaTen.Text;
            string gioitinh = txtGioiTinh.Text;
            string diachi = txtDiaChi.Text;
            DateTime ngaysinh = dtpNgaySinh.Value;
            string sdt = txtSDT.Text;

            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();

                 
                    string query1 = "UPDATE QuayKinhDoanh SET tenquay = @tenquay, tinhtrang = @tinhtrang, vitri = @vitri, mathangchinh = @mathangchinh WHERE maquay = @maquay";
                    SqlCommand command1 = new SqlCommand(query1, connection);

                    command1.Parameters.AddWithValue("@maquay", machuquay);
                    command1.Parameters.AddWithValue("@tenquay", tenquay);
                    command1.Parameters.AddWithValue("@tinhtrang", tinhtrangquay);
                    command1.Parameters.AddWithValue("@vitri", vitri);
                    command1.Parameters.AddWithValue("@mathangchinh", mathangchinh);

                    command1.ExecuteNonQuery();

                 
                    if (!string.IsNullOrEmpty(mahang))
                    {
                        string query2 = "UPDATE HangHoa SET mahanghoa = @mahanghoa, xuatxu = @xuatxu, giaban = @giaban WHERE mahanghoa = @mahanghoa";
                        SqlCommand command2 = new SqlCommand(query2, connection);

                        command2.Parameters.AddWithValue("@mahanghoa", mahang);
                        command2.Parameters.AddWithValue("@xuatxu", xuatxu);
                        command2.Parameters.AddWithValue("@giaban", giaban);

                        command2.ExecuteNonQuery();
                    }

                  
                    string query3 = "UPDATE ChuQuay SET hovaten = @hovaten, gioitinh = @gioitinh, diachi = @diachi, ngaysinh = @ngaysinh, sdt = @sdt WHERE machuquay = @machuquay";
                    SqlCommand command3 = new SqlCommand(query3, connection);

                    command3.Parameters.AddWithValue("@machuquay", machuquay);
                    command3.Parameters.AddWithValue("@hovaten", hoten);
                    command3.Parameters.AddWithValue("@gioitinh", gioitinh);
                    command3.Parameters.AddWithValue("@diachi", diachi);
                    command3.Parameters.AddWithValue("@ngaysinh", ngaysinh);
                    command3.Parameters.AddWithValue("@sdt", sdt);

                    command3.ExecuteNonQuery();

                    int a = command3.ExecuteNonQuery();
                    if (a == 0)
                    {
                        MessageBox.Show("Lỗi cập nhật thông tin!");
                    }    
                    else

                    MessageBox.Show("Lưu thông tin thành công!");

                    RefreshDataGrid();
                }
            }

            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi cập nhật thông tin: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! Vui lòng thử lại: " + ex.Message);
            }
        }

        private void RefreshDataGrid()
        {
        
            string query = "SELECT q.maquay as N'Mã quầy', q.tenquay as N'Tên quầy',q.tinhtrang as N'Tình trạng quầy', q.vitri as N'Vị trí',q.mathangchinh as N'Mặt hàng chính',h.mahanghoa as N'Mã hàng' ,h.xuatxu as N'Xuất xứ',h.giaban as N'Giá bán',c.hovaten as N'Chủ quầy',c.machuquay as N'Mã chủ quầy',c.gioitinh as N'Giới tính' ,c.diachi as N'Địa chỉ chủ quầy',c.ngaysinh as N'Ngày sinh',c.sdt as N'Số điện thoại'\r\nFROM QuayKinhDoanh q\r\nJOIN ChuQuay c ON q.maquay = c.maquay\r\nJOIN HangHoa h ON q.maquay = h.maquay WHERE c.machuquay = '" + account_username + "';";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
