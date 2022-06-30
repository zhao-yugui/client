using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.Diagnostics;
using System.Web;

namespace sengokugifu
{
    public partial class BrowserForm : Form
    {
        public static string UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.79 Safari/537.1";
        public ChromiumWebBrowser chromeBrowser;
        public static BrowserForm Instance;

        private static int DEFAULT_HEIGHT = 720;
        private static int DEFAULT_WIDTH = 1280;


        public List<int> downloadCancelRequests;
        public double defaultZoomLevel = 0.0;

        public Boolean isCtrlPressing = false;

        private string targetUrl;

        public BrowserForm()
        {

            InitializeComponent();

            InitBrowser();            
        }

        public BrowserForm(string url)
        {

            InitializeComponent();

            var uri = new Uri(url);

            var collection = HttpUtility.ParseQueryString(uri.Query);//默认采用UTF-8编码，当然也可以传入特定编码进行解析
            
            WindowState = FormWindowState.Maximized;    //最大化窗体
            this.Text = "戦国義風 " + collection["ServerId"] + " " + collection["nickname"];
            targetUrl = url;

            ActiveControl = this.browserPanel;

            InitBrowser();
        }

        private void InitBrowser()
        {
            Instance = this;
            CefSettings settings = new CefSettings();
            settings.Locale = "ja-JP";
            //指定flash的版本，不使用系统安装的flash版本

            settings.CefCommandLineArgs.Add("ppapi-flash-path", System.AppDomain.CurrentDomain.BaseDirectory + "plugins/pepflashplayer.dll");
            var flashVerison = "18.0.0.160";
            settings.CefCommandLineArgs.Add("ppapi-flash-version", flashVerison);

            settings.UserAgent = UserAgent;

            settings.IgnoreCertificateErrors = true;

          //  settings.CachePath = GetAppDir("Cache");

            Cef.Initialize(settings);


            // 创建实例
            chromeBrowser = new ChromiumWebBrowser(this.targetUrl);
            chromeBrowser.LifeSpanHandler = new LifeSpanHandler(this);

            BrowserSettings browserSettings = new BrowserSettings();
            browserSettings.AcceptLanguageList = "ja_JP";
            chromeBrowser.BrowserSettings = browserSettings;
            //InstalledFontCollection MyFont = new InstalledFontCollection();
            this.browserPanel.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;


            IKeyboardHandler keyboardHandler = new KeyboardHandler(this);
            chromeBrowser.KeyboardHandler = keyboardHandler;
            //chromeBrowser.KeyDown += keyboardHandler;
            //chromeBrowser.KeyUp += keyboardHandler;
            //this.KeyDown += onKeyDown;
            //this.KeyUp += onKeyUp;


            //this.chromeBrowser.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pMouseWheel);
            //this.chromeBrowser.Focus();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Console.Out.WriteLine("1111mouse wheel: " + e.Delta);
        }

        //protected override void OnSizeChanged(EventArgs e)
        //{
        //    base.OnSizeChanged(e);
        //    Console.Out.WriteLine("height: " + this.Height + " width:" + this.Width);
        //    if (chromeBrowser != null && chromeBrowser.IsBrowserInitialized)
        //    {
        //        if (this.Height <= DEFAULT_HEIGHT || this.Width <= DEFAULT_WIDTH)
        //        {
        //            chromeBrowser.SetZoomLevel(0.0);
        //            return;
        //        }                
        //
        //        double zoomLevelH = this.Height / (DEFAULT_HEIGHT * 1.0f);
        //        double zoomLevelW = this.Width / (DEFAULT_WIDTH * 1.0f);
        //        double zoomLevel = Math.Min(zoomLevelH, zoomLevelW) - 1;
        //        Console.Out.WriteLine("zoomLevel: " + zoomLevel);
        //        this.chromeBrowser.SetZoomLevel(zoomLevel);
        //    }
        //    
        //}

        private void pMouseWheel(object sender, MouseEventArgs e)
        {
            if (isCtrlPressing)
            {

            }
            Console.Out.WriteLine("mouse wheel: " + e.Delta);
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            Console.Out.WriteLine("key up: " + e.KeyCode);
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            var keyCode = e.KeyCode;
            Console.Out.WriteLine("key down: " + e.KeyCode);

        }

        private void 放大ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            defaultZoomLevel += 0.25;
            if (defaultZoomLevel >= 4)
            {
                defaultZoomLevel = 4;
            }
            chromeBrowser.SetZoomLevel(defaultZoomLevel);
        }

        private void 缩小ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            defaultZoomLevel -= 0.25;
            if (defaultZoomLevel <= 0)
            {
                defaultZoomLevel = 0.0;
            }
            chromeBrowser.SetZoomLevel(defaultZoomLevel);
        }
    }


    internal class KeyboardHandler : IKeyboardHandler
    {
        BrowserForm myForm;

        public KeyboardHandler(BrowserForm form)
        {
            myForm = form;
        }
        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {


            return false;
        }

        /// <inheritdoc/>
		public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {

            Console.Out.WriteLine("key type:" + type + ", windowsKeyCode: " + windowsKeyCode + ", nativeKeyCode:" + nativeKeyCode);

            if(windowsKeyCode == 17)
            {
                if (type == KeyType.RawKeyDown)
                {   
                    myForm.isCtrlPressing = true;
                } else if (type == KeyType.KeyUp)
                {
                    myForm.isCtrlPressing = false;
                }
            }
            if (windowsKeyCode == 187) //加
            {
                if (type == KeyType.RawKeyDown  && myForm.isCtrlPressing)
                {

                    myForm.defaultZoomLevel += 0.25;
                    if (myForm.defaultZoomLevel >= 3)
                    {
                        myForm.defaultZoomLevel = 3;
                    }
                    myForm.chromeBrowser.SetZoomLevel(myForm.defaultZoomLevel);
                }
            }
            if (windowsKeyCode == 189) //减
            {
                if (type == KeyType.RawKeyDown && myForm.isCtrlPressing)
                {
                    myForm.defaultZoomLevel -= 0.25;
                    if(myForm.defaultZoomLevel <= 0)
                    {
                        myForm.defaultZoomLevel = 0.0;
                    }
                    myForm.chromeBrowser.SetZoomLevel(myForm.defaultZoomLevel);
                }
               
            }

            return false;
        }
    }

}
