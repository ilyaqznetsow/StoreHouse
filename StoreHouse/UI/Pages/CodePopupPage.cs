﻿using System;
using StoreHouse.XF.Base.UI;
using Xamarin.Forms;
using XF.Base.Extensions;
using ZXing.Net.Mobile.Forms;

namespace StoreHouse.UI.Pages
{
    public class CodePopupPage : BasePopupPage
    {
      
        public CodePopupPage()
        {
            DismissableContent = new StackLayout
            {
                BackgroundColor = Color.White,
                Children = {
                    new Label { Text = "Result" }.Bind(Label.TextProperty, "CodeValue"),
                    new ZXing.Net.Mobile.Forms.ZXingBarcodeImageView
                        {
                            BackgroundColor = Color.Gray,
                            BarcodeFormat = ZXing.BarcodeFormat.QR_CODE,
                            BarcodeOptions = new ZXing.Common.EncodingOptions
                            {
                                Height = 300,
                                Width = 300,
                                Margin = 10
                            },
                            WidthRequest = 300,
                            HeightRequest = 300
                        }
                    .CenterExpand().Bind(ZXingBarcodeImageView.BarcodeValueProperty, "CodeValue"),
                     new Button{Text="OK"}.Bind(Button.CommandProperty, GoBackCommandProperty.PropertyName, source:this)
        }
            };
        }
    }
}

