using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the movement inertia of <see cref="T:UnityEngine.UI.ScrollRect" />.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    ///     xmlns:playgroundMarkup="clr-namespace:Mux.Playground.Markup;assembly=Assembly-CSharp">
    ///     <!--
    ///       Note that you can use "using" scheme instead of "clr-namespace" to omit assembly
    ///       specification if:
    ///       - the referenced type is in an assembly already loaded. (interpreter)
    ///       - the referenced type is in the assembly containing the compiled XAML. (compiler)
    ///     -->
    ///     <mu:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <playgroundMarkup:ScrollViewTransform Inertia="{mu:Inertia}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Inertia : ScrollRect.Modifier
    {
        /// <summary>Backing store for the <see cref="DecelerationRate" /> property.</summary>
        public static readonly BindableProperty DecelerationRateProperty = BindableProperty.Create(
            "DecelerationRate",
            typeof(float),
            typeof(Inertia),
            0.135f,
            BindingMode.OneWay,
            null,
            OnDecelerationRateChanged);

        private static void OnDecelerationRateChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((Inertia)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.decelerationRate = (float)state, newValue);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.decelerationRate" />.</summary>
        /// <remarks>This is the content property; you do not have to specify the property name in XAML.</remarks>
        public float DecelerationRate
        {
            get
            {
                return (float)GetValue(DecelerationRateProperty);
            }

            set
            {
                SetValue(DecelerationRateProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.decelerationRate = DecelerationRate;
        }
    }
}
