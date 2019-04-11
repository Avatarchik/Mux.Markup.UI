using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An abstract class that represents <see cref="T:UnityEngine.UI.Shadow" /> or its subclass.</summary>
    public class Shadow<T> : Behaviour<T> where T : UnityEngine.UI.Shadow
    {
        /// <summary>Backing store for the <see cref="EffectColor" /> property.</summary>
        public static readonly BindableProperty EffectColorProperty = CreateBindableBodyProperty<UnityEngine.Color>(
            "EffectColor",
            typeof(Shadow<T>),
            (body, value) => body.effectColor = value,
            new UnityEngine.Color(0f, 0f, 0f, 0.5f));

        /// <summary>Backing store for the <see cref="EffectDistance" /> property.</summary>
        public static readonly BindableProperty EffectDistanceProperty = CreateBindableBodyProperty<UnityEngine.Vector2>(
            "EffectDistance",
            typeof(Shadow<T>),
            (body, value) => body.effectDistance = value,
            new UnityEngine.Vector2(1, -1));

        /// <summary>Backing store for the <see cref="UseGraphicAlpha" /> property.</summary>
        public static readonly BindableProperty UseGraphicAlphaProperty = CreateBindableBodyProperty<bool>(
            "UseGraphicAlpha",
            typeof(Shadow<T>),
            (body, value) => body.useGraphicAlpha = value,
            true);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Shadow.effectColor" />.</summary>
        /// <seealso cref="Color" />
        /// <seealso cref="Color32" />
        public UnityEngine.Color EffectColor
        {
            get
            {
                return (UnityEngine.Color)GetValue(EffectColorProperty);
            }

            set
            {
                SetValue(EffectColorProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Shadow.effectDistance" />.</summary>
        /// <seealso cref="Vector2" />
        public UnityEngine.Vector2 EffectDistance
        {
            get
            {
                return (UnityEngine.Vector2)GetValue(EffectDistanceProperty);
            }

            set
            {
                SetValue(EffectDistanceProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Shadow.useGraphicAlpha" />.</summary>
        public bool UseGraphicAlpha
        {
            get
            {
                return (bool)GetValue(UseGraphicAlphaProperty);
            }

            set
            {
                SetValue(UseGraphicAlphaProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            Body.effectColor = EffectColor;
            Body.effectDistance = EffectDistance;
            Body.useGraphicAlpha = UseGraphicAlpha;

            base.AwakeInMainThread();
        }
    }

    /// <summary>A class that represents <see cref="T:UnityEngine.UI.Shadow" />.</summary>
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
    ///     <mu:Shadow />
    ///     <mu:Text Content="This text is shadowed." />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class Shadow : Shadow<UnityEngine.UI.Shadow>
    {
    }

    /// <summary>A class that represents <see cref="T:UnityEngine.UI.Outline" />.</summary>
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
    ///     <mu:Outline />
    ///     <mu:Text Content="This text is outlined." />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public sealed class Outline : Shadow<UnityEngine.UI.Outline>
    {
    }
}
