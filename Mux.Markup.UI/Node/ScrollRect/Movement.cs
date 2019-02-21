using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents the unrestricted movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
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
    ///         Movement="{mu:Unrestricted}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Unrestricted : ScrollRect.Modifier
    {
        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.movementType = UnityEngine.UI.ScrollRect.MovementType.Unrestricted;
        }
    }

    /// <summary>
    /// A class that represents the elastic movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
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
    ///         Movement="{mu:Elastic}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Elasticity")]
    public class Elastic : ScrollRect.Modifier
    {
        /// <summary>Backing store for the <see cref="Elasticity" /> property.</summary>
        public static readonly BindableProperty ElasticityProperty = BindableProperty.Create(
            "Elasticity",
            typeof(float),
            typeof(Elastic),
            0.1f,
            BindingMode.OneWay,
            null,
            OnElasticityChanged);

        private static void OnElasticityChanged(BindableObject sender, object oldValue, object newValue)
        {
            var body = ((Elastic)sender).Body;

            if (body != null)
            {
                Forms.mainThread.Send(state => body.elasticity = (float)state, newValue);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ScrollRect.elasticity" />.
        /// </summary>
        /// <remarks>
        /// This is the content property; you do not have to specify the property name in XAML.
        /// </remarks>
        public float Elasticity
        {
            get
            {
                return (float)GetValue(ElasticityProperty);
            }

            set
            {
                SetValue(ElasticityProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.movementType = UnityEngine.UI.ScrollRect.MovementType.Elastic;
            Body.elasticity = Elasticity;
        }
    }

    /// <summary>
    /// A class that represents the clamped movement of <see cref="T:UnityEngine.UI.ScrollRect" />.
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
    ///         Movement="{mu:Clamped}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Clamped : ScrollRect.Modifier
    {
        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            Body.movementType = UnityEngine.UI.ScrollRect.MovementType.Clamped;
        }
    }
}
