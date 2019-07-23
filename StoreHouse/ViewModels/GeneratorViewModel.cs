using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StoreHouse.DAL;
using StoreHouse.Models;
using Xamarin.Forms;
using XF.Base.Enums;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class GeneratorViewModel : BaseViewModel
    {
        public List<string> Types { get; set; } = new List<string>
        {
            "Коробка",
            "Конверт",
            "Пакет",
            "Кружка"
        };

        

        public StoreItem NewItem { get; set; } = new StoreItem() { };
        public StorePlace NewPlace { get; set; } = new StorePlace() { };

        public ObservableCollection<StoreItem> StoreItems { get; set; }

        public GeneratorViewModel()
        {
            StoreItems = new ObservableCollection<StoreItem>();
        }

        public ICommand GenerateItemCommand => MakeCommand(async () =>
        {
            var code = Guid.NewGuid();
            NewItem.CodeId = code;
            NewItem.PlaceId = NewPlace.Id;
            NewItem.Id = await App.Database.SaveAsync(NewItem);


            await NavigateTo(Pages.CodePopup, Pages.Generator, NavigationMode.Popup,
                navParams: new Dictionary<string, object> {
                    {"CodeValue", code.ToString() },
                }) ;
        });

        public ICommand GeneratePlaceCommand => MakeCommand(async() =>
        {
            NewPlace.Id = await App.Database.SaveAsync(NewPlace);
            NewItem.PlaceId = await App.Database.GetCountAsync(typeof(StorePlace));


            await NavigateTo(Pages.CodePopup, Pages.Generator, NavigationMode.Popup,
                navParams: new Dictionary<string, object> {
                    {"CodeValue", NewItem.PlaceId.ToString()  },
                });
        });

       
        
    }
}
