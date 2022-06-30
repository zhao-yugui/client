using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace sengokugifu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show(System.AppDomain.CurrentDomain.BaseDirectory);
            string url = "http://api.sengokugifu.jp/home/login/login?Uname=9979289486&userid=9979289486&GameId=2001&ServerId=s13&Time=1602140715&al=1&from=aima&siteurl=woopie.jp&Sign=a93067ff3f83871346152319faef7410&nickname=%E3%82%B2%E3%82%B9%E3%83%88-20710514";
            //string url = "https://www.sogou.com?ServerId=s13";
            Application.Run(new BrowserForm(url));

             //if (args.Length != 0)
             //{
             //    if (args[0].StartsWith("sengoku://"))
             //    {
             //        string url = args[0].Substring(10);
             //        // MessageBox.Show(url);
             //        Application.Run(new BrowserForm(url));
             //    }
             //    else
             //    {
             //        MessageBox.Show("エラーを発生したため、起動できません。");
             //    }
             //}
             //else
             //{
             //     System.Diagnostics.Process.Start("https://www.bitqueen.jp");
             //     // Application.Run(new BrowserForm("http://www.bitqueen.jp"));
             //     // MessageBox.Show("ブラウザから起動してください。");
             //}

        }
    }

}
