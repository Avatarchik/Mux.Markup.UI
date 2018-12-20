using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the flexible constraint of <see cref="T:UnityEngine.UI.GridLayoutGroup" />.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:GridLayoutGroup Constraint="{mu:Flexible}" />
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=0, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=0, B=1}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=1, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=1, B=1}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=0, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=0, B=1}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=1, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=1, B=1}" /></m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
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
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:GridLayoutGroup Constraint="{mu:FixedColumnCount Count=2}" />
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=0, B=1}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=1, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=0, B=0}" /></m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
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
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:GridLayoutGroup Constraint="{mu:FixedRowCount Count=2}" StartAxis="Vertical" />
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=0, B=1}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=0, G=1, B=0}" /></m:RectTransform>
    ///     <m:RectTransform><mu:Image Color="{m:Color R=1, G=0, B=0}" /></m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Count")]
    public class FixedRowCount : FixedCount
    {
        /// <inheritdoc />
        protected sealed override UnityEngine.UI.GridLayoutGroup.Constraint Constraint =>
            UnityEngine.UI.GridLayoutGroup.Constraint.FixedRowCount;
    }
}
