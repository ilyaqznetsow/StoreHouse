using System;
using XF.Base.Extensions;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using System.Windows.Input;

namespace StoreHouse.UI.Pages
{
    public class GeneratorPage : ContentPage
    {
        

        public GeneratorPage()
        {
           
          

            Content =
                 new ScrollView()
                 {
                     Content =

                new StackLayout()
                {
                    Children = {
                    new Picker (){ Title="Тип детали"}
                    .Bind(Picker.ItemsSourceProperty,"Types")
                    .Bind(Picker.SelectedItemProperty, "NewItem.Type"),
                    new Picker (){ Title="Место"}
                    .Bind(Picker.ItemsSourceProperty,"Places")
                    .Bind(Picker.SelectedItemProperty, "NewItem.Place"),
                    new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Ширина"
                    }.Bind(Entry.TextProperty,  "NewItem.Volume.Width", BindingMode.TwoWay),
                    new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Высота"
                    }.Bind(Entry.TextProperty,  "NewItem.Volume.Height", BindingMode.TwoWay),
                    new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Длина"
                    }.Bind(Entry.TextProperty,  "NewItem.Volume.Length", BindingMode.TwoWay),
                    new Button
            {
                Text = "Генерировать"
            }.Bind(Button.CommandProperty, "GenerateCommand")

                    }
                }
                 };
        }

        
    }
}

