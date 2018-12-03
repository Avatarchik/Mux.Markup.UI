using System;
using Xamarin.Forms.Xaml;

namespace Mux.Markup
{
    /// <summary>
    /// A <xref href="Xamarin.Forms.Xaml.IMarkupExtension`1?text=markup extension" /> that represents
    /// <see cref="T:UnityEngine.UI.AnimationTriggers" />.
    /// </summary>
    [AcceptEmptyServiceProvider]
    public class AnimationTriggers : IMarkupExtension<UnityEngine.UI.AnimationTriggers>
    {
        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.AnimationTriggers.disabledTrigger" />.
        /// </summary>
        public string DisabledTrigger { get; set; } = "Disabled";

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.AnimationTriggers.highlightedTrigger" />.
        /// </summary>
        public string HighlightedTrigger { get; set; } = "Highlighted";

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.AnimationTriggers.normalTrigger" />.
        /// </summary>
        public string NormalTrigger { get; set; } = "Normal";

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.AnimationTriggers.pressedTrigger" />.
        /// </summary>
        public string PressedTrigger { get; set; } = "Pressed";

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public UnityEngine.UI.AnimationTriggers ProvideValue(IServiceProvider serviceProvider)
        {
            return new UnityEngine.UI.AnimationTriggers
            {
                disabledTrigger = DisabledTrigger,
                highlightedTrigger = HighlightedTrigger,
                normalTrigger = NormalTrigger,
                pressedTrigger = PressedTrigger
            };
        }
    }
}
