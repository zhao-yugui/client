using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CefSharp;


namespace sengokugifu
{
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
		public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey) {

            Console.Out.WriteLine("key type:" + type + ", windowsKeyCode: " + windowsKeyCode  + ", nativeKeyCode:" + nativeKeyCode);

			if (type == KeyType.RawKeyDown) {

				
			}

			return false;
		}
    }
}
