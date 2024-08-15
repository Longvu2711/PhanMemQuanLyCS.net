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
    public partial class kho : Form
    {
        string sqlConnectionString = $"Data Source=LAPTOP-LIGMABAL\\MSSQLSERVER01;Initial Catalog=quanlyquay1;Integrated Security=True";
        SqlConnection con = null;
        DataTable dt = null;
        SqlDataAdapter da = null;
        public kho()
        {
            InitializeComponent();
        }
        private void ChangeRowColorByKho()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Số lượng tồn kho"].Value != null && row.Cells["Số lượng tồn kho"].Value != DBNull.Value)
                {
                    int tonKho = Convert.ToInt32(row.Cells["Số lượng tồn kho"].Value);

                    if (tonKho <= 30)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else if (tonKho > 30 && tonKho <= 70)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;

                    }
                    else
                    {
                        // Đặt lại màu mặc định cho dòng
                        row.DefaultCellStyle.BackColor = dataGridView1.DefaultCellStyle.BackColor;
                    }
                }
            }
        }
        private void kho_Load(object sender, EventArgs e)
        {
            string query = "select k.maquay as N'Mã quầy', q.tenquay as N'Tên quầy', k.tonkho as N'Số lượng tồn kho' from Kho k join QuayKinhDoanh q ON q.maquay = k.maquay";
            SqlConnection con = new SqlConnection(sqlConnectionString);
            da = new SqlDataAdapter(query, con);
            dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            ChangeRowColorByKho();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
