using System;

using Xamarin.Forms;
using XF.Base.Extensions;
using XF.Base.UI;
using ZXing.Net.Mobile.Forms;

namespace StoreHouse.UI.Pages
{
    public class ScannerPage : BasePage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        public ScannerPage()
        {
            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingScannerView",
            }.Bind(ZXingScannerView.ScanResultCommandProperty, "ScanResultCommand")
            .Bind(ZXingScannerView.IsAnalyzingProperty, "IsAnalyzing")
            .Bind(ZXingScannerView.IsScanningProperty, "IsScanning");

            
            overlay = new ZXingDefaultOverlay
            {
                TopText = "Сканирование",
                BottomText = "Места",
                ShowFlashButton = zxing.HasTorch,
                AutomationId = "zxingDefaultOverlay",
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }
    }
}

