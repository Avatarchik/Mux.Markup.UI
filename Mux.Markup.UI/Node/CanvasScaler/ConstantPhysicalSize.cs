using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="T:UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize" />
    /// and its scaling properties.
    /// </summary>
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
            var component = ((ConstantPhysicalSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(
                    state => component.physicalUnit = (UnityEngine.UI.CanvasScaler.Unit)state,
                    newValue);
            }
        }

        private static void OnFallbackScreenDPIChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ConstantPhysicalSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.fallbackScreenDPI = (float)state, newValue);
            }
        }

        private static void OnDefaultSpriteDPIChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ConstantPhysicalSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.defaultSpriteDPI = (float)state, newValue);
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
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPhysicalSize;
            Component.physicalUnit = PhysicalUnit;
            Component.fallbackScreenDPI = FallbackScreenDPI;
            Component.defaultSpriteDPI = DefaultSpriteDPI;
        }
    }
}
