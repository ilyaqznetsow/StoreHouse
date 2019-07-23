using System;
using System.Threading.Tasks;
using Foundation;
using StoreHouse.iOS;
using StoreHouse.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Saver))]

namespace StoreHouse.iOS
{
    public class Saver:ISaver
    {
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
                var stream = bitmap.AsPNG().AsStream();

                byte[] imageData = bitmap.AsPNG().ToArray();


                var chartImage = new UIImage(NSData.FromArray(imageData));
                chartImage.SaveToPhotosAlbum((image, error) =>
                {

                    //you can retrieve the saved UI Image as well if needed using
                    //var i = image as UIImage;
                    if (error != null)
                    {
                        Console.WriteLine(error.ToString());
                    }

                });
                SaveQRComplete.SetResult(true);

            }
            catch (Exception ex)
            {
                SaveQRComplete.SetResult(false);
            }
            return SaveQRComplete.Task.Result;
        }
    }
    }