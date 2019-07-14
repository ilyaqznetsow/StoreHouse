using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreHouse.Models;
using StoreHouse.UI;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class StoreViewModel:BaseViewModel
    {
        public InfiniteScrollCollection<object> StoreCollection { get; set; }

        public StoreViewModel()
        {
            StoreCollection = new InfiniteScrollCollection<object>();
            StoreCollection.OnCanLoadMore = ()=> StoreCollection.Count < 20;
            StoreCollection.OnLoadMore += async () =>
            {
                return await LoadData();
            };
        }

        async Task<List<StoreItem>> LoadData()
        {
            return await Task.Run(() => new List<StoreItem> { new StoreItem
               {
                   AssignDate = DateTime.Now,
                    CreationDate = DateTime.Now.AddDays(-1),
                     Name = "name",
                      Place = new StorePlace
                      {
                           Name="name place 1",
                            HorizontalPosition = 0,
                            VerticalPosition = 1
                      },
                       Type="Коробок",
                        Volume = new Volume
                        {
                            Width = 100,
                            Height = 200,
                            Length = 50
                        }
               }});
        }

        public override async Task OnPageAppearing()
        {
            await  base.OnPageAppearing();
            await StoreCollection.LoadMoreAsync();
        }
    }
}
