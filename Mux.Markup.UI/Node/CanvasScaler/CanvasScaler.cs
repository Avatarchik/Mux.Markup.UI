using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.CanvasScaler" />.
    /// </summary>
    public class CanvasScaler : Object<UnityEngine.UI.CanvasScaler>
    {
        /// <summary>Backing store for the <see cref="UiScale" /> property.</summary>
        public static readonly BindableProperty UiScaleProperty = CreateBindableModifierProperty(
            "UiScale",
            typeof(CanvasScaler),
            sender => new ConstantPixelSize());

        /// <summary>Backing store for the <see cref="ReferencePixelsPerUnit" /> property.</summary>
        public static readonly BindableProperty ReferencePixelsPerUnitProperty = CreateBindableComponentProperty<float>(
            "ReferencePixelsPerUnit",
            typeof(CanvasScaler),
            (component, value) => component.referencePixelsPerUnit = value,
            100f);

        /// <summary>A property that represents scaling mode and its properties.</summary>
        /// <remarks>
        /// Setting <see cref="Object{T:UnityEngine.UI.CanvasScaler}.Modifier" />
        /// to this property binds its lifetime to the lifetime of this object.
        /// </remarks>
        public Modifier UiScale
        {
            get
            {
                return (Modifier)GetValue(UiScaleProperty);
            }

            set
            {
                SetValue(UiScaleProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.referencePixelsPerUnit" />.
        /// </summary>
        public float ReferencePixelsPerUnit
        {
            get
            {
                return (float)GetValue(ReferencePixelsPerUnitProperty);
            }

            set
            {
                SetValue(ReferencePixelsPerUnitProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(UiScale, BindingContext);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.CanvasScaler>();
            Component.referencePixelsPerUnit = ReferencePixelsPerUnit;
            UiScale.Component = Component;
        }

        internal override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            UiScale?.DestroyMux();
        }
    }
}
