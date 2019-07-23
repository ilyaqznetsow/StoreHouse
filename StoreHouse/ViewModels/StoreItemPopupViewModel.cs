using System;
using System.Collections.Generic;
using System.Windows.Input;
using StoreHouse.Models;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class StoreItemPopupViewModel:BaseViewModel
    {
        public string Title { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string CompleteButtonText { get; set; }

         object CurrentItem { get; set; }
        StoreItem StoreItem { get; set; }

        public ICommand CompleteCommand => MakeCommand(async () =>
        {
            if (CurrentItem is StorePlace place && StoreItem != null)
            {
                place.CurrentlyLockedBy = StoreItem.Id;
                await App.Database.SaveAsync(CurrentItem);
            }

            GoBackPopupCommand?.Execute(null);
        });


        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);
            if (navigationParams == null) return;
            if (navigationParams.ContainsKey("Object"))
            {
                CurrentItem = navigationParams["Object"];
            }
            if (navigationParams.ContainsKey("StoreItem"))
            {
                StoreItem = (StoreItem)navigationParams["StoreItem"];
            }

            if (CurrentItem is StoreItem storeItem)
            {
                Title = storeItem.Name;
                Property1 = storeItem.Type;
                Property2 = storeItem.CreationDate.ToString();
                Property3 = storeItem.AssignDate.ToString();
                CompleteButtonText = "Вижу предмет";
            }

            if(CurrentItem is StorePlace storePlace)
            {
                Title = storePlace.Name;
                Property1 = storePlace.VerticalPosition.ToString();
                Property2 = storePlace.HorizontalPosition.ToString();
                Property3 = storePlace.CurrentlyLockedBy.ToString();
                CompleteButtonText = "Подтверждаю место";
            }
        }
    }
}
