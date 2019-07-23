using System;
using System.Collections.Generic;
using StoreHouse.Behaviors;
using StoreHouse.Models;
using StoreHouse.XF.Base.Behaviors;
using Xamarin.Forms;
using XF.Base.Extensions;
using XF.Base.UI;

namespace StoreHouse.UI.Pages
{
    public class StorePage : BasePage
    {
        public StorePage()
        {
            ToolbarItems.Add(new ToolbarItem { Text = "Обновить" }
            .Bind(ToolbarItem.CommandProperty, "RefreshCommand"));
            Content = new ListView(ListViewCachingStrategy.RecycleElement)
            {
                IsPullToRefreshEnabled = true,
                Behaviors = {new InfiniteScrollBehavior()},
                SelectionMode = ListViewSelectionMode.None,
                Header =
                new StackLayout
                {
                    Padding = 10,
                    Spacing = 10,
                    Children =
                    {
                        new Label()
                        .Bind(Label.TextProperty, "TotalCount", stringFormat:"Количество записей: {0}", source:BindingContext),
                        new Picker {
                            SelectedIndex = 0,
                            ItemDisplayBinding = new Binding("Title"),
                            ItemsSource = new List<object>
                            {
                                new {Type = typeof(StoreItem), Title = "Предметы"},
                                new {Type = typeof(StorePlace), Title = "Места"},
                            },
                            Title= "Хранилище"
                        }.Bind(Picker.SelectedItemProperty,"SelectedItem")
                    }
                },
                Footer="",
                HasUnevenRows = true,
                ItemTemplate = new StoreDataTemplateSelector
                {
                    StoreItemTemplate = new DataTemplate(() => new ViewCell
                    {
                        View = new Grid
                        {
                            GestureRecognizers = {new TapGestureRecognizer()
                            .Bind(TapGestureRecognizer.CommandProperty,"GoToDetailsCommand", source:this.BindingContext)
                            .Bind(TapGestureRecognizer.CommandParameterProperty,".")},
                            Padding = 10,
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
                            new Label().Bind(Label.TextProperty, "Id").RowCol(0,0),
                            new Label().Bind(Label.TextProperty, "Name").RowCol(1,0),
                            new Label().Bind(Label.TextProperty, "Type").RowCol(2,0),
                            new Label().Bind(Label.TextProperty, "CreationDate").RowCol(3,0),
                        }
                        }
                    }),

                    StorePlaceTemplate = new DataTemplate(() => new ViewCell
                    {
                        View = new Grid
                        {
                            GestureRecognizers = {new TapGestureRecognizer()
                            .Bind(TapGestureRecognizer.CommandProperty,"GoToDetailsCommand", source:this.BindingContext)
                            .Bind(TapGestureRecognizer.CommandParameterProperty,".")},
                            Padding = 10,
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
                            new Label().Bind(Label.TextProperty, "Id").RowCol(0,0),
                            new Label().Bind(Label.TextProperty, "Name").RowCol(1,0),
                            new Label().Bind(Label.TextProperty, "VerticalPosition").RowCol(2,0),
                            new Label().Bind(Label.TextProperty, "HorizontalPosition").RowCol(3,0),
                        }
                        }
                    })
                }
                
            }.Bind(ListView.ItemsSourceProperty, "StoreCollection")
            .Bind(ListView.RefreshCommandProperty , "RefreshCommand")
            .Bind(ListView.IsRefreshingProperty, "IsRefreshing");
        }
    }
}

