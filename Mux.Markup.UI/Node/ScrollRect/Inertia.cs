using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the movement inertia of <see cref="T:UnityEngine.UI.ScrollRect" />.
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
    ///     <mu:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <m:RectTransform x:Name="viewport">
    ///         <mu:RectMask2D />
    ///         <m:RectTransform x:Name="content" X="{m:Sized SizeDelta=999}" Y="{m:Sized SizeDelta=999}">
    ///             <mue:UIMesh>
    ///                 <mue:UIMesh.Items>
    ///                     <mue:UIVertexTriangleStream>
    ///                         <mue:UIVertexTriangleStream.Verts>
    ///                             <m:UIVertex Color="{m:Color R=0, G=0, B=1}" Position="{m:Vector3 X=-0.5, Y=-0.5, Z=0}" />
    ///                             <m:UIVertex Color="{m:Color R=0, G=1, B=0}" Position="{m:Vector3 X=0, Y=0.5, Z=0}" />
    ///                             <m:UIVertex Color="{m:Color R=1, G=0, B=0}" Position="{m:Vector3 X=0.5, Y=-0.5, Z=0}" />
    ///                         </mue:UIVertexTriangleStream.Verts>
    ///                     </mue:UIVertexTriangleStream>
    ///                 </mue:UIMesh.Items>
    ///             </mue:UIMesh>
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    ///     <mu:ScrollRect
    ///         Viewport="{Binding Path=Body, Source={x:Reference Name=viewport}}"
    ///         Content="{Binding Path=Body, Source={x:Reference Name=content}}"
    ///         Inertia="{mu:Inertia}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Inertia : ScrollRect.Modifier
    {
        /// <summary>Backing store for the <see cref="DecelerationRate" /> property.</summary>
        public static readonly BindableProperty DecelerationRateProperty = BindableProperty.Create(
            "DecelerationRate",
            typeof(float),
            typeof(Inertia),
            0.135f,
            BindingMode.OneWay,
            null,
            OnDecelerationRateChanged);

        private static void OnDecelerationRateChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((Inertia)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.decelerationRate = (float)state, newValue);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.decelerationRate" />.</summary>
        /// <remarks>This is the content property; you do not have to specify the property name in XAML.</remarks>
        public float DecelerationRate
        {
            get
            {
                return (float)GetValue(DecelerationRateProperty);
            }

            set
            {
                SetValue(DecelerationRateProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.decelerationRate = DecelerationRate;
        }
    }
}
