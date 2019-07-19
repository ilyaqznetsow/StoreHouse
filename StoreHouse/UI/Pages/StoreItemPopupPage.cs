using System;
using StoreHouse.XF.Base.UI;
using Xamarin.Forms;
using XF.Base.Extensions;

namespace StoreHouse.UI.Pages
{
    public class StoreItemPopupPage :BasePopupPage
    {
        public StoreItemPopupPage()
        {
            Content = new StackLayout
            {
                BackgroundColor = Color.White,
                Padding = 10,
                Spacing = 10,
                Children =
                {
                    new Label(){ TextColor = Color.Black}.Bind(Label.TextProperty, "Title"),
                    new Label(){ TextColor = Color.Black}.Bind(Label.TextProperty, "Property1"),
                    new Label(){ TextColor = Color.Black}.Bind(Label.TextProperty, "Property2"),
                    new Label(){ TextColor = Color.Black}.Bind(Label.TextProperty, "Property3"),
                    new Button{Text="OK", TextColor = Color.Black}
                    .BottomExpand()
                    .Bind(Button.TextProperty, "CompleteButtonText")
                    .Bind(Button.CommandProperty, "CompleteCommand")
                }
            };
        }
    }
}
