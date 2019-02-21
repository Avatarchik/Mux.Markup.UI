using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="T:UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize" />
    /// and its scaling properties.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:Text>
    ///         <mu:Text.Content>
    /// > Using the Constant Physical Size mode, positions and sizes of UI elements are specified in physical units,
    /// > such as millimeters, points, or picas.
    /// Unity - Scripting API: UI.CanvasScaler.ScaleMode.ConstantPhysicalSize
    /// https://docs.unity3d.com/ScriptReference/UI.CanvasScaler.ScaleMode.ConstantPhysicalSize.html
    ///         </mu:Text.Content>
    ///     </mu:Text>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class ConstantPhysicalSize : CanvasScaler.Modifier
    {
        /// <summary>Backing store for the <see cref="PhysicalUnit" /> property.</summary>
        public static readonly BindableProperty PhysicalUnitProperty = BindableProperty.Create(
            "PhysicalUnit",
            typeof(UnityEngine.UI.CanvasScaler.Unit),
            typeof(ConstantPhysicalSize),
            UnityEngine.UI.CanvasScaler.Unit.Points,
            BindingMode.OneWay,
            null,
            OnPhysicalUnitChanged);

        /// <summary>Backing store for the <see cref="FallbackScreenDPI" /> property.</summary>
        public static readonly BindableProperty FallbackScreenDPIProperty = BindableProperty.Create(
            "FallbackScreenDPI",
            typeof(float),
            typeof(ConstantPhysicalSize),
            96f,
            BindingMode.OneWay,
            null,
            OnFallbackScreenDPIChanged);

        /// <summary>Backing store for the <see cref="DefaultSpriteDPI" /> property.</summary>
        public static readonly BindableProperty DefaultSpriteDPIProperty = BindableProperty.Create(
            "DefaultSpriteDPI",
            typeof(float),
            typeof(ConstantPhysicalSize),
            96f,
            BindingMode.OneWay,
            null,
            OnDefaultSpriteDPIChanged);

        private static void OnPhysicalUnitChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((ConstantPhysicalSize)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(
                    state => body.physicalUnit = (UnityEngine.UI.CanvasScaler.Unit)state,
                    newValue);
            }
        }

        private static void OnFallbackScreenDPIChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((ConstantPhysicalSize)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.fallbackScreenDPI = (float)state, newValue);
            }
        }

        private static void OnDefaultSpriteDPIChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((ConstantPhysicalSize)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.defaultSpriteDPI = (float)state, newValue);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.physicalUnit" />.
        /// </summary>
        public UnityEngine.UI.CanvasScaler.Unit PhysicalUnit
        {
            get
            {
                return (UnityEngine.UI.CanvasScaler.Unit)GetValue(PhysicalUnitProperty);
            }

            set
            {
                SetValue(PhysicalUnitProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.fallbackScreenDPI" />.
        /// </summary>
        public float FallbackScreenDPI
        {
            get
            {
                return (float)GetValue(FallbackScreenDPIProperty);
            }

            set
            {
                SetValue(FallbackScreenDPIProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.defaultSpriteDPI" />.
        /// </summary>
        public float DefaultSpriteDPI
        {
            get
            {
                return (float)GetValue(DefaultSpriteDPIProperty);
            }

            set
            {
                SetValue(DefaultSpriteDPIProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize;
            Body.physicalUnit = PhysicalUnit;
            Body.fallbackScreenDPI = FallbackScreenDPI;
            Body.defaultSpriteDPI = DefaultSpriteDPI;
        }
    }
}
