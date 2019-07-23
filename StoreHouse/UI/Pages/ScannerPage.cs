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
            .Bind(ZXingScannerView.IsAnalyzingProperty, "IsAnalyzing");


            overlay = new ZXingDefaultOverlay
            {
                TopText = "Наведите на",
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
            grid.Children.Add(new Label {
                Margin = new Thickness(0,0,0,40),
                TextColor = Color.White }.CenterH()
            .Bind(Label.TextProperty, "ScannerItem").Bottom());
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

