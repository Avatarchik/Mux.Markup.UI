using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the unrestricted movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
    /// </summary>
    public class Unrestricted : ScrollRect.Modifier
    {
        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.movementType = UnityEngine.UI.ScrollRect.MovementType.Unrestricted;
        }
    }

    /// <summary>
    /// A class that represents the elastic movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
    /// </summary>
    [ContentProperty("Elasticity")]
    public class Elastic : ScrollRect.Modifier
    {
        /// <summary>Backing store for the <see cref="Elasticity" /> property.</summary>
        public static readonly BindableProperty ElasticityProperty = BindableProperty.Create(
            "Elasticity",
            typeof(float),
            typeof(Elastic),
            0.1f,
            BindingMode.OneWay,
            null,
            OnElasticityChanged);

        private static void OnElasticityChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((Elastic)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.elasticity = (float)state, newValue);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ScrollRect.elasticity" />.
        /// </summary>
        /// <remarks>
        /// This is the content property; you do not have to specify the property name in XAML.
        /// </remarks>
        public float Elasticity
        {
            get
            {
                return (float)GetValue(ElasticityProperty);
            }

            set
            {
                SetValue(ElasticityProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.movementType = UnityEngine.UI.ScrollRect.MovementType.Elastic;
            Component.elasticity = Elasticity;
        }
    }

    /// <summary>
    /// A class that represents the clamped movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
    /// </summary>
    public class Clamped : ScrollRect.Modifier
    {
        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.movementType = UnityEngine.UI.ScrollRect.MovementType.Clamped;
        }
    }
}
