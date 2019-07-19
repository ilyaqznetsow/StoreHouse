﻿using System;
using Xamarin.Forms;

namespace StoreHouse.XF.Base.Behaviors
{
   
        public class BaseBehavior<T> : Behavior<T>
       where T : BindableObject
        {
            public T AssociatedObject { get; private set; }

            protected override void OnAttachedTo(T bindable)
            {
                base.OnAttachedTo(bindable);
                AssociatedObject = bindable;

                if (bindable.BindingContext != null)
                {
                    BindingContext = bindable.BindingContext;
                }

                bindable.BindingContextChanged += OnBindingContextChanged;
            }

            protected override void OnDetachingFrom(T bindable)
            {
                base.OnDetachingFrom(bindable);
                bindable.BindingContextChanged -= OnBindingContextChanged;
                AssociatedObject = null;
            }

            protected override void OnBindingContextChanged()
            {
                base.OnBindingContextChanged();
                BindingContext = AssociatedObject.BindingContext;
            }

            void OnBindingContextChanged(object sender, EventArgs e)
            {
                OnBindingContextChanged();
            }
        }
    
}
