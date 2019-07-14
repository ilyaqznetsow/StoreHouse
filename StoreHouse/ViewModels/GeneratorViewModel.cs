using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
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

        

        public StoreItem NewItem { get; set; } = new StoreItem() { Volume = new Volume()};

        public List<StorePlace> Places { get; set; } = new List<StorePlace>
        {
             new StorePlace{ Id = Guid.NewGuid(), Name ="name 1", VerticalPosition = 1, HorizontalPosition = 1},
             new StorePlace{ Id = Guid.NewGuid(), Name ="name 2", VerticalPosition = 2, HorizontalPosition = 1},
             new StorePlace{ Id = Guid.NewGuid(), Name ="name 3", VerticalPosition = 3, HorizontalPosition = 1},
             new StorePlace{ Id = Guid.NewGuid(), Name ="name 4", VerticalPosition = 4, HorizontalPosition = 1},
        };

        public string CodeValue { get; set; } = "asd";

        public ObservableCollection<StoreItem> StoreItems { get; set; }

        public GeneratorViewModel()
        {
            StoreItems = new ObservableCollection<StoreItem>();
        }


        public ICommand GenerateCommand => MakeCommand(async() =>
        {

            CodeValue = NewItem.Id.ToString();

            await NavigateTo(Pages.CodePopup, Pages.Generator, NavigationMode.Popup,
                navParams: new Dictionary<string, object> {
                    {"CodeValue", CodeValue }
                });
        });

       
        
    }
}
