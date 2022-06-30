﻿using CefSharp;

namespace sengokugifu {
    internal class DownloadHandler : IDownloadHandler
    {
        BrowserForm myForm;

        public DownloadHandler(BrowserForm form)
        {
            myForm = form;
        }

        public void OnBeforeDownload(IBrowser browser, DownloadItem item, IBeforeDownloadCallback callback)
        {
            if (!callback.IsDisposed)
            {
                using (callback)
                {

                    myForm.UpdateDownloadItem(item);

                    // ask browser what path it wants to save the file into
                    string path = "C:\\sengoku\\download";//myForm.CalcDownloadPath(item);

					// if file should not be saved, path will be null, so skip file
					if (path != null) {
						callback.Continue(path, false);
					} else {
						callback.Dispose();
					}
                }
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            myForm.UpdateDownloadItem(downloadItem);
			if (downloadItem.IsInProgress && myForm.CancelRequests.Contains(downloadItem.Id)) {
				callback.Cancel();
			}
            //Console.WriteLine(downloadItem.Url + " %" + downloadItem.PercentComplete + " complete");
        }
    }
}
