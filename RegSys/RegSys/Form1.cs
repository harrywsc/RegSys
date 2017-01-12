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
        int times = 0;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            //webBrowser1.Url = new Uri("http://www.acfun.tv");
            string [] webPath = new string[1];            
            ReadPath(@".\FilePath.cfg.xml",webPath);
            webBrowser1.Navigate(webPath[0]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (times<10)
            {
                times++;
                MessageBox.Show("窗口禁止关闭！");
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
                webpath[0] = xnl0.Item(0).InnerText;
            }
            reader.Close();
        }

    }
}
