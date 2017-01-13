using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace RegSys
{
    public partial class Form1 : Form
    {       
        //调用API
        [System.Runtime.InteropServices.DllImport("user32", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetFocus(); //获得本窗体的句柄
        [System.Runtime.InteropServices.DllImport("user32", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetFocus(IntPtr hWnd);//设置此窗体为活动窗体

        

        public Form1()
        {
            InitializeComponent();
            Util.hanfm1 = this.Handle;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            RegistryHelper rh = new RegistryHelper();
            string disableTaskMgr = "00000001";
            rh.SetRegistryData(Registry.CurrentUser, @"Software\Microsoft\Windows\CurrentVersion\Policies\system", "DisableTaskMgr", disableTaskMgr,RegistryValueKind.DWord);
            //webBrowser1.Url = new Uri("http://www.acfun.tv");
            string [] webPath = new string[2];            
            ReadPath(@".\FilePath.cfg.xml",webPath);
            webBrowser1.Navigate(webPath[0]);
            Util.password = webPath[1];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.passwordChecking = true;
            Form2 fm2 = new Form2();
            fm2.ShowDialog();
            if (!Util.passwordResult)
            {
                e.Cancel = true;
            }
        }

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //将所有的链接的目标，指向本窗体  
            foreach (HtmlElement archor in this.webBrowser1.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }
            //将所有的FORM的提交目标，指向本窗体  
            foreach (HtmlElement form in this.webBrowser1.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }
        }
        private void ReadPath(string path,string[] webpath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;//忽略文档里面的注释
            XmlReader reader = XmlReader.Create(path, settings);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(reader);
            XmlNode xn = xmlDoc.SelectSingleNode("File");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xn1 in xnl)
            {
                XmlElement xe = (XmlElement)xn1;// 将节点转换为元素，便于得到节点的属性值
                XmlNodeList xnl0 = xe.ChildNodes;
                webpath[0] = xnl0.Item(0).InnerText;//导入网页地址
                webpath[1] = xnl0.Item(1).InnerText;//导入密码信息
            }
            reader.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Util.passwordChecking)
            {
                if (Util.hanfm1 != GetFocus())
                {
                    SetFocus(Util.hanfm1);
                }
                this.WindowState = FormWindowState.Maximized;
            }
        }
        
    }
}
