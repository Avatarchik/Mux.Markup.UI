using System;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// An abstract class that represents rendering properties of <see cref="T:UnityEngine.Canvas" />.
    /// </summary>
    public abstract class Render : Canvas.Modifier
    {
        /// <summary>Backing store for the <see cref="SortingOrder" /> property.</summary>
        public static readonly BindableProperty SortingOrderProperty = BindableProperty.Create(
            "SortingOrder",
            typeof(int),
            typeof(Render),
            0,
            BindingMode.OneWay,
            null,
            OnSortingOrderChanged);

        /// <summary>Backing store for the <see cref="OverrideSorting" /> property.</summary>
        public static readonly BindableProperty OverrideSortingProperty = BindableProperty.Create(
            "OverrideSorting",
            typeof(bool),
            typeof(Render),
            false,
            BindingMode.OneWay,
            null,
            OnOverrideSortingChanged);

        /// <summary>Backing store for the <see cref="TargetDisplay" /> property.</summary>
        public static readonly BindableProperty TargetDisplayProperty = BindableProperty.Create(
            "TargetDisplay",
            typeof(int),
            typeof(Render),
            0,
            BindingMode.OneWay,
            null,
            OnTargetDisplayChanged);

        private static void OnPixelPerfectChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Render)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.pixelPerfect = (bool)state, newValue);
            }
        }

        private static void OnOverridePixelPerfectChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Render)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.overridePixelPerfect = (bool)state, newValue);
            }
        }

        private static void OnWorldCameraChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScreenSpaceCamera)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.worldCamera = (UnityEngine.Camera)state, newValue);
            }
        }

        private static void OnPlaneDistanceChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScreenSpaceCamera)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.planeDistance = (float)state, newValue);
            }
        }

        private static void OnSortingLayerChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScreenSpaceCamera)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.sortingLayerID = (int)state, newValue);
            }
        }

        private static void OnSortingOrderChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Render)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.sortingOrder = (int)state, newValue);
            }
        }

        private static void OnOverrideSortingChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Render)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.overrideSorting = (bool)state, newValue);
            }
        }

        private static void OnTargetDisplayChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Render)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.targetDisplay = (int)state, newValue);
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindableProperty" /> class
        /// that represents <see cref="P:UnityEngine.Canvas.pixelPerfect" />.
        /// </summary>
        protected static BindableProperty CreateBindablePixelPerfectProperty(Type declarer)
        {
            return BindableProperty.Create(
                "PixelPerfect",
                typeof(bool),
                declarer,
                false,
                BindingMode.OneWay,
                null,
                OnPixelPerfectChanged);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindableProperty" /> class
        /// that represents <see cref="P:UnityEngine.Canvas.overridePixelPerfect" />.
        /// </summary>
        protected static BindableProperty CreateBindableOverridePixelPerfectProperty(Type declarer)
        {
            return BindableProperty.Create(
                "OverridePixelPerfect",
                typeof(bool),
                declarer,
                false,
                BindingMode.OneWay,
                null,
                OnOverridePixelPerfectChanged);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindableProperty" /> class
        /// that represents <see cref="P:UnityEngine.Canvas.worldCamera" />.
        /// </summary>
        protected static BindableProperty CreateBindableWorldCameraProperty(Type declarer)
        {
            return BindableProperty.Create(
                "WorldCamera",
                typeof(UnityEngine.Camera),
                declarer,
                null,
                BindingMode.OneWay,
                null,
                OnWorldCameraChanged);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindableProperty" /> class
        /// that represents <see cref="P:UnityEngine.Canvas.planeDistance" />.
        /// </summary>
        protected static BindableProperty CreateBindablePlaneDistanceProperty(Type declarer)
        {
            return BindableProperty.Create(
                "PlaneDistance",
                typeof(float),
                declarer,
                100f,
                BindingMode.OneWay,
                null,
                OnPlaneDistanceChanged);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="BindableProperty" /> class
        /// that represents <see cref="P:UnityEngine.Canvas.sortingLayerID" />.
        /// </summary>
        protected static BindableProperty CreateBindableSortingLayerProperty(Type declarer)
        {
            return BindableProperty.Create(
                "SortingLayer",
                typeof(int),
                declarer,
                0,
                BindingMode.OneWay,
                null,
                OnSortingLayerChanged);
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.Canvas.sortingOrder" />.</summary>
        public int SortingOrder
        {
            get
            {
                return (int)GetValue(SortingOrderProperty);
            }

            set
            {
                SetValue(SortingOrderProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.Canvas.overrideSorting" />.</summary>
        public bool OverrideSorting
        {
            get
            {
                return (bool)GetValue(OverrideSortingProperty);
            }

            set
            {
                SetValue(OverrideSortingProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.Canvas.targetDisplay" />.</summary>
        public int TargetDisplay
        {
            get
            {
                return (int)GetValue(TargetDisplayProperty);
            }

            set
            {
                SetValue(TargetDisplayProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void InitializeComponentInMainThread()
        {
            Component.overrideSorting = OverrideSorting;
            Component.sortingOrder = SortingOrder;
            Component.targetDisplay = TargetDisplay;
        }
    }
}
