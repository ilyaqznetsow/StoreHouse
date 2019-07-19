using System;
using System.Collections.Generic;
using System.Windows.Input;
using XF.Base.Enums;
using XF.Base.ViewModel;
using ZXing;

namespace StoreHouse.ViewModels
{
    public class ScannerViewModel: BaseViewModel
    {
        public bool IsAnalyzing { get; set; }

        public string ScannerItem { get; set; }
        int StoreItemId { get; set; }
        public ICommand ScanResultCommand => MakeCommand(async (scanResult) =>
         {
             IsAnalyzing = true;
             if(scanResult is Result result)
             {
                 var recognizedObject = new object();
                 if (ScannerItem == "Предмет")
                 {
                     var codeIdString = result.Text;
                     var storeItem = await  App.Database.GetItem(Guid.Parse(codeIdString));
                     StoreItemId = storeItem.Id;
                     recognizedObject = storeItem;
                     ScannerItem = "Место";
                 }

                 if (ScannerItem == "Место")
                 {
                     var placeId = result.Text;
                     recognizedObject = await App.Database.GetPlace(int.Parse(placeId));
                    
                     ScannerItem = "Предмет";
                 }

                 await NavigateTo(Pages.StoreItemPopup, Pages.Scanner, NavigationMode.Popup,
                        navParams: new Dictionary<string, object> { { "Object", recognizedObject },
                            {"StoreItemId", StoreItemId } });
             }
             IsAnalyzing = false;
         });

        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);

            ScannerItem = "Предмет";
        }
    }
}
