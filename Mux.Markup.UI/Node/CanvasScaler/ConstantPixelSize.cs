using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="F:UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize" />
    /// and its scaling properties.
    /// </summary>
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
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPixelSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:Text>
    ///         <mu:Text.Content>
    /// > Using the Constant Pixel Size mode, positions and sizes of UI elements are specified in pixels on the screen.
    /// Unity - Scripting API: UI.CanvasScaler.ScaleMode.ConstantPixelSize
    /// https://docs.unity3d.com/ScriptReference/UI.CanvasScaler.ScaleMode.ConstantPixelSize.html
    ///         </mu:Text.Content>
    ///     </mu:Text>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
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
            var body = ((ConstantPixelSize)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.scaleFactor = (float)state, body);
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
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
            Body.scaleFactor = ScaleFactor;
        }
    }
}
