using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Component{T}" /> that represents <see cref="T:UnityEngine.UI.LayoutElement" />.</summary>
    public class LayoutElement : Component<UnityEngine.UI.LayoutElement>
    {
        /// <summary>Backing store for the <see cref="IgnoreLayout" /> property.</summary>
        public static readonly BindableProperty IgnoreLayoutProperty = CreateBindableBodyProperty<bool>(
            "IgnoreLayout",
            typeof(LayoutElement),
            (body, value) => body.ignoreLayout = value,
            false);

        /// <summary>Backing store for the <see cref="MinWidth" /> property.</summary>
        public static readonly BindableProperty MinWidthProperty = CreateBindableBodyProperty<float>(
            "MinWidth",
            typeof(LayoutElement),
            (body, value) => body.minWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="MinHeight" /> property.</summary>
        public static readonly BindableProperty MinHeightProperty = CreateBindableBodyProperty<float>(
            "MinHeight",
            typeof(LayoutElement),
            (body, value) => body.minHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="PreferredWidth" /> property.</summary>
        public static readonly BindableProperty PreferredWidthProperty = CreateBindableBodyProperty<float>(
            "PreferredWidth",
            typeof(LayoutElement),
            (body, value) => body.preferredWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="PreferredHeight" /> property.</summary>
        public static readonly BindableProperty PreferredHeightProperty = CreateBindableBodyProperty<float>(
            "PreferredHeight",
            typeof(LayoutElement),
            (body, value) => body.preferredHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="FlexibleWidth" /> property.</summary>
        public static readonly BindableProperty FlexibleWidthProperty = CreateBindableBodyProperty<float>(
            "FlexibleWidth",
            typeof(LayoutElement),
            (body, value) => body.flexibleWidth = value,
            -1f);

        /// <summary>Backing store for the <see cref="FlexibleHeight" /> property.</summary>
        public static readonly BindableProperty FlexibleHeightProperty = CreateBindableBodyProperty<float>(
            "FlexibleHeight",
            typeof(LayoutElement),
            (body, value) => body.flexibleHeight = value,
            -1f);

        /// <summary>Backing store for the <see cref="LayoutPriority" /> property.</summary>
        public static readonly BindableProperty LayoutPriorityProperty = CreateBindableBodyProperty<int>(
            "LayoutPriority",
            typeof(LayoutElement),
            (body, value) => body.layoutPriority = value,
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
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();

            Body.ignoreLayout = IgnoreLayout;
            Body.minWidth = MinWidth;
            Body.minHeight = MinHeight;
            Body.preferredWidth = PreferredWidth;
            Body.preferredHeight = PreferredHeight;
            Body.flexibleWidth = FlexibleWidth;
            Body.flexibleHeight = FlexibleHeight;
            Body.layoutPriority = LayoutPriority;
        }
    }
}
