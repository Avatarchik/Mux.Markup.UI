using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.ContentSizeFitter" />.</summary>
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
