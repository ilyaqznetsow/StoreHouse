using System;
using System.IO;
using StoreHouse.DAL;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Base.Enums;
using XF.Base.Services;

namespace StoreHouse
{

    public partial class App : Application
    {
        static Database database;
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StoreHouse.db"));
                }
                return database;
            }
        }

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
