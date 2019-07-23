using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreHouse.Models;
using XF.Base.Enums;
using XF.Base.ViewModel;
using ZXing;

namespace StoreHouse.ViewModels
{
    public class ScannerViewModel: BaseViewModel
    {
        public bool IsAnalyzing { get; set; }

        public string ScannerItem { get; set; } = "Предмет";
        StoreItem StoreItem { get; set; }
        public ICommand ScanResultCommand => MakeCommand(async (scanResult) =>
         {
             IsAnalyzing = true;
             if(scanResult is Result result)
             {
                 if (ScannerItem == "Предмет")
                 {
                     var codeIdString = result.Text;
                     if (Guid.TryParse(codeIdString, out Guid guid))
                     {
                         StoreItem = await App.Database.GetItem(guid);
                         if (StoreItem != null)
                         {
                             await ShowAlert("успех", "Предмет опознан", "ok");
                             ScannerItem = "Место";
                         }
                         else await ShowAlert("не успех", "Предмет не опознан", "ok");

                     }
                     else await ShowAlert("Ошибка", "Не удалось распознать предмет", "ok");
                 }

                 if (ScannerItem == "Место")
                 {
                     var placeId = result.Text;
                     if (int.TryParse(placeId, out int id))
                     {
                         var place = await App.Database.GetPlace(id);
                         await NavigateTo(Pages.StoreItemPopup, Pages.Scanner, NavigationMode.Popup,
                      navParams: new Dictionary<string, object> { { "Object", place },
                            {"StoreItem", StoreItem } });
                         ScannerItem = "Предмет";

                     }
                     else await ShowAlert("Ошибка", "Не удалось распознать место", "ok");
                 }

               
             }
             IsAnalyzing = false;
         });

    }
}
