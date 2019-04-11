using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A <see cref="Behaviour{T}" /> that represents <see cref="T:UnityEngine.UI.CanvasScaler" />.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    ///     xmlns:playground="clr-namespace:Mux.Playground;assembly=Assembly-CSharp"
    //      x:DataType="playground:PlaygroundViewModel">
    ///     <!--
    ///       Note that you can use "using" scheme instead of "clr-namespace" to omit assembly
    ///       specification if:
    ///       - the referenced type is in an assembly already loaded. (interpreter)
    ///       - the referenced type is in the assembly containing the compiled XAML. (compiler)
    ///     -->
    ///     <mu:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:Text Font="{Binding Path=Resources.Font}">
    ///         <mu:Text.Content>
    /// You have to give property name "Path" to Binding only when you compile
    /// the interpreter with IL2CPP. It is because ContentPropertyAttribute does
    /// not work with IL2CPP.
    ///
    /// mu:CanvasScaler determines the scale of uGUI components.
    /// See what happens to this text if you change UiScale property of mu:CanvasScaler!
    ///         </mu:Text.Content>
    ///     </mu:Text>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class CanvasScaler : Behaviour<UnityEngine.UI.CanvasScaler>
    {
        /// <summary>Backing store for the <see cref="UiScale" /> property.</summary>
        public static readonly BindableProperty UiScaleProperty = CreateBindableModifierProperty(
            "UiScale",
            typeof(CanvasScaler),
            sender => new ConstantPixelSize());

        /// <summary>Backing store for the <see cref="ReferencePixelsPerUnit" /> property.</summary>
        public static readonly BindableProperty ReferencePixelsPerUnitProperty = CreateBindableBodyProperty<float>(
            "ReferencePixelsPerUnit",
            typeof(CanvasScaler),
            (body, value) => body.referencePixelsPerUnit = value,
            100f);

        /// <summary>A property that represents scaling mode and its properties.</summary>
        /// <remarks>
        /// Setting <see cref="Component{T:UnityEngine.UI.CanvasScaler}.Modifier" />
        /// to this property binds its lifetime to the lifetime of this object.
        /// </remarks>
        public Modifier UiScale
        {
            get
            {
                return (Modifier)GetValue(UiScaleProperty);
            }

            set
            {
                SetValue(UiScaleProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.referencePixelsPerUnit" />.
        /// </summary>
        public float ReferencePixelsPerUnit
        {
            get
            {
                return (float)GetValue(ReferencePixelsPerUnitProperty);
            }

            set
            {
                SetValue(ReferencePixelsPerUnitProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(UiScale, BindingContext);
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            Body.referencePixelsPerUnit = ReferencePixelsPerUnit;
            UiScale.Body = Body;

            base.AwakeInMainThread();
        }

        /// <inheritdoc />
        protected override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            UiScale?.DestroyMux();
        }
    }
}
