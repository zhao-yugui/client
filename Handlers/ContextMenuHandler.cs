using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CefSharp;
using System.Windows.Forms;
using CefSharp.WinForms;

namespace sengokugifu
{
	internal class ContextMenuHandler : IContextMenuHandler {

		private const int ShowDevTools = 26501;
		private const int CloseDevTools = 26502;
		private const int SaveImageAs = 26503;
		private const int SaveAsPdf = 26504;
		private const int SaveLinkAs = 26505;
		private const int CopyLinkAddress = 26506;
		private const int RefreshTab = 40008;
		BrowserForm myForm;

		private string lastSelText = "";

		public ContextMenuHandler(BrowserForm form) {
			myForm = form;
		}

		public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model) {
			
			// clear the menu
			model.Clear();

			// save text
			//lastSelText = parameters.SelectionText;


			//Removing existing menu item
			//bool removed = model.Remove(CefMenuCommand.ViewSource); // Remove "View Source" option
			if (parameters.LinkUrl != "") {
			//	model.AddItem((CefMenuCommand)OpenLinkInNewTab, "新标签打开");
				//model.AddItem((CefMenuCommand)CopyLinkAddress, "复制链接");
			//	model.AddSeparator();
			}


			if (parameters.SelectionText != null) {

				// TEXT IS SELECTED

			}

			//Add new custom menu items
			//#if DEBUG
			//model.AddItem((CefMenuCommand)ShowDevTools, "Developer tools");
			//model.AddItem(CefMenuCommand.ViewSource, "View source");
			//model.AddSeparator();
			//#endif

			//model.AddItem((CefMenuCommand)RefreshTab, "刷新");
			//model.AddItem((CefMenuCommand)CloseTab, "关闭");

		}

		public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags) {

			int id = (int)commandId;

			if (id == ShowDevTools) {
				browser.ShowDevTools();
			}
			if (id == CloseDevTools) {
				browser.CloseDevTools();
			}
			if (id == SaveImageAs) {
				browser.GetHost().StartDownload(parameters.SourceUrl);
			}
			if (id == SaveLinkAs) {
				browser.GetHost().StartDownload(parameters.LinkUrl);
			}
			if (id == CopyLinkAddress) {
				Clipboard.SetText(parameters.LinkUrl);
			}

			return false;
		}

		public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) {

		}

		public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback) {

			// show default menu
			return false;
		}
	}
}
