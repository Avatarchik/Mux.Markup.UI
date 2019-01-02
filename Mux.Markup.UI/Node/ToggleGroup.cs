using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.ToggleGroup" />.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:ToggleGroup x:Name="group" />
    ///     <!--
    ///         You have to give property name "Path" to Binding and "Name" to x:Reference
    ///         only when you compile the interpreter with IL2CPP.
    ///         It is because ContentPropertyAttribute does not work with IL2CPP.
    ///     -->
    ///     <m:RectTransform X="{m:Stretch AnchorMax=0.5}">
    ///         <mu:Toggle Group="{Binding Path=Component, Source={x:Reference Name=group}}" />
    ///     </m:RectTransform>
    ///     <m:RectTransform X="{m:Stretch AnchorMin=0.5}">
    ///         <mu:Toggle Group="{Binding Path=Component, Source={x:Reference Name=group}}" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class ToggleGroup : Object<UnityEngine.UI.ToggleGroup>
    {
        /// <summary>Backing store for the <see cref="AllowSwitchOff" /> property.</summary>
        public static readonly BindableProperty AllowSwitchOffProperty = CreateBindableComponentProperty<bool>(
            "AllowSwitchOff",
            typeof(ToggleGroup),
            (component, value) => component.allowSwitchOff = value,
            false);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ToggleGroup.allowSwitchOff" />.
        /// </summary>
        public bool AllowSwitchOff
        {
            get
            {
                return (bool)GetValue(AllowSwitchOffProperty);
            }

            set
            {
                SetValue(AllowSwitchOffProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.ToggleGroup>();
            Component.allowSwitchOff = AllowSwitchOff;
        }
    }
}
