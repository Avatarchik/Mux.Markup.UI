using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the movement inertia of <see cref="T:UnityEngine.UI.ScrollRect" />.
    /// </summary>
    public class Inertia : ScrollRect.Modifier
    {
        /// <summary>Backing store for the <see cref="DecelerationRate" /> property.</summary>
        public static readonly BindableProperty DecelerationRateProperty = BindableProperty.Create(
            "DecelerationRate",
            typeof(float),
            typeof(Inertia),
            0.135f,
            BindingMode.OneWay,
            null,
            OnDecelerationRateChanged);

        private static void OnDecelerationRateChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Inertia)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.decelerationRate = (float)state, newValue);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.decelerationRate" />.</summary>
        /// <remarks>This is the content property; you do not have to specify the property name in XAML.</remarks>
        public float DecelerationRate
        {
            get
            {
                return (float)GetValue(DecelerationRateProperty);
            }

            set
            {
                SetValue(DecelerationRateProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.decelerationRate = DecelerationRate;
        }
    }
}
