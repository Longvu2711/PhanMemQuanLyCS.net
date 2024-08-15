using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account
{
    internal class Taikhoan
    {
        private string tenTaikhoan;
        private string matKhau;

        public Taikhoan(string TaiKhoan, string matKhau)
        {
            this.tenTaikhoan = TaiKhoan;
            this.matKhau= matKhau;

        }

        public string TenTaikhoan { get => tenTaikhoan; set => tenTaikhoan = value; }
        public string MatKhau { get => matKhau; set => matKhau = value; }
    }
}
