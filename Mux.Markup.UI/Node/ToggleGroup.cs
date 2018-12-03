using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.ToggleGroup" />.
    /// </summary>
    public class ToggleGroup : Object<UnityEngine.UI.ToggleGroup>
    {
        /// <summary>Backing store for the <see cref="AllowSwitchOff" /> property.</summary>
        public static readonly BindableProperty AllowSwitchOffProperty = CreateBindableComponentProperty<bool>(
            "AllowSwitchOff",
            typeof(ToggleGroup),
            (component, value) => component.allowSwitchOff = value,
            false);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ToggleGroup.allowSwitchOff" />.
        /// </summary>
        public bool AllowSwitchOff
        {
            get
            {
                return (bool)GetValue(AllowSwitchOffProperty);
            }

            set
            {
                SetValue(AllowSwitchOffProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.ToggleGroup>();
            Component.allowSwitchOff = AllowSwitchOff;
        }
    }
}
