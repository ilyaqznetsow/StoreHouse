using System;
using System.IO;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using StoreHouse.Droid;
using StoreHouse.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Saver))]
namespace StoreHouse.Droid
{
    public class Saver:ISaver
    {
        [Obsolete]
        public bool Save(string data)
        {
            var SaveQRComplete = new TaskCompletionSource<bool>();
            try
            {
                var barcodeWriter = new ZXing.Mobile.BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 1000,
                        Height = 1000,
                        Margin = 10
                    }
                };

                barcodeWriter.Renderer = new ZXing.Mobile.BitmapRenderer();
                var bitmap = barcodeWriter.Write(data);
                var stream = new MemoryStream();
                bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
                stream.Position = 0;

                byte[] imageData = stream.ToArray();

                var dir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
                var pictures = dir.AbsolutePath;
                //adding a time stamp time file name to allow saving more than one image... otherwise it overwrites the previous saved image of the same name
                string name = "MY_QR" + System.DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                string filePath = System.IO.Path.Combine(pictures, name);

                System.IO.File.WriteAllBytes(filePath, imageData);
                //mediascan adds the saved image into the gallery
                var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(filePath)));

                Xamarin.Forms.Forms.Context.SendBroadcast(mediaScanIntent);
                SaveQRComplete.SetResult(true);
                return SaveQRComplete.Task.Result;
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
                return false;
            }

        }
    }
}
