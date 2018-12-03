using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.AspectRatioFitter" />.</summary>
    public class AspectRatioFitter : Object<UnityEngine.UI.AspectRatioFitter>
    {
        /// <summary>Backing store for the <see cref="AspectMode" /> property.</summary>
        public static readonly BindableProperty AspectModeProperty = CreateBindableComponentProperty<UnityEngine.UI.AspectRatioFitter.AspectMode>(
            "AspectMode",
            typeof(AspectRatioFitter),
            (component, value) => component.aspectMode = value,
            UnityEngine.UI.AspectRatioFitter.AspectMode.None);

        /// <summary>Backing store for the <see cref="AspectRatio" /> property.</summary>
        public static readonly BindableProperty AspectRatioProperty = CreateBindableComponentProperty<float>(
            "AspectRatio",
            typeof(AspectRatioFitter),
            (component, value) => component.aspectRatio = value,
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
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.AspectRatioFitter>();
            Component.aspectMode = AspectMode;
            Component.aspectRatio = AspectRatio;
        }
    }
}
