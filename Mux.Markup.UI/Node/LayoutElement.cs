using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.LayoutElement" />.</summary>
    public class LayoutElement : Object<UnityEngine.UI.LayoutElement>
    {
        /// <summary>Backing store for the <see cref="IgnoreLayout" /> property.</summary>
        public static readonly BindableProperty IgnoreLayoutProperty = CreateBindableComponentProperty<bool>(
            "IgnoreLayout",
            typeof(LayoutElement),
            (component, value) => component.ignoreLayout = value,
            false);

        /// <summary>Backing store for the <see cref="MinWidth" /> property.</summary>
        public static readonly BindableProperty MinWidthProperty = CreateBindableComponentProperty<float>(
            "MinWidth",
            typeof(LayoutElement),
            (component, value) => component.minWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="MinHeight" /> property.</summary>
        public static readonly BindableProperty MinHeightProperty = CreateBindableComponentProperty<float>(
            "MinHeight",
            typeof(LayoutElement),
            (component, value) => component.minHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="PreferredWidth" /> property.</summary>
        public static readonly BindableProperty PreferredWidthProperty = CreateBindableComponentProperty<float>(
            "PreferredWidth",
            typeof(LayoutElement),
            (component, value) => component.preferredWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="PreferredHeight" /> property.</summary>
        public static readonly BindableProperty PreferredHeightProperty = CreateBindableComponentProperty<float>(
            "PreferredHeight",
            typeof(LayoutElement),
            (component, value) => component.preferredHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="FlexibleWidth" /> property.</summary>
        public static readonly BindableProperty FlexibleWidthProperty = CreateBindableComponentProperty<float>(
            "FlexibleWidth",
            typeof(LayoutElement),
            (component, value) => component.flexibleWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="FlexibleHeight" /> property.</summary>
        public static readonly BindableProperty FlexibleHeightProperty = CreateBindableComponentProperty<float>(
            "FlexibleHeight",
            typeof(LayoutElement),
            (component, value) => component.flexibleHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="LayoutPriority" /> property.</summary>
        public static readonly BindableProperty LayoutPriorityProperty = CreateBindableComponentProperty<int>(
            "LayoutPriority",
            typeof(LayoutElement),
            (component, value) => component.layoutPriority = value,
            1);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.ignoreLayout" />.
        /// </summary>
        public bool IgnoreLayout
        {
            get
            {
                return (bool)GetValue(IgnoreLayoutProperty);
            }

            set
            {
                SetValue(IgnoreLayoutProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.minWidth" />.
        /// </summary>
        public float MinWidth
        {
            get
            {
                return (float)GetValue(MinWidthProperty);
            }

            set
            {
                SetValue(MinWidthProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.minHeight" />.
        /// </summary>
        public float MinHeight
        {
            get
            {
                return (float)GetValue(MinHeightProperty);
            }

            set
            {
                SetValue(MinHeightProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.preferredWidth" />.
        /// </summary>
        public float PreferredWidth
        {
            get
            {
                return (float)GetValue(PreferredWidthProperty);
            }

            set
            {
                SetValue(PreferredWidthProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.preferredHeight" />.
        /// </summary>
        public float PreferredHeight
        {
            get
            {
                return (float)GetValue(PreferredHeightProperty);
            }

            set
            {
                SetValue(PreferredHeightProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.flexibleWidth" />.
        /// </summary>
        public float FlexibleWidth
        {
            get
            {
                return (float)GetValue(FlexibleWidthProperty);
            }

            set
            {
                SetValue(FlexibleWidthProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.flexibleHeight" />.
        /// </summary>
        public float FlexibleHeight
        {
            get
            {
                return (float)GetValue(FlexibleHeightProperty);
            }

            set
            {
                SetValue(FlexibleHeightProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.LayoutElement.layoutPriority" />.
        /// </summary>
        public int LayoutPriority
        {
            get
            {
                return (int)GetValue(LayoutPriorityProperty);
            }

            set
            {
                SetValue(LayoutPriorityProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.LayoutElement>();
            Component.ignoreLayout = IgnoreLayout;
            Component.minWidth = MinWidth;
            Component.minHeight = MinHeight;
            Component.preferredWidth = PreferredWidth;
            Component.preferredHeight = PreferredHeight;
            Component.flexibleWidth = FlexibleWidth;
            Component.flexibleHeight = FlexibleHeight;
            Component.layoutPriority = LayoutPriority;
        }
    }
}
