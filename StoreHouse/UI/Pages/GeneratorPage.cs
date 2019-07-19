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


                   new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Наименование места"
                    }.Bind(Entry.TextProperty,  "NewPlace.Name", BindingMode.TwoWay),
                    new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Место по вертикали"
                    }.Bind(Entry.TextProperty,  "NewPlace.VerticalPosition", BindingMode.TwoWay),
                    new Entry{
                        Keyboard = Keyboard.Numeric,
                        Placeholder ="Место по горизонтали"
                    }.Bind(Entry.TextProperty,  "NewPlace.HorizontalPosition", BindingMode.TwoWay),

                    new Button
                        {
                            Text = "Генерировать"
                        }.Bind(Button.CommandProperty, "GeneratePlaceCommand"),




                    new Picker() { Title = "Тип детали" }
                    .Bind(Picker.ItemsSourceProperty, "Types")
                    .Bind(Picker.SelectedItemProperty, "NewItem.Type"),
                    new DatePicker { }
                    .Bind(DatePicker.DateProperty, "NewItem.CreationDate"),

                    new Button
                    {
                        Text = "Генерировать деталь"
                    }.Bind(Button.CommandProperty, "GenerateItemCommand"),
                    }
                    }
                 };
        }

        
    }
}

