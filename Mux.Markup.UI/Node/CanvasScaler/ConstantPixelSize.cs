using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="F:UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize" />
    /// and its scaling properties.
    /// </summary>
    public sealed class ConstantPixelSize : CanvasScaler.Modifier
    {
        /// <summary>Backing store for the <see cref="ScaleFactor" /> property.</summary>
        public static readonly BindableProperty ScaleFactorProperty = BindableProperty.Create(
            "ScaleFactor",
            typeof(float),
            typeof(ConstantPixelSize),
            1f,
            BindingMode.OneWay,
            null,
            OnScaleFactorChanged);

        private static void OnScaleFactorChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ConstantPixelSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.scaleFactor = (float)state, component);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.scaleFactor" />.
        /// </summary>
        public float ScaleFactor
        {
            get
            {
                return (float)GetValue(ScaleFactorProperty);
            }

            set
            {
                SetValue(ScaleFactorProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
            Component.scaleFactor = ScaleFactor;
        }
    }
}
