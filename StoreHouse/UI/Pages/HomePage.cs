using StoreHouse.ViewModels;
using StoreHouse.XF.Base.UI;
using Xamarin.Forms;

namespace StoreHouse.UI.Pages
{
    public class HomePage : BaseTabbedPage
    {
        public HomePage()
        {
            Title = "StoreHouse app";

            UnselectedTabColor = Color.DimGray;
            SelectedTabColor = Color.DarkGreen;

            this.Children.Add(new GeneratorPage() {
                BindingContext = new GeneratorViewModel(),
                Title="Generator"
            });
            this.Children.Add(new ScannerPage() {
                BindingContext = new ScannerViewModel(),
                Title="Scanner"
            });

            this.Children.Add(new StorePage()
            {
                BindingContext = new StoreViewModel(),
                Title = "Store"
            });
        }
    }
}

