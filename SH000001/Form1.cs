using System;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SH000001
{
    public partial class Form1 : Form
    {
        private Point _offset;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetStock000001();
            GetStockColor();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            GetStock000001();
            GetStockColor();
        }

        private void GetStock000001()
        {
            try
            {
                WebClient myWebClient = new WebClient {Credentials = CredentialCache.DefaultCredentials};
                byte[] pageData = myWebClient.DownloadData("https://hq.sinajs.cn/?format=text&list=sh000001");
                string pageHtml = Encoding.Default.GetString(pageData);
                //拆解#0
                string stockContent = pageHtml.Split('=')[1];
                //拆解#1
                string[] stockContentList = stockContent.Split(',');
                //昨收
                double yesterdayClose = Convert.ToDouble(stockContentList[2]);
                //现价
                double priceNow = Convert.ToDouble(stockContentList[3]);
                //计算百分比
                double percent = priceNow > yesterdayClose ? (priceNow - yesterdayClose) / yesterdayClose :
                    priceNow < yesterdayClose ? priceNow / yesterdayClose - 1 : 0.00;
                //百分比精确至两位
                label2.Text = priceNow.ToString("F") + @"      " + (priceNow > yesterdayClose ? "+" : "") +
                              (percent * 100).ToString("F") + @"%";
                //红涨绿跌
                Color redGreen = priceNow > yesterdayClose ? Color.Red :
                    priceNow < yesterdayClose ? Color.Green : Color.Black;
                label2.ForeColor = redGreen;
                pictureBox1.BackColor = redGreen;
            }
            catch (Exception)
            {
                // ignored
            }

            //获取API数据
        }

        private void GetStockColor()
        {
            try
            {
                string[] stockList = {"sz399001", "sz399006", "sh000300", "sh000905", "sh000016"};
                foreach (string s in stockList)
                {
                    WebClient myWebClient = new WebClient {Credentials = CredentialCache.DefaultCredentials};
                    byte[] pageData = myWebClient.DownloadData("https://hq.sinajs.cn/?format=text&list=" + s);
                    string pageHtml = Encoding.Default.GetString(pageData);
                    //拆解#0
                    string stockContent = pageHtml.Split('=')[1];
                    //拆解#1
                    string[] stockContentList = stockContent.Split(',');
                    //昨收
                    double yesterdayClose = Convert.ToDouble(stockContentList[2]);
                    //现价
                    double priceNow = Convert.ToDouble(stockContentList[3]);
                    //红涨绿跌
                    Color redGreen = priceNow > yesterdayClose ? Color.Red :
                        priceNow < yesterdayClose ? Color.Green : Color.Black;
                    switch (s)
                    {
                        case "sz399001":
                            pictureBox2.BackColor = redGreen;
                            break;
                        case "sz399006":
                            pictureBox3.BackColor = redGreen;
                            break;
                        case "sh000300":
                            pictureBox4.BackColor = redGreen;
                            break;
                        case "sh000905":
                            pictureBox5.BackColor = redGreen;
                            break;
                        case "sh000016":
                            pictureBox6.BackColor = redGreen;
                            break;
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = PointToScreen(e.Location);
            _offset = new Point(cur.X - Left, cur.Y - Top);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left != e.Button) return;
            Point cur = MousePosition;
            Location = new Point(cur.X - _offset.X, cur.Y - _offset.Y);
        }
    }
}