﻿using System;
using System.IO;
using Xamarin.Forms;
using Android.Content;
using Android.Webkit;
using Android.Graphics;
using Android.Views;

[assembly: Dependency(typeof(Forms9Patch.Droid.HtmlToPngService))]
namespace Forms9Patch.Droid
{
	public class HtmlToPngService : Java.Lang.Object, IHtmlToPngPdfService
	{
		public void ToPng(string html, string fileName, Action<string> onComplete)
		{
			var size = new Size(8.5, 11);
			var externalPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
			var dir = new Java.IO.File(externalPath);
			var file = new Java.IO.File(dir + "/" + fileName + ".png");
			if (!dir.Exists())
				dir.Mkdir();
			if (file.Exists())
				file.Delete();

			var webView = new CustomWebView(Android.App.Application.Context);
			webView.Settings.JavaScriptEnabled = true;
			webView.DrawingCacheEnabled = true;

			webView.Layout(0,0, (int)((size.Width - 0.5) * 72), (int)((size.Height - 0.5) * 72));

			webView.LoadData(html,"text/html; charset=utf-8","UTF-8");
			webView.SetWebViewClient(new WebViewCallBack(fileName, onComplete));
		}

	}

	class WebViewCallBack : Android.Webkit.WebViewClient, IValueCallback
	{

		readonly string _fileName;
		readonly Action<string> _onComplete;
		CustomWebView webView;

		Java.IO.File _dir;


		public WebViewCallBack(string fileName, Action<string> onComplete)
		{
			_dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDocuments);
			_fileName = fileName;
			_onComplete = onComplete;
		}

		public void OnReceiveValue(Java.Lang.Object value)
		{
			System.Diagnostics.Debug.WriteLine("value=[" + value + "]");
			var height = Convert.ToInt32(value.ToString());

			int specWidth = MeasureSpecFactory.MakeMeasureSpec(webView.ContentWidth, MeasureSpecMode.Exactly);
			int specHeight = MeasureSpecFactory.MakeMeasureSpec(height+36, MeasureSpecMode.Exactly);
			webView.Measure(specWidth,specHeight);
			webView.Layout(0,0, webView.MeasuredWidth,webView.MeasuredHeight);

			var bitmap = Bitmap.CreateBitmap(webView.DrawingCache);

			if (!_dir.Exists())
				_dir.Mkdir();
			var path = _dir.Path + "/" + _fileName + ".png";
			var file = new Java.IO.File(path);
			if (!file.Exists())
				file.CreateNewFile();
			var stream = new FileStream(file.Path, FileMode.Create, System.IO.FileAccess.Write);
			bitmap.Compress(Bitmap.CompressFormat.Png, 80, stream);
			stream.Flush();
			stream.Close();
			_onComplete?.Invoke(path);
		}


		public override void OnPageFinished(Android.Webkit.WebView view, string url)
		{
			webView = view as CustomWebView;
			webView.EvaluateJavascript("Math.max( document.body.scrollHeight, document.body.offsetHeight)", this);
		}
	}

	class CustomWebView : Android.Webkit.WebView
	{
		public CustomWebView(Context context) : base(context) { }

		public int ContentWidth
		{
			get
			{
				return base.ComputeHorizontalScrollRange();
			}
		}

		public new int ContentHeight
		{
			get
			{
				return base.ComputeVerticalScrollRange();
			}
		}
	}
}
