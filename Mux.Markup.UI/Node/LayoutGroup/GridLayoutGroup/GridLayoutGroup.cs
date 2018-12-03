using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="LayoutGroup{T}" /> that represents <see cref="T:UnityEngine.UI.GridLayoutGroup" />.</summary>
    public class GridLayoutGroup : LayoutGroup<UnityEngine.UI.GridLayoutGroup>
    {
        /// <summary>Backing store for the <see cref="CellSize" /> property.</summary>
        public static readonly BindableProperty CellSizeProperty = CreateBindableComponentProperty<UnityEngine.Vector2>(
            "CellSize",
            typeof(GridLayoutGroup),
            (component, value) => component.cellSize = value,
            new UnityEngine.Vector2(100, 100));

        /// <summary>Backing store for the <see cref="Spacing" /> property.</summary>
        public static readonly BindableProperty SpacingProperty = CreateBindableComponentProperty<UnityEngine.Vector2>(
            "Spacing",
            typeof(GridLayoutGroup),
            (component, value) => component.spacing = value,
            UnityEngine.Vector2.zero);

        /// <summary>Backing store for the <see cref="StartCorner" /> property.</summary>
        public static readonly BindableProperty StartCornerProperty = CreateBindableComponentProperty<UnityEngine.UI.GridLayoutGroup.Corner>(
            "StartCorner",
            typeof(GridLayoutGroup),
            (component, value) => component.startCorner = value,
            UnityEngine.UI.GridLayoutGroup.Corner.UpperLeft);

        /// <summary>Backing store for the <see cref="StartAxis" /> property.</summary>
        public static readonly BindableProperty StartAxisProperty = CreateBindableComponentProperty<UnityEngine.UI.GridLayoutGroup.Axis>(
            "StartAxis",
            typeof(GridLayoutGroup),
            (component, value) => component.startAxis = value,
            UnityEngine.UI.GridLayoutGroup.Axis.Horizontal);

        /// <summary>Backing store for the <see cref="Constraint" /> property.</summary>
        public static readonly BindableProperty ConstraintProperty = CreateBindableModifierProperty(
            "Constraint",
            typeof(GridLayoutGroup),
            sender => new Flexible());

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.cellSize" />.</summary>
        /// <seealso cref="Vector2" />
        public UnityEngine.Vector2 CellSize
        {
            get
            {
                return (UnityEngine.Vector2)GetValue(CellSizeProperty);
            }

            set
            {
                SetValue(CellSizeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.spacing" />.</summary>
        /// <seealso cref="Vector2" />
        public UnityEngine.Vector2 Spacing
        {
            get
            {
                return (UnityEngine.Vector2)GetValue(SpacingProperty);
            }

            set
            {
                SetValue(SpacingProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.startCorner" />.</summary>
        public UnityEngine.UI.GridLayoutGroup.Corner StartCorner
        {
            get
            {
                return (UnityEngine.UI.GridLayoutGroup.Corner)GetValue(StartCornerProperty);
            }

            set
            {
                SetValue(StartCornerProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.startAxis" />.</summary>
        public UnityEngine.UI.GridLayoutGroup.Axis StartAxis
        {
            get
            {
                return (UnityEngine.UI.GridLayoutGroup.Axis)GetValue(StartAxisProperty);
            }

            set
            {
                SetValue(StartAxisProperty, value);
            }
        }

        /// <summary>A property that represents the constraint for <see cref="T:UnityEngine.UI.GridLayoutGroup" />.</summary>
        /// <seealso cref="Flexible" />
        /// <seealso cref="FixedColumnCount" />
        /// <seealso cref="FixedRowCount" />
        public Modifier Constraint
        {
            get
            {
                return (Modifier)GetValue(ConstraintProperty);
            }

            set
            {
                SetValue(ConstraintProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(Constraint, BindingContext);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            Component.cellSize = CellSize;
            Component.spacing = Spacing;
            Component.startCorner = StartCorner;
            Component.startAxis = StartAxis;
            Constraint.Component = Component;
        }

        internal override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            Constraint.DestroyMux();
        }
    }
}
