using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.ContentSizeFitter" />.</summary>
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
    ///     <mu:ContentSizeFitter
    ///         VerticalFit="PreferredSize"
    ///         HorizontalFit="PreferredSize" />
    ///     <mu:LayoutElement
    ///         PreferredWidth="99"
    ///         PreferredHeight="50" />
    ///     <mu:Image />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class ContentSizeFitter : Object<UnityEngine.UI.ContentSizeFitter>
    {
        /// <summary>Backing store for the <see cref="HorizontalFit" /> property.</summary>
        public static readonly BindableProperty HorizontalFitProperty = CreateBindableComponentProperty<UnityEngine.UI.ContentSizeFitter.FitMode>(
            "HorizontalFit",
            typeof(ContentSizeFitter),
            (component, value) => component.horizontalFit = value,
            UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained);

        /// <summary>Backing store for the <see cref="VerticalFit" /> property.</summary>
        public static readonly BindableProperty VerticalFitProperty = CreateBindableComponentProperty<UnityEngine.UI.ContentSizeFitter.FitMode>(
            "VerticalFit",
            typeof(ContentSizeFitter),
            (component, value) => component.verticalFit = value,
            UnityEngine.UI.ContentSizeFitter.FitMode.Unconstrained);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ContentSizeFitter.horizontalFit" />.</summary>
        public UnityEngine.UI.ContentSizeFitter.FitMode HorizontalFit
        {
            get
            {
                return (UnityEngine.UI.ContentSizeFitter.FitMode)GetValue(HorizontalFitProperty);
            }

            set
            {
                SetValue(HorizontalFitProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ContentSizeFitter.verticalFit" />.</summary>
        public UnityEngine.UI.ContentSizeFitter.FitMode VerticalFit
        {
            get
            {
                return (UnityEngine.UI.ContentSizeFitter.FitMode)GetValue(VerticalFitProperty);
            }

            set
            {
                SetValue(VerticalFitProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.ContentSizeFitter>();
            Component.horizontalFit = HorizontalFit;
            Component.verticalFit = VerticalFit;
        }
    }
}
