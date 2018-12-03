using System;
using Xamarin.Forms.Xaml;

namespace Mux.Markup
{
    /// <summary>
    /// A <xref href="Xamarin.Forms.Xaml.IMarkupExtension`1?text=markup extension" />
    /// that represents <see cref="T:UnityEngine.UI.SpriteState" />.
    /// </summary>
    [AcceptEmptyServiceProvider]
    public class SpriteState : IMarkupExtension<UnityEngine.UI.SpriteState>
    {
        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.SpriteState.highlightedSprite" />.
        /// </summary>
        public UnityEngine.Sprite HighlightedSprite { get; set; }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.SpriteState.pressedSprite" />.
        /// </summary>
        public UnityEngine.Sprite PressedSprite { get; set; }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.SpriteState.disabledSprite" />.
        /// </summary>
        public UnityEngine.Sprite DisabledSprite { get; set; }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public UnityEngine.UI.SpriteState ProvideValue(IServiceProvider serviceProvider)
        {
            return new UnityEngine.UI.SpriteState
            {
                highlightedSprite = HighlightedSprite,
                pressedSprite = PressedSprite,
                disabledSprite = DisabledSprite
            };
        }
    }
}
