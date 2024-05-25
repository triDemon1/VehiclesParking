﻿using QlyBaiGuiXe.GUI;
using QlyBaiGuiXe.MoHinhDuLieu;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QlyBaiGuiXe
{
    public partial class fDangNhap : Form
    {
        
        public fDangNhap()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(Setting.BoForm.CreateRoundRectRgn(0, 0, Width, Height, 30, 30));
            txbTk.Select();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Setting.TitleBar.ReleaseCapture();
                Setting.TitleBar.SendMessage(Handle, Setting.TitleBar.WM_NCLBUTTONDOWN, Setting.TitleBar.HTCAPTION, 0);
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Setting.TitleBar.ReleaseCapture();
                Setting.TitleBar.SendMessage(Handle, Setting.TitleBar.WM_NCLBUTTONDOWN, Setting.TitleBar.HTCAPTION, 0);
            }
        }

        private bool showPass = false;
        private void picMk_Click(object sender, EventArgs e)
        {
            if (showPass == false)
            {
                this.picMk.Image = global::QlyBaiGuiXe.Properties.Resources.hide;
                txbMk.PasswordChar = '\0';
                showPass = true;
            }
            else
            {
                this.picMk.Image = global::QlyBaiGuiXe.Properties.Resources.view;
                txbMk.PasswordChar = '\u2022';
                showPass = false;
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fQuenMK_1 newform = new fQuenMK_1();
            this.Hide();
            //this.Close();
            newform.ShowDialog();
        }

        private void fDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void reset()
        {
            txbMk.Text = string.Empty;
            txbTk.Text = string.Empty;
        }
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            // check tai khoan

            try
            {
                BaiXeDBContext db = new BaiXeDBContext();
                var nhanVien = (from nv in db.NhanVien
                               where nv.MaNv == txbTk.Text
                               select nv).FirstOrDefault();
                if(nhanVien == null)
                {
                    MessageBox.Show("Vui lòng kiểm tra lại tên đăng nhập", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txbTk.Focus();
                }
                else
                {
                    var checkMK = (from tk in db.TaiKhoan
                                where tk.MaTk==nhanVien.MaTk
                                select tk.MatKhau).FirstOrDefault();

                    if (checkMK != null)
                    {
                        if (checkMK.Equals(txbMk.Text))
                        {
                            mainUI m = new mainUI(nhanVien);
                            this.Hide();
                            m.ShowDialog();
                            if (!m.isClose)
                            {
                                reset();
                                txbTk.Focus();
                                this.Show();
                                this.ShowInTaskbar = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vui lòng kiểm tra lại mật khẩu", "Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txbMk.Focus();
                        }
                    }
                
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi lấy dữ liệu đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void fDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
