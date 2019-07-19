using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreHouse.Models;
using Xamarin.Forms;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class CodePopupViewModel: BaseViewModel
    {
        public string CodeValue { get; set; }
        public ImageSource CodeSource { get; set; }

        public ICommand ShareCommand => MakeCommand(() =>
        {
        });

        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);
            if (navigationParams == null) return;
            if (navigationParams.ContainsKey("CodeValue"))
                CodeValue = (string)navigationParams["CodeValue"];
       
        }
    }
}
