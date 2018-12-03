using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Graphic{T}" /> that represents <see cref="T:UnityEngine.UI.RawImage" />.</summary>
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
