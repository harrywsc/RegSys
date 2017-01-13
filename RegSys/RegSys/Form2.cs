using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RegSys
{
    public partial class Form2 : Form
    {
        //调用API
        [System.Runtime.InteropServices.DllImport("user32", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetFocus(IntPtr hWnd);//设置此窗体为活动窗体


        public Form2()
        {
            InitializeComponent();
            Util.hanfm2 = this.Handle;
        }

        private void Btn_PW_Click(object sender, EventArgs e)
        {
            Util.CheckPW(tb_PW.Text);
            if (!Util.passwordResult)
            {
                MessageBox.Show("禁止退出系统，请联系管理员！");
            }
            this.Close();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.passwordChecking = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Util.passwordChecking)
            {
                if (Util.hanfm2 != GetFocus())
                {
                    SetFocus(Util.hanfm2);
                }
                this.WindowState = FormWindowState.Maximized;
            }
        }
    }
}
