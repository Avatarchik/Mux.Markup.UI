using System;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Slider" />.</summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <!--
    ///         You have to give property name "Path" to Binding and "Name" to x:Reference
    ///         only when you compile the interpreter with IL2CPP.
    ///         It is because ContentPropertyAttribute does not work with IL2CPP.
    ///
    ///         Note that mu:Slider.FillRect is bound to Component.transform of a graphic
    ///         component. It ensures a graphic already exists when the value is resolved,
    ///         which is an undocumented prerequisite of UnityEngine.UI.Slider.fillRect.
    ///     -->
    ///     <mu:Slider
    ///         FillRect="{Binding Path=Component.transform, Source={x:Reference Name=fill}}"
    ///         HandleRect="{Binding Path=Component, Source={x:Reference Name=handle}}"
    ///         TargetGraphic="{Binding Path=Component, Source={x:Reference Name=targetGraphic}}" />
    ///     <m:RectTransform X="{m:Stretch OffsetMin=2, OffsetMax=-2}">
    ///         <m:RectTransform X="{m:Sized SizeDelta=4}" Y="{m:Stretch}">
    ///             <mu:Image x:Name="fill" Color="{m:Color R=0, G=0, B=1}" />
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    ///     <m:RectTransform X="{m:Stretch OffsetMin=4, OffsetMax=-4}">
    ///         <m:RectTransform x:Name="handle" X="{m:Sized SizeDelta=8}" Y="{m:Stretch}">
    ///             <mu:Image x:Name="targetGraphic" Color="{m:Color R=0, G=1, B=0}" />
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Slider : Selectable<UnityEngine.UI.Slider>
    {
        [StructLayout(LayoutKind.Auto)]
        private readonly struct BuiltinPrefabs
        {
            public readonly UnityEngine.ResourceRequest fillArea;
            public readonly UnityEngine.ResourceRequest handleSlideArea;

            public BuiltinPrefabs(UnityEngine.ResourceRequest fillArea, UnityEngine.ResourceRequest handleSlideArea)
            {
                this.fillArea = fillArea;
                this.handleSlideArea = handleSlideArea;
            }
        }

        private static Lazy<BuiltinPrefabs> s_builtinPrefabs = new Lazy<BuiltinPrefabs>(LoadBuiltinPrefabs, false);

        private static BuiltinPrefabs LoadBuiltinPrefabs()
        {
            return new BuiltinPrefabs(
                UnityEngine.Resources.LoadAsync("Mux/Slider/Fill Area"),
                UnityEngine.Resources.LoadAsync("Mux/Slider/Handle Slide Area"));
        }

        private UnityEngine.GameObject _builtinFillArea;
        private UnityEngine.GameObject _builtinHandleSlideArea;

        /// <summary>Backing store for the <see cref="FillRect" /> property.</summary>
        public static readonly BindableProperty FillRectProperty = BindableProperty.Create(
            "FillRect",
            typeof(UnityEngine.RectTransform),
            typeof(Slider),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnFillRectChanged);

        /// <summary>Backing store for the <see cref="HandleRect" /> property.</summary>
        public static readonly BindableProperty HandleRectProperty = BindableProperty.Create(
            "HandleRect",
            typeof(UnityEngine.RectTransform),
            typeof(Slider),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnHandleRectChanged);

        /// <summary>Backing store for the <see cref="Direction" /> property.</summary>
        public static readonly BindableProperty DirectionProperty = CreateBindableComponentProperty<UnityEngine.UI.Slider.Direction>(
            "Direction",
            typeof(Slider),
            (component, value) => component.direction = value,
            UnityEngine.UI.Slider.Direction.LeftToRight);

        /// <summary>Backing store for the <see cref="MinValue" /> property.</summary>
        public static readonly BindableProperty MinValueProperty = CreateBindableComponentProperty<float>(
            "MinValue",
            typeof(Slider),
            (component, value) => component.minValue = value,
            0f);

        /// <summary>Backing store for the <see cref="MaxValue" /> property.</summary>
        public static readonly BindableProperty MaxValueProperty = CreateBindableComponentProperty<float>(
            "MaxValue",
            typeof(Slider),
            (component, value) => component.maxValue = value,
            1f);

        /// <summary>Backing store for the <see cref="WholeNumbers" /> property.</summary>
        public static readonly BindableProperty WholeNumbersProperty = CreateBindableComponentProperty<bool>(
            "WholeNumbers",
            typeof(Slider),
            (component, value) => component.wholeNumbers = value,
            false);

        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = CreateBindableComponentProperty<float>(
            "Value",
            typeof(Slider),
            (component, value) =>
            {
                var old = component.onValueChanged;
                component.onValueChanged = new UnityEngine.UI.Slider.SliderEvent();

                try
                {
                    component.value = value;
                }
                finally
                {
                    component.onValueChanged = old;
                }
            },
            0f,
            BindingMode.TwoWay);

        private static void OnFillRectChanged(BindableObject boxedSlider, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var slider = (Slider)state;

                if (slider.FillRect == slider._builtinFillArea.transform.GetChild(0))
                {
                    slider._builtinFillArea.hideFlags = UnityEngine.HideFlags.None;

                    if (slider.Component != null)
                    {
                        slider._builtinFillArea.transform.SetParent(slider.Component.transform, false);
                        slider._builtinFillArea.layer = slider.Component.gameObject.layer;
                        slider.FillRect.gameObject.layer = slider._builtinFillArea.layer;
                    }
                }
                else if (slider.FillRect != null)
                {
                    // This clause will only executed if the value is not null because
                    // null causes NullReferenceException in uGUI.

                    slider._builtinFillArea.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (slider.Component != null)
                    {
                        slider._builtinFillArea.transform.SetParent(null);
                        slider.Component.fillRect = slider.FillRect;
                    }
                }
            }, boxedSlider);
        }

        private static void OnHandleRectChanged(BindableObject boxedSlider, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var slider = (Slider)state;
                var builtinHandleRect = slider._builtinHandleSlideArea.transform.GetChild(0);

                if (slider.HandleRect == builtinHandleRect)
                {
                    slider._builtinHandleSlideArea.hideFlags = UnityEngine.HideFlags.None;

                    if (slider.Component != null)
                    {
                        slider._builtinHandleSlideArea.transform.SetParent(slider.Component.transform, false);
                        slider._builtinHandleSlideArea.layer = slider.Component.gameObject.layer;
                        slider.HandleRect.gameObject.layer = slider._builtinHandleSlideArea.layer;
                        slider.Component.handleRect = slider.HandleRect;
                    }
                }
                else
                {
                    slider._builtinHandleSlideArea.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (slider.TargetGraphic == builtinHandleRect.gameObject.GetComponent<UnityEngine.UI.Image>())
                    {
                        slider.SetValueCore(TargetGraphicProperty, null);
                    }

                    if (slider.Component != null)
                    {
                        slider._builtinHandleSlideArea.transform.SetParent(null);
                        slider.Component.handleRect = slider.HandleRect;
                    }
                }
            }, boxedSlider);
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.fillRect" />.</summary>
        /// <seealso cref="RectTransform" />
        public UnityEngine.RectTransform FillRect
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(FillRectProperty);
            }

            set
            {
                SetValue(FillRectProperty, value);
            }
        }

        /// <summary>A property that represents <see href="P:UnityEngine.UI.Slider.handleRect" />.</summary>
        /// <seealso cref="RectTransform" />
        public UnityEngine.RectTransform HandleRect
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(HandleRectProperty);
            }

            set
            {
                SetValue(HandleRectProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.direction" />.</summary>
        public UnityEngine.UI.Slider.Direction Direction
        {
            get
            {
                return (UnityEngine.UI.Slider.Direction)GetValue(DirectionProperty);
            }

            set
            {
                SetValue(DirectionProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.minValue" />.</summary>
        public float MinValue
        {
            get
            {
                return (float)GetValue(MinValueProperty);
            }

            set
            {
                SetValue(MinValueProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.maxValue" />.</summary>
        public float MaxValue
        {
            get
            {
                return (float)GetValue(MaxValueProperty);
            }

            set
            {
                SetValue(MaxValueProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.wholeNumbers" />.</summary>
        public bool WholeNumbers
        {
            get
            {
                return (bool)GetValue(WholeNumbersProperty);
            }

            set
            {
                SetValue(WholeNumbersProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Slider.value" />.</summary>
        public float Value
        {
            get
            {
                return (float)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public Slider()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinFillArea = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinPrefabs.Value.fillArea.asset);
                _builtinHandleSlideArea = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinPrefabs.Value.handleSlideArea.asset);
                SetValueCore(FillRectProperty, (UnityEngine.RectTransform)_builtinFillArea.transform.GetChild(0));
                SetValueCore(HandleRectProperty, (UnityEngine.RectTransform)_builtinHandleSlideArea.transform.GetChild(0));
                SetValueCore(TargetGraphicProperty, _builtinHandleSlideArea.GetComponentInChildren<UnityEngine.UI.Image>());
            }, null);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            if (FillRect == _builtinFillArea.transform.GetChild(0))
            {
                _builtinFillArea.transform.SetParent(gameObject.transform, false);
                _builtinFillArea.layer = gameObject.layer;
                FillRect.gameObject.layer = _builtinFillArea.layer;
            }

            if (HandleRect == _builtinHandleSlideArea.transform.GetChild(0))
            {
                _builtinHandleSlideArea.transform.SetParent(gameObject.transform, false);
                _builtinHandleSlideArea.layer = gameObject.layer;
                HandleRect.gameObject.layer = _builtinHandleSlideArea.layer;
            }

            Component.fillRect = FillRect;
            Component.handleRect = HandleRect;
            Component.direction = Direction;
            Component.minValue = MinValue;
            Component.maxValue = MaxValue;
            Component.wholeNumbers = WholeNumbers;
            Component.value = Value;
            Component.onValueChanged.AddListener(newValue => SetValueCore(ValueProperty, newValue));
        }
    }
}
