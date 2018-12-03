using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.LayoutGroup" />.</summary>
    public abstract class LayoutGroup<T> : Object<T> where T : UnityEngine.UI.LayoutGroup
    {
        /// <summary>Backing store for the <see cref="PaddingLeft" /> property.</summary>
        public static readonly BindableProperty PaddingLeftProperty = CreateBindableComponentProperty<int>(
            "PaddingLeft",
            typeof(LayoutGroup<T>),
            (component, value) => component.padding.left = value,
            0);

        /// <summary>Backing store for the <see cref="PaddingRight" /> property.</summary>
        public static readonly BindableProperty PaddingRightProperty = CreateBindableComponentProperty<int>(
            "PaddingRight",
            typeof(LayoutGroup<T>),
            (component, value) => component.padding.right = value,
            0);

        /// <summary>Backing store for the <see cref="PaddingTop" /> property.</summary>
        public static readonly BindableProperty PaddingTopProperty = CreateBindableComponentProperty<int>(
            "PaddingTop",
            typeof(LayoutGroup<T>),
            (component, value) => component.padding.right = value,
            0);

        /// <summary>Backing store for the <see cref="PaddingBottom" /> property.</summary>
        public static readonly BindableProperty PaddingBottomProperty = CreateBindableComponentProperty<int>(
            "PaddingBottom",
            typeof(LayoutGroup<T>),
            (component, value) => component.padding.bottom = value,
            0);

        /// <summary>Backing store for the <see cref="ChildAlignment" /> property.</summary>
        public static readonly BindableProperty ChildAlignmentProperty = CreateBindableComponentProperty<UnityEngine.TextAnchor>(
            "ChildAlignment",
            typeof(LayoutGroup<T>),
            (component, value) => component.childAlignment = value,
            UnityEngine.TextAnchor.UpperLeft);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.RectOffset.left" /> of
        /// <see cref="P:UnityEngine.UI.LayoutGroup.padding" />.
        /// </summary>
        public int PaddingLeft
        {
            get
            {
                return (int)GetValue(PaddingLeftProperty);
            }

            set
            {
                SetValue(PaddingLeftProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.RectOffset.right" /> of
        /// <see cref="P:UnityEngine.UI.LayoutGroup.padding" />.
        /// </summary>
        public int PaddingRight
        {
            get
            {
                return (int)GetValue(PaddingRightProperty);
            }

            set
            {
                SetValue(PaddingRightProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.RectOffset.top" /> of
        /// <see cref="P:UnityEngine.UI.LayoutGroup.padding" />.
        /// </summary>
        public int PaddingTop
        {
            get
            {
                return (int)GetValue(PaddingTopProperty);
            }

            set
            {
                SetValue(PaddingTopProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.RectOffset.bottom" /> of
        /// <see cref="P:UnityEngine.UI.LayoutGroup.padding" />.
        /// </summary>
        public int PaddingBottom
        {
            get
            {
                return (int)GetValue(PaddingBottomProperty);
            }

            set
            {
                SetValue(PaddingBottomProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.LayoutGroup.childAlignment" />.</summary>
        public UnityEngine.TextAnchor ChildAlignment
        {
            get
            {
                return (UnityEngine.TextAnchor)GetValue(ChildAlignmentProperty);
            }

            set
            {
                SetValue(ChildAlignmentProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<T>();
            Component.padding.left = PaddingLeft;
            Component.padding.right = PaddingRight;
            Component.padding.top = PaddingTop;
            Component.padding.bottom = PaddingBottom;
            Component.childAlignment = ChildAlignment;
        }
    }
}
