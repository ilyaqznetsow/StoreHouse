using System;
using StoreHouse.iOS;
using StoreHouse.XF.Base.UI.Components;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ScrollViewExtended), typeof(ScrollViewExtendedRenderer))]

namespace StoreHouse.iOS
{
    [Preserve(AllMembers = true)]
    public class ScrollViewExtendedRenderer : ScrollViewRenderer
    {
        public static void Initialize() { }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                ShowsHorizontalScrollIndicator = false;
                ShowsVerticalScrollIndicator = false;

                DraggingEnded -= OnDraggingEnded;
                DraggingStarted -= OnDraggingStarted;

                DraggingEnded += OnDraggingEnded;
                DraggingStarted += OnDraggingStarted;

                DecelerationStarted -= OnDecelerationStarted;
                DecelerationStarted += OnDecelerationStarted;

                this.ScrollsToTop = true;

                this.Scrolled += OnScrolled;
                DecelerationRate = DecelerationRateFast;
                Bounces = ((ScrollViewExtended)Element).IsBouncingEnabled;
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                    ContentInsetAdjustmentBehavior = UIKit.UIScrollViewContentInsetAdjustmentBehavior.Never;

            }
        }

        private void OnScrolled(object sender, EventArgs e)
        {
            if (Element is ScrollViewExtended scroll)
            {
                Bounces = ContentOffset.Y <= 0 && scroll.IsBouncingEnabled;

                scroll.OnScrolled(sender, new ScrolledEventArgs(0, ContentOffset.Y));
            }
        }

        private void OnDecelerationStarted(object sender, EventArgs e)
        {
            (Element as ScrollViewExtended)?.OnFlingStarted();
        }

        private void OnDraggingEnded(object sender, DraggingEventArgs e)
        {
            (Element as ScrollViewExtended)?.OnTouchEnded();
        }

        private void OnDraggingStarted(object sender, EventArgs e)
        {
            (Element as ScrollViewExtended)?.OnTouchStarted();
        }
    }
}
