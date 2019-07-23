using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreHouse.Models;
using StoreHouse.Services;
using Xamarin.Forms;
using XF.Base.ViewModel;
using ZXing;
using ZXing.Common;

namespace StoreHouse.ViewModels
{
    public class CodePopupViewModel: BaseViewModel
    {
        public string CodeValue { get; set; }
        public ImageSource CodeSource { get; set; }

        public ICommand SaveCommand => MakeCommand(async() =>
        {
            try
            {
                var barcodeWriter = new BarcodeWriterPixelData
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.Common.EncodingOptions
                    {
                        Width = 1000,
                        Height = 1000,
                        Margin = 10
                    }
                };
                /*
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string filename = Path.Combine(path, $"{CodeValue}.jpg");
                if (Device.RuntimePlatform == Device.iOS)
                    filename = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "..", "Library", $"{CodeValue}.jpg");
                var result = barcodeWriter.Write(CodeValue);
                
                if (!File.Exists(filename))
                    File.Create(filename);
                File.WriteAllBytes(filename, result.Pixels);
                */
                var result =  DependencyService.Get<ISaver>().Save(CodeValue);
                if(result)
                await ShowAlert("Сохранение", "Успех!", "ok");
                else
                    await ShowAlert("Ошибка", "не успехф", "ok");

            }
            catch (Exception ex)
            {
                await ShowAlert("Ошибка", ex.Message, "ok");
            }
        });

        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);
            if (navigationParams == null) return;
            if (navigationParams.ContainsKey("CodeValue"))
                CodeValue = (string)navigationParams["CodeValue"];
       
        }
    }
}
