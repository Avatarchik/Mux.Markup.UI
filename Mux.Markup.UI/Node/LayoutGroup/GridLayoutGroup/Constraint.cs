using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the flexible constraint of <see cref="T:UnityEngine.UI.GridLayoutGroup" />.
    /// </summary>
    public class Flexible : GridLayoutGroup.Modifier
    {
        /// <inheritdoc />
        protected override void InitializeComponentInMainThread()
        {
            Component.constraint = UnityEngine.UI.GridLayoutGroup.Constraint.Flexible;
        }
    }
    public abstract class FixedCount : GridLayoutGroup.Modifier
    {
        /// <summary>Backing store for the <see cref="Count" /> property.</summary>
        public static readonly BindableProperty CountProperty = BindableProperty.Create(
            "Count",
            typeof(int),
            typeof(FixedCount),
            null,
            BindingMode.OneWay,
            null,
            OnCountChanged);

        private static void OnCountChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((FixedCount)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.constraintCount = (int)state, newValue);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.constraint" />.
        /// </summary>
        protected abstract UnityEngine.UI.GridLayoutGroup.Constraint Constraint { get; }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.GridLayoutGroup.constraintCount" />.
        /// </summary>
        /// <remarks>
        /// This is the content property; you do not have to specify the property name in XAML.
        /// </remarks>
        public int Count
        {
            get
            {
                return (int)GetValue(CountProperty);
            }

            set
            {
                SetValue(CountProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.constraint = Constraint;
            Component.constraintCount = Count;
        }
    }

    /// <summary>
    /// A class that represents the fixed column count constraint of <see cref="T:UnityEngine.UI.GridLayoutGroup" />.
    /// </summary>
    [ContentProperty("Count")]
    public class FixedColumnCount : FixedCount
    {
        /// <inheritdoc />
        protected sealed override UnityEngine.UI.GridLayoutGroup.Constraint Constraint =>
            UnityEngine.UI.GridLayoutGroup.Constraint.FixedColumnCount;
    }

    /// <summary>
    /// A class that represents the fixed row count constraint of <see cref="T:UnityEngine.UI.GridLayoutGroup" />.
    /// </summary>
    [ContentProperty("Count")]
    public class FixedRowCount : FixedCount
    {
        /// <inheritdoc />
        protected sealed override UnityEngine.UI.GridLayoutGroup.Constraint Constraint =>
            UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount;
    }
}
