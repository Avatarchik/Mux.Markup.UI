﻿using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An abstract class that represents <see cref="T:UnityEngine.UI.Shadow" /> or its subclass.</summary>
    public class Shadow<T> : Object<T> where T : UnityEngine.UI.Shadow
    {
        /// <summary>Backing store for the <see cref="EffectColor" /> property.</summary>
        public static readonly BindableProperty EffectColorProperty = CreateBindableComponentProperty<UnityEngine.Color>(
            "EffectColor",
            typeof(Shadow<T>),
            (component, value) => component.effectColor = value,
            new UnityEngine.Color(0f, 0f, 0f, 0.5f));

        /// <summary>Backing store for the <see cref="EffectDistance" /> property.</summary>
        public static readonly BindableProperty EffectDistanceProperty = CreateBindableComponentProperty<UnityEngine.Vector2>(
            "EffectDistance",
            typeof(Shadow<T>),
            (component, value) => component.effectDistance = value,
            new UnityEngine.Vector2(1, -1));

        /// <summary>Backing store for the <see cref="UseGraphicAlpha" /> property.</summary>
        public static readonly BindableProperty UseGraphicAlphaProperty = CreateBindableComponentProperty<bool>(
            "UseGraphicAlpha",
            typeof(Shadow<T>),
            (component, value) => component.useGraphicAlpha = value,
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
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<T>();
            Component.effectColor = EffectColor;
            Component.effectDistance = EffectDistance;
            Component.useGraphicAlpha = UseGraphicAlpha;
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
    ///     <m:StandaloneInputModule />
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
    ///     <m:StandaloneInputModule />
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
