using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreHouse.Models;
using StoreHouse.UI;
using XF.Base.Enums;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class StoreViewModel:BaseViewModel
    {
        public InfiniteScrollCollection<object> StoreCollection { get; set; }
        int CollectionSize = 100;
        public dynamic SelectedItem { get; set; }

        public StoreViewModel()
        {
            StoreCollection = new InfiniteScrollCollection<object>();
            StoreCollection.OnCanLoadMore = ()=> StoreCollection.Count < TotalCount;
            StoreCollection.OnLoadMore += async () =>
            {
                return await LoadData(SelectedItem.Type);
            };
        }
        public bool IsRefreshing { get; set; }
        public ICommand RefreshCommand => MakeCommand(async () =>
        {
            if(SelectedItem == null)
            {
              await  ShowAlert("Ошибка", "Сначала выберите что смотрим","понял");
                return;
            }
            IsRefreshing = true;
            StoreCollection.Clear();
            TotalCount = await LoadCount(SelectedItem?.Type ?? typeof(StoreItem));
            await StoreCollection.LoadMoreAsync();
            IsRefreshing = false;
        });

        public ICommand GoToDetailsCommand =>
            MakeCommand(async (item) =>
            {
                if (item != null)
                {
                    await NavigateTo(Pages.StoreItemPopup, Pages.Store, NavigationMode.Popup,
                navParams: new Dictionary<string, object> { { "Object", item } });
                }
            });

        async Task<List<object>> LoadData(Type t)
        {
            if(t != null)
            return await App.Database.GetAsync(t);
            return null;
        }

        async Task<int> LoadCount(Type t)
        {
            if (t != null)
                return await App.Database.GetCountAsync(t);
            return 0;
        }

        public int TotalCount { get; set; }
    }
}
