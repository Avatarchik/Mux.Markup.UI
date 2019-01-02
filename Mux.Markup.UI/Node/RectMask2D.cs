using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.RectMask2D" />.
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
    ///     <m:RectTransform>
    ///         <mu:RectMask2D />
    ///         <m:RectTransform X="{m:Sized Anchor=0, Pivot=0, SizeDelta=999}" Y="{m:Sized Anchor=1, Pivot=1, SizeDelta=999}">
    ///             <mu:Text>
    ///                 <mu:Text.Content>
    /// This text is masked by mu:RectMask2D.
    ///
    /// Comparison between mu:RectMask2D and mu:Mask:
    /// - mu:RectMask2D performs better than mu:Mask.
    ///   Concretely, mu:RectMask2D does not require the stencil buffer while mu:Mask does.
    /// - mu:Mask can mask with a complex graphic while mu:RectMask2D always masks with a rectangle.
    ///                 </mu:Text.Content>
    ///             </mu:Text>
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class RectMask2D : Object<UnityEngine.UI.RectMask2D>
    {
        /// <inheritdoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.RectMask2D>();
        }
    }
}
