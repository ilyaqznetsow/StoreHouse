using System;
using StoreHouse.Behaviors;
using Xamarin.Forms;
using XF.Base.Extensions;
using XF.Base.UI;

namespace StoreHouse.UI.Pages
{
    public class StorePage : BasePage
    {
        public StorePage()
        {
            Content = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                Behaviors = {new InfiniteScrollBehavior()},
                Header = new Label
                {
                    Text = "Header"
                },
                Footer="",
                HasUnevenRows = true,
                ItemTemplate = new DataTemplate(()=>new ViewCell
                {
                    View = new Grid
                    {
                        ColumnDefinitions = new ColumnDefinitionCollection
                        {
                            new ColumnDefinition{Width = GridLength.Star},
                            //new ColumnDefinition{Width = GridLength.Auto}
                        },
                        RowDefinitions = new RowDefinitionCollection
                        {
                            new RowDefinition{Height = GridLength.Auto},
                            new RowDefinition{Height = GridLength.Auto},
                            new RowDefinition{Height = GridLength.Auto},
                            new RowDefinition{Height = GridLength.Auto},
                        },
                        Children =
                        {
                            new Label().Bind(Label.TextProperty, "Name").RowCol(0,0),
                            new Label().Bind(Label.TextProperty, "Type").RowCol(1,0),
                            new Label().Bind(Label.TextProperty, "Place",
                            converter:new FuncConverter<object>(obj=> obj.ToString())).RowCol(2,0),
                            new Label().Bind(Label.TextProperty, "Volume",
                            converter:new FuncConverter<object>(obj=> obj.ToString())).RowCol(3,0),
                        }
                    }
                })
            }.Bind(ListView.ItemsSourceProperty, "StoreCollection");
        }
    }
}

