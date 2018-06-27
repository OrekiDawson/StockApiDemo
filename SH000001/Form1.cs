using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SH000001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //try
            //{
            WebClient MyWebClient = new WebClient { Credentials = CredentialCache.DefaultCredentials };
            byte[] pageData = MyWebClient.DownloadData("https://hq.sinajs.cn/?format=text&list=sh000001");
            string pageHtml = Encoding.Default.GetString(pageData);
            string stockContent = pageHtml.Split('=')[1];
            string[] stockContentList = stockContent.Split(',');
            var YesterdayClose =Convert.ToDouble(stockContentList[2]) ;
            var PriceNow = Convert.ToDouble(stockContentList[3]) ;
            //MessageBox.Show(stockContent);
            var percent = (PriceNow > YesterdayClose) ? (PriceNow - YesterdayClose) / YesterdayClose :
                (PriceNow == YesterdayClose) ? 0.00 : (PriceNow / YesterdayClose) - 1;
            label2.Text = (percent*100).ToString();
            //}
            //catch (Exception)
            //{
            // ignored
            //}
        }
    }
}
