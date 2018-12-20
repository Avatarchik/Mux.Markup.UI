using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Graphic{T}" /> that represents <see cref="T:UnityEngine.UI.RawImage" />.</summary>
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
    ///     <mu:RawImage />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class RawImage : Graphic<UnityEngine.UI.RawImage>
    {
        /// <summary>Backing store for the <see cref="Texture" /> property.</summary>
        public static readonly BindableProperty TextureProperty = CreateBindableComponentProperty<UnityEngine.Texture>(
            "Texture",
            typeof(RawImage),
            (component, value) => component.texture = value);

        /// <summary>Backing store for the <see cref="UvRect" /> property.</summary>
        public static readonly BindableProperty UvRectProperty = CreateBindableComponentProperty<UnityEngine.Rect>(
            "UvRect",
            typeof(RawImage),
            (component, value) => component.uvRect = value,
            new UnityEngine.Rect(0, 0, 1, 1));

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.RawImage.texture" />.</summary>
        public UnityEngine.Texture Texture
        {
            get
            {
                return (UnityEngine.Texture)GetValue(TextureProperty);
            }

            set
            {
                SetValue(TextureProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.RawImage.uvRect" />.</summary>
        public UnityEngine.Rect UvRect
        {
            get
            {
                return (UnityEngine.Rect)GetValue(UvRectProperty);
            }

            set
            {
                SetValue(UvRectProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            Component.texture = Texture;
            Component.uvRect = UvRect;
        }
    }
}
