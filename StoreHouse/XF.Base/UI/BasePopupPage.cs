using System;
using System.Threading.Tasks;
using System.Windows.Input;
using StoreHouse.XF.Base.UI.Components;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using XF.Base.Enums;
using XF.Base.Extensions;
using XF.Base.ViewModel;
using ScrollView = Xamarin.Forms.ScrollView;

namespace StoreHouse.XF.Base.UI
{
    public class BasePopupPage : Rg.Plugins.Popup.Pages.PopupPage, IDisposable
    {
        protected BaseViewModel BaseViewModel => BindingContext as BaseViewModel;
        public void Dispose()
        {
            BaseViewModel?.Dispose();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent == null) Dispose();
        }

        public static BindableProperty DismissableContentProperty =
            BindableProperty.Create(nameof(DismissableContent),
                typeof(View), typeof(BasePopupPage));

        public View DismissableContent
        {
            get => (View)GetValue(DismissableContentProperty);
            set => SetValue(DismissableContentProperty, value);
        }

        public static BindableProperty GoBackCommandProperty =
            BindableProperty.Create(nameof(GoBackCommand),
                typeof(ICommand), typeof(BasePopupPage));

        public ICommand GoBackCommand
        {
            get => (ICommand)GetValue(GoBackCommandProperty);
            set => SetValue(GoBackCommandProperty, value);
        }

        public static BindableProperty DisappearedCommandProperty =
            BindableProperty.Create(nameof(DisappearedCommand),
                typeof(ICommand), typeof(BasePopupPage));

        public ICommand DisappearedCommand
        {
            get => (ICommand)GetValue(DisappearedCommandProperty);
            set => SetValue(DisappearedCommandProperty, value);
        }

       

        public BasePopupPage()
        {


            Animation = new PopupPageAnimation();
            IsAnimationEnabled = true;
            CloseWhenBackgroundIsClicked = true;
            SystemPaddingSides = Rg.Plugins.Popup.Enums.PaddingSide.Top;
            HasSystemPadding = true;
            HasKeyboardOffset = false;

            var scroll = new ScrollViewExtended()
            {
                Orientation = ScrollOrientation.Vertical,
                VerticalScrollBarVisibility = ScrollBarVisibility.Never,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Never,
                Padding = new Thickness(0, 0, 0, -1),
                Content = new ContentView() { }
                .Bind(ContentView.ContentProperty, DismissableContentProperty.PropertyName, source: this)
            };
            scroll.On<iOS>().SetShouldDelayContentTouches(true);
            scroll.TouchEnded += async () => {
                if (scroll.ScrollY <= 0)
                {
                    if (Math.Abs(scroll.ScrollY) > scroll.Content.Height * 0.1)
                        GoBackCommand?.Execute(NavigationMode.Popup);
                }
                else
                {
                    await scroll.ScrollToAsync(0, 0, true);
                }
            };


            if (Device.RuntimePlatform == Device.Android)
                scroll.Flinged += () => { GoBackCommand?.Execute(NavigationMode.Popup); };
            Content = scroll;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (GoBackCommand is null)
            {
                GoBackCommand = BaseViewModel?.GoBackPopupCommand;
                 
            }
        }

        protected override void OnAppearing()
        {
            Task.Run(async () => {
                await Task.Delay(50); // Allow UI to handle events loop
                if (BaseViewModel != null) await BaseViewModel.OnPageAppearing();
            });
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            Task.Run(async () => {
                await Task.Delay(50); // Allow UI to handle events loop
                if (BaseViewModel != null) await BaseViewModel.OnPageDisappearing();
            });
            base.OnDisappearing();
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            await base.OnAppearingAnimationEndAsync();
           
        }

        protected override bool OnBackgroundClicked()
        {
            GoBackCommand?.Execute(NavigationMode.Popup);
            return false; // base.OnBackgroundClicked();
        }
    }

    public class BasePopupPage<T> : BasePopupPage where T : BaseViewModel
    {
        public T ViewModel => BaseViewModel as T;
    }
}

