using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.Mask" />.</summary>
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
