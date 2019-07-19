using System;
using StoreHouse.Models;
using Xamarin.Forms;

namespace StoreHouse.UI
{
    public class StoreDataTemplateSelector :DataTemplateSelector
    {
        public DataTemplate StoreItemTemplate { get; set; }
        public DataTemplate StorePlaceTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return item is StoreItem ? StoreItemTemplate : StorePlaceTemplate;
        }
    }
}
