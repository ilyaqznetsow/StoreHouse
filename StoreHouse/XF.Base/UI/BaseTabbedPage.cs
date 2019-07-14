using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF.Base.ViewModel;

namespace StoreHouse.XF.Base.UI
{
    public class BaseTabbedPage: TabbedPage, IDisposable
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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () => {
            await Task.Delay(50); // Allow UI to handle events loop
            if (BaseViewModel != null) await BaseViewModel.OnPageAppearing();
        });
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        Task.Run(async () => {
            await Task.Delay(50); // Allow UI to handle events loop
            if (BaseViewModel != null) await BaseViewModel.OnPageDisappearing();
        });
    }
}

public class BaseTabbedPage<T> : BaseTabbedPage where T : BaseViewModel
{
    public T ViewModel => BaseViewModel as T;
}
}
