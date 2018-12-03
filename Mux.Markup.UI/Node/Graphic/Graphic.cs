using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An abstract class that represents <see cref="T:UnityEngine.UI.Graphic" />.</summary>
    public abstract class Graphic<T> : Object<T> where T : UnityEngine.UI.Graphic
    {
        /// <summary>Backing store for the <see cref="Color" /> property.</summary>
        public static readonly BindableProperty ColorProperty = CreateBindableComponentProperty<UnityEngine.Color>(
            "Color",
            typeof(Graphic<T>),
            (component, value) => component.color = value,
            UnityEngine.Color.white);

        /// <summary>Backing store for the <see cref="Material" /> property.</summary>
        public static readonly BindableProperty MaterialProperty = CreateBindableComponentProperty<UnityEngine.Material>(
            "Material",
            typeof(Graphic<T>),
            (component, value) => component.material = value);

        /// <summary>Backing store for the <see cref="RaycastTarget" /> property.</summary>
        public static readonly BindableProperty RaycastTargetProperty = CreateBindableComponentProperty<bool>(
            "RaycastTarget",
            typeof(Graphic<T>),
            (component, value) => component.raycastTarget = value,
            true);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Graphic.color" />.</summary>
        /// <seealso cref="Color" />
        /// <seealso cref="Color32" />
        public UnityEngine.Color Color
        {
            get
            {
                return (UnityEngine.Color)GetValue(ColorProperty);
            }

            set
            {
                SetValue(ColorProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Graphic.material" />.</summary>
        public UnityEngine.Material Material
        {
            get
            {
                return (UnityEngine.Material)GetValue(MaterialProperty);
            }

            set
            {
                SetValue(MaterialProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Graphic.raycastTarget" />.</summary>
        public bool RaycastTarget
        {
            get
            {
                return (bool)GetValue(RaycastTargetProperty);
            }

            set
            {
                SetValue(RaycastTargetProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<T>();
            Component.color = Color;
            Component.material = Material;
            Component.raycastTarget = RaycastTarget;
        }
    }
}
