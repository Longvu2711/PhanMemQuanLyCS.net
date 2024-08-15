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
    public partial class quaykinhdoanh : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlDataAdapter da = null;
        DataTable dt = null;
        public quaykinhdoanh()
        {
            InitializeComponent();
        }
        private void quaykinhdoanh_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConnectionString);
            string query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }



        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                string query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh";



                string maquay = txtMaQuay.Text;
                string tenquay = txtTenQuay.Text;
                string mathangchinh = txtMatHangChinh.Text;
                string vitri = txtViTri.Text;
                string tinhtrang = txtTinhTrang.Text;

                //  thêm thông tin vào bảng QuayKinhDoanh
                string query2 = $"INSERT INTO QuayKinhDoanh ( maquay, tenquay, mathangchinh, vitri, tinhtrang) VALUES ( @maquay, @tenquay,@mathangchinh, @vitri, @tinhtrang)";
                using (SqlConnection con = new SqlConnection(sqlConnectionString))
                {
                    con.Open();

                    SqlCommand command = new SqlCommand(query2, con);
                    command.Parameters.AddWithValue("@maquay", txtMaQuay.Text);
                    command.Parameters.AddWithValue("@tenquay", txtTenQuay.Text);
                    command.Parameters.AddWithValue("@mathangchinh", txtMatHangChinh.Text);
                    command.Parameters.AddWithValue("@vitri", txtViTri.Text);
                    command.Parameters.AddWithValue("@tinhtrang", txtTinhTrang.Text);


                    command.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("Thêm Thông Tin Quầy Thành Công!");



                    da = new SqlDataAdapter(query, con);
                    dt = new DataTable();
                    dt.Clear();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                //tạo 1 kho cho quầy 
                //try
                //{
                //    string queryKho = "INSERT INTO Kho (maquay,tonkho) VALUES (@maquay, 0)";
                //    using (SqlConnection con = new SqlConnection(sqlConnectionString))
                //    {
                //        con.Open();
                //        SqlCommand commandKho = new SqlCommand(queryKho, con);
                //        commandKho.Parameters.AddWithValue("@maquay", maquay);
                //        commandKho.ExecuteNonQuery();
                //        con.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin." + ex.Message);

            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            try
            {

                string maquay = txtMaQuay.Text;
                string tenquay = txtTenQuay.Text;
                string mathangchinh = txtMatHangChinh.Text;
                string vitri = txtViTri.Text;
                string tinhtrang = txtTinhTrang.Text;
                SqlConnection con = new SqlConnection(sqlConnectionString);
                con.Open();

                string query = "UPDATE QuayKinhDoanh SET tenquay = @tenquay, mathangchinh = @mathangchinh, vitri = @vitri, tinhtrang = @tinhtrang WHERE maquay = @maquay";

                using (SqlCommand command = new SqlCommand(query, con))
                {

                    command.Parameters.AddWithValue("@tenquay", tenquay);
                    command.Parameters.AddWithValue("@mathangchinh", mathangchinh);
                    command.Parameters.AddWithValue("@vitri", vitri);
                    command.Parameters.AddWithValue("@tinhtrang", tinhtrang);
                    command.Parameters.AddWithValue("@maquay", maquay);


                    command.ExecuteNonQuery();
                }

                con.Close();
                query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh";

                da = new SqlDataAdapter(query, con);
                dt = new DataTable();
                dt.Clear();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                MessageBox.Show("Lưu thông tin thành công");
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi! Hãy kiểm tra lại thông tin.");

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnectionString))
                {
                    connection.Open();
                    string maquay = txtMaQuay.Text;
                    // Bắt đầu transaction
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        //Xóa quầy = xóa tất cả dữ liệu ở các bảng có chung mã quầy
                        string deleteHangHoaQuery = "DELETE FROM HangHoa WHERE maquay = @maquay";
                        using (SqlCommand deleteHangHoaCommand = new SqlCommand(deleteHangHoaQuery, connection, transaction))
                        {
                            deleteHangHoaCommand.Parameters.AddWithValue("@maquay", maquay);
                            deleteHangHoaCommand.ExecuteNonQuery();
                        }
                        string deleteChuQuayQuery = "DELETE FROM ChuQuay WHERE maquay = @maquay";
                        using (SqlCommand deleteChuQuayCommand = new SqlCommand(deleteChuQuayQuery, connection, transaction))
                        {
                            deleteChuQuayCommand.Parameters.AddWithValue("@maquay", maquay);
                            deleteChuQuayCommand.ExecuteNonQuery();
                        }

                        string deleteKhoQuery = "DELETE FROM Kho WHERE maquay = @maquay";
                        using (SqlCommand deleteKhoCommand = new SqlCommand(deleteKhoQuery, connection, transaction))
                        {
                            deleteKhoCommand.Parameters.AddWithValue("@maquay", maquay);
                            deleteKhoCommand.ExecuteNonQuery();
                        } 
                        string deleteNhapHangQuery = "DELETE FROM NhapHang WHERE maquay = @maquay";
                        using (SqlCommand deleteNhapHangCommand = new SqlCommand(deleteNhapHangQuery, connection, transaction))
                        {
                            deleteNhapHangCommand.Parameters.AddWithValue("@maquay", maquay);
                            deleteNhapHangCommand.ExecuteNonQuery();
                        }

                        string deleteQuayQuery = "DELETE FROM QuayKinhDoanh WHERE maquay = @maquay";
                        using (SqlCommand deleteQuayCommand = new SqlCommand(deleteQuayQuery, connection, transaction))
                        {
                            deleteQuayCommand.Parameters.AddWithValue("@maquay", maquay);
                            deleteQuayCommand.ExecuteNonQuery();
                        }
                        
                        // Commit transaction nếu không có lỗi xảy ra
                        transaction.Commit();
                        SqlConnection con = new SqlConnection(sqlConnectionString);
                        string query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh";

                        MessageBox.Show("Xóa dữ liệu thành công!");
                        da = new SqlDataAdapter(query, con);
                        dt = new DataTable();
                        dt.Clear();
                        da.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction nếu có lỗi xảy ra
                        transaction.Rollback();

                        MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(sqlConnectionString);
            string query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh";
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            dt.Clear();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string timkiem = txtTimKiem.Text;
            string thuoctinh = comboBoxThuocTinh.SelectedItem.ToString();
            string query = "Select maquay as N'Mã quầy', tenquay as N'Tên quầy', mathangchinh as N'Mặt hàng chính',vitri as N'Vị Trí',tinhtrang as N'Tình trạng' from QuayKinhDoanh  ";


            switch (thuoctinh)
            {
                case "Mã quầy":
                    query += "WHERE maquay LIKE @timkiem";
                    break;
                case "Tên quầy":
                    query += "WHERE tenquay LIKE @timkiem";
                    break;
                case "Mặt hàng chính":
                    query += "WHERE mathangchinh LIKE @timkiem";
                    break;
                case "Tình trạng":
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                string maquay = row.Cells["Mã quầy"].Value.ToString();
                string tenquay = row.Cells["Tên quầy"].Value.ToString();
                string mathangchinh = row.Cells["Mặt hàng chính"].Value.ToString();
                string vitri = row.Cells["Vị trí"].Value.ToString();
                string tinhtrang = row.Cells["Tình trạng"].Value.ToString();

                txtMaQuay.Text = maquay;
                txtTenQuay.Text = tenquay;
                txtMatHangChinh.Text = mathangchinh;
                txtViTri.Text = vitri;
                txtTinhTrang.Text = tinhtrang;

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
