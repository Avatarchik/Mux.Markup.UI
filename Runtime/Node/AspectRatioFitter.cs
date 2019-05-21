using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Behaviour{T}" /> that represents <see cref="T:UnityEngine.UI.AspectRatioFitter" />.</summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <mu:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:ContentSizeFitter HorizontalFit="PreferredSize" VerticalFit="PreferredSize" />
    ///     <mu:LayoutElement PreferredWidth="99" PreferredHeight="50" />
    ///     <mu:AspectRatioFitter AspectMode="WidthControlsHeight" AspectRatio="1" />
    ///     <mu:Image />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class AspectRatioFitter : Behaviour<UnityEngine.UI.AspectRatioFitter>
    {
        /// <summary>Backing store for the <see cref="AspectMode" /> property.</summary>
        public static readonly BindableProperty AspectModeProperty = CreateBindableBodyProperty<UnityEngine.UI.AspectRatioFitter.AspectMode>(
            "AspectMode",
            typeof(AspectRatioFitter),
            (body, value) => body.aspectMode = value,
            UnityEngine.UI.AspectRatioFitter.AspectMode.None);

        /// <summary>Backing store for the <see cref="AspectRatio" /> property.</summary>
        public static readonly BindableProperty AspectRatioProperty = CreateBindableBodyProperty<float>(
            "AspectRatio",
            typeof(AspectRatioFitter),
            (body, value) => body.aspectRatio = value,
            1f);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.AspectRatioFitter.aspectMode" />.</summary>
        public UnityEngine.UI.AspectRatioFitter.AspectMode AspectMode
        {
            get
            {
                return (UnityEngine.UI.AspectRatioFitter.AspectMode)GetValue(AspectModeProperty);
            }

            set
            {
                SetValue(AspectModeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.AspectRatioFitter.aspectRatio" />.</summary>
        public float AspectRatio
        {
            get
            {
                return (float)GetValue(AspectRatioProperty);
            }

            set
            {
                SetValue(AspectRatioProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            Body.aspectMode = AspectMode;
            Body.aspectRatio = AspectRatio;

            base.AwakeInMainThread();
        }
    }
}
