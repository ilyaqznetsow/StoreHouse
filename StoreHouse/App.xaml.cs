using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Base.Enums;
using XF.Base.Services;

namespace StoreHouse
{

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DialogService.Init(this);
            NavigationService.Init(Pages.Home);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
