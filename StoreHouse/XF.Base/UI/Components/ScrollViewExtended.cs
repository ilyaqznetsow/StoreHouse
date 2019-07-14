using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StoreHouse.XF.Base.UI.Components
{
    public enum ScrollDirection
    {
        Close,
        Open
    }

    public enum ScrollState
    {
        Closed,
        Opened,
        Moving
    }

    public class ScrollViewExtended : ScrollView
    {
        protected bool HasBeenAccelerated { get; set; }
        public int CurrentPosition { get; set; }
        public Action PositionChanged;
        protected ScrollDirection CurrentDirection { get; set; }
        protected ScrollState CurrentState { get; set; }
        protected double PrevScrollX { get; set; }
        protected bool IsInteracted { get; set; }
        public event Action TouchStarted;
        public event Action TouchEnded;
        public event Action Flinged;

        public EventHandler OnScrollTouchEnded;

        //  public event EventHandler OnViewScrolled;
        public double MovingWidthMultiplier { get; set; } = 0.33;

        public static BindableProperty IsBouncingEnabledProperty = BindableProperty.Create(nameof(IsBouncingEnabled),
            typeof(bool), typeof(ScrollViewExtended), false);

        public bool IsBouncingEnabled
        {
            get => (bool)GetValue(IsBouncingEnabledProperty);
            set => SetValue(IsBouncingEnabledProperty, value);
        }

        public virtual async Task OnFlingStarted(bool needScroll = true, bool animated = true,
            bool inMainThread = false, View scrollToView = null)
        {
            HasBeenAccelerated = true;
            Flinged?.Invoke();
            if (needScroll)
                using (var task = ScrollToAsync(scrollToView?.X ?? 0, 0, animated))
                {
                    var completionSource = new TaskCompletionSource<bool>();
                    if (inMainThread)
                    {
                        Device.BeginInvokeOnMainThread(async () => {
                            if (task != null) await task;
                            completionSource.SetResult(true);
                        });
                        await completionSource.Task;
                        return;
                    }

                    await task;
                }
        }

        public virtual void OnTouchStarted()
        {
            IsInteracted = true;
            TouchStarted?.Invoke();
            HasBeenAccelerated = false;
        }

        public virtual void OnTouchEnded()
        {
            IsInteracted = false;
            TouchEnded?.Invoke();
        }

        public virtual void OnScrolled(object sender, ScrolledEventArgs args)
        {
            CurrentDirection = Math.Abs(PrevScrollX - ScrollX) < double.Epsilon ?
                CurrentDirection :
                PrevScrollX > ScrollX ?
                ScrollDirection.Close :
                ScrollDirection.Open;
            PrevScrollX = ScrollX;
        }

        protected double GetMovingWidth(double contextWidth)
        {
            return contextWidth * MovingWidthMultiplier;
        }
    }
}
