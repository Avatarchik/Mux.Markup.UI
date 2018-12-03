using System;
using Xamarin.Forms.Xaml;

namespace Mux.Markup
{
    /// <summary>
    /// A <xref href="Xamarin.Forms.Xaml.IMarkupExtension`1?text=markup extension" />
    /// that represents <see cref="T:UnityEngine.UI.ColorBlock" />.
    /// </summary>
    [AcceptEmptyServiceProvider]
    public class ColorBlock : IMarkupExtension<UnityEngine.UI.ColorBlock>
    {
        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.colorMultiplier" />.
        /// </summary>
        public float ColorMultiplier { get; set; } = 1;

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.disabledColor" />.
        /// </summary>
        public UnityEngine.Color DisabledColor { get; set; } = new UnityEngine.Color32(200, 200, 200, 128);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.fadeDuration" />.
        /// </summary>
        public float FadeDuration { get; set; } = 0.1f;

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.highlightedColor" />.
        /// </summary>
        public UnityEngine.Color HighlightedColor { get; set; } = new UnityEngine.Color32(255, 255, 255, 255);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.normalColor" />.
        /// </summary>
        public UnityEngine.Color NormalColor { get; set; } = new UnityEngine.Color32(255, 255, 255, 255);

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.ColorBlock.pressedColor" />.
        /// </summary>
        public UnityEngine.Color PressedColor { get; set; } = new UnityEngine.Color32(200, 200, 200, 255);

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public UnityEngine.UI.ColorBlock ProvideValue(IServiceProvider serviceProvider)
        {
            return new UnityEngine.UI.ColorBlock
            {
                colorMultiplier = ColorMultiplier,
                disabledColor = DisabledColor,
                fadeDuration = FadeDuration,
                highlightedColor = HighlightedColor,
                normalColor = NormalColor,
                pressedColor = PressedColor
            };
        }
    }
}
