using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.GraphicRaycaster" />.</summary>
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
    ///     <mu:Text>
    ///         <mu:Text.Content>
    /// mu:GraphicRaycaster is required for the interactive components.
    /// Remove mu:GraphicRaycaster and try to click the toggle!
    ///         </mu:Text.Content>
    ///     </mu:Text>
    ///     <m:RectTransform>
    ///         <mu:Toggle />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class GraphicRaycaster : Object<UnityEngine.UI.GraphicRaycaster>
    {
        /// <summary>Backing store for the <see cref="IgnoreReversedGraphics" /> property.</summary>
        public static readonly BindableProperty IgnoreReversedGraphicsProperty = CreateBindableComponentProperty<bool>(
            "IgnoreReversedGraphics",
            typeof(GraphicRaycaster),
            (component, value) => component.ignoreReversedGraphics = value,
            true);

        /// <summary>Backing store for the <see cref="BlockingObjects" /> property.</summary>
        public static readonly BindableProperty BlockingObjectsProperty = CreateBindableComponentProperty<UnityEngine.UI.GraphicRaycaster.BlockingObjects>(
            "BlockingObjects",
            typeof(GraphicRaycaster),
            (component, value) => component.blockingObjects = value,
            UnityEngine.UI.GraphicRaycaster.BlockingObjects.None);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GraphicRaycaster.ignoreReversedGraphics" />.</summary>
        public bool IgnoreReversedGraphics
        {
            get
            {
                return (bool)GetValue(IgnoreReversedGraphicsProperty);
            }

            set
            {
                SetValue(IgnoreReversedGraphicsProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.GraphicRaycaster.blockingObjects" />.</summary>
        public UnityEngine.UI.GraphicRaycaster.BlockingObjects BlockingObjects
        {
            get
            {
                return (UnityEngine.UI.GraphicRaycaster.BlockingObjects)GetValue(BlockingObjectsProperty);
            }

            set
            {
                SetValue(BlockingObjectsProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.GraphicRaycaster>();
            Component.ignoreReversedGraphics = IgnoreReversedGraphics;
            Component.blockingObjects = BlockingObjects;
        }
    }
}
