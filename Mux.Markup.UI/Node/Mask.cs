using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.Mask" />.</summary>
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
    ///     <mu:Mask />
    ///     <mu:Text Content="Masking mu:Image with mu:Text" />
    ///     <m:RectTransform X="{m:Stretch}" Y="{m:Stretch}">
    ///         <mu:Image Color="{m:Color R=0, G=0, B=1}" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Mask : Object<UnityEngine.UI.Mask>
    {
        /// <summary>Backing store for the <see cref="ShowMaskGraphic" /> property.</summary>
        public static readonly BindableProperty ShowMaskGraphicProperty = CreateBindableComponentProperty<bool>(
            "ShowMaskGraphic",
            typeof(Mask),
            (component, value) => component.showMaskGraphic = value,
            true);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Mask.showMaskGraphic" />.</summary>
        public bool ShowMaskGraphic
        {
            get
            {
                return (bool)GetValue(ShowMaskGraphicProperty);
            }

            set
            {
                SetValue(ShowMaskGraphicProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.Mask>();
            Component.showMaskGraphic = ShowMaskGraphic;
        }
    }
}
