using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XF.Base.ViewModel;

namespace StoreHouse.ViewModels
{
    public class CodePopupViewModel: BaseViewModel
    {
        public string CodeValue { get; set; }

        public override void OnSetNavigationParams(Dictionary<string, object> navigationParams)
        {
            base.OnSetNavigationParams(navigationParams);
            if (navigationParams == null) return;
            if (navigationParams.ContainsKey("CodeValue"))
                CodeValue = (string)navigationParams["CodeValue"];
        }
    }
}
