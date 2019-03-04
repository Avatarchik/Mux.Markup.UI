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
    ///     -->
    ///     <mu:Slider
    ///         FillRect="{Binding Path=Body, Source={x:Reference Name=fill}}"
    ///         HandleRect="{Binding Path=Body, Source={x:Reference Name=handle}}"
    ///         TargetGraphic="{Binding Path=Body, Source={x:Reference Name=targetGraphic}}" />
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
            public readonly UnityEngine.GameObject fillArea;
            public readonly UnityEngine.GameObject handleSlideArea;

            public BuiltinPrefabs(UnityEngine.GameObject fillArea, UnityEngine.GameObject handleSlideArea)
            {
                this.fillArea = fillArea;
                this.handleSlideArea = handleSlideArea;
            }
        }

        private static Lazy<BuiltinPrefabs> s_builtinPrefabs = new Lazy<BuiltinPrefabs>(LoadBuiltinPrefabs, false);

        private static BuiltinPrefabs LoadBuiltinPrefabs()
        {
            return new BuiltinPrefabs(
                UnityEngine.Resources.Load<UnityEngine.GameObject>("Mux/Slider/Fill Area"),
                UnityEngine.Resources.Load<UnityEngine.GameObject>("Mux/Slider/Handle Slide Area"));
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
        public static readonly BindableProperty DirectionProperty = CreateBindableBodyProperty<UnityEngine.UI.Slider.Direction>(
            "Direction",
            typeof(Slider),
            (body, value) => body.direction = value,
            UnityEngine.UI.Slider.Direction.LeftToRight);

        /// <summary>Backing store for the <see cref="MinValue" /> property.</summary>
        public static readonly BindableProperty MinValueProperty = CreateBindableBodyProperty<float>(
            "MinValue",
            typeof(Slider),
            (body, value) => body.minValue = value,
            0f);

        /// <summary>Backing store for the <see cref="MaxValue" /> property.</summary>
        public static readonly BindableProperty MaxValueProperty = CreateBindableBodyProperty<float>(
            "MaxValue",
            typeof(Slider),
            (body, value) => body.maxValue = value,
            1f);

        /// <summary>Backing store for the <see cref="WholeNumbers" /> property.</summary>
        public static readonly BindableProperty WholeNumbersProperty = CreateBindableBodyProperty<bool>(
            "WholeNumbers",
            typeof(Slider),
            (body, value) => body.wholeNumbers = value,
            false);

        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = CreateBindableBodyProperty<float>(
            "Value",
            typeof(Slider),
            (body, value) =>
            {
                var old = body.onValueChanged;
                body.onValueChanged = new UnityEngine.UI.Slider.SliderEvent();

                try
                {
                    body.value = value;
                }
                finally
                {
                    body.onValueChanged = old;
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

                    if (slider.Body != null)
                    {
                        slider._builtinFillArea.transform.SetParent(slider.Body.transform, false);
                        slider._builtinFillArea.layer = slider.Body.gameObject.layer;
                        slider.FillRect.gameObject.layer = slider._builtinFillArea.layer;
                    }
                }
                else if (slider.FillRect != null)
                {
                    // This clause will only executed if the value is not null because
                    // null causes NullReferenceException in uGUI.

                    slider._builtinFillArea.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (slider.Body != null)
                    {
                        slider._builtinFillArea.transform.SetParent(null);
                        slider.Body.fillRect = slider.FillRect;
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

                    if (slider.Body != null)
                    {
                        slider._builtinHandleSlideArea.transform.SetParent(slider.Body.transform, false);
                        slider._builtinHandleSlideArea.layer = slider.Body.gameObject.layer;
                        slider.HandleRect.gameObject.layer = slider._builtinHandleSlideArea.layer;
                        slider.Body.handleRect = slider.HandleRect;
                    }
                }
                else
                {
                    slider._builtinHandleSlideArea.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (slider.TargetGraphic == builtinHandleRect.gameObject.GetComponent<UnityEngine.UI.Image>())
                    {
                        slider.SetValueCore(TargetGraphicProperty, null);
                    }

                    if (slider.Body != null)
                    {
                        slider._builtinHandleSlideArea.transform.SetParent(null);
                        slider.Body.handleRect = slider.HandleRect;
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
                _builtinFillArea = UnityEngine.Object.Instantiate(s_builtinPrefabs.Value.fillArea);
                _builtinHandleSlideArea = UnityEngine.Object.Instantiate(s_builtinPrefabs.Value.handleSlideArea);
                SetValueCore(FillRectProperty, (UnityEngine.RectTransform)_builtinFillArea.transform.GetChild(0));
                SetValueCore(HandleRectProperty, (UnityEngine.RectTransform)_builtinHandleSlideArea.transform.GetChild(0));
                SetValueCore(TargetGraphicProperty, _builtinHandleSlideArea.GetComponentInChildren<UnityEngine.UI.Image>());
            }, null);
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();

            if (FillRect == _builtinFillArea.transform.GetChild(0))
            {
                _builtinFillArea.transform.SetParent(Body.transform, false);
                _builtinFillArea.layer = Body.gameObject.layer;
                FillRect.gameObject.layer = _builtinFillArea.layer;
            }

            if (HandleRect == _builtinHandleSlideArea.transform.GetChild(0))
            {
                _builtinHandleSlideArea.transform.SetParent(Body.transform, false);
                _builtinHandleSlideArea.layer = Body.gameObject.layer;
                HandleRect.gameObject.layer = _builtinHandleSlideArea.layer;
            }

            Body.fillRect = FillRect;
            Body.handleRect = HandleRect;
            Body.direction = Direction;
            Body.minValue = MinValue;
            Body.maxValue = MaxValue;
            Body.wholeNumbers = WholeNumbers;
            Body.value = Value;
            Body.onValueChanged.AddListener(newValue => SetValueCore(ValueProperty, newValue));
        }
    }
}
