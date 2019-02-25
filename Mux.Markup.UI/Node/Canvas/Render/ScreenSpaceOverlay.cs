using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="T:UnityEngine.RenderMode.ScreenSpaceOverlay" />
    /// and its rendering properties.
    /// </summary>
    public class ScreenSpaceOverlay : Render
    {
        /// <summary>Backing store for the <see cref="PixelPerfect" /> property.</summary>
        public static readonly BindableProperty PixelPerfectProperty =
            CreateBindablePixelPerfectProperty(typeof(ScreenSpaceOverlay));

        /// <summary>Backing store for the <see cref="OverridePixelPerfect" /> property.</summary>
        public static readonly BindableProperty OverridePixelPerfectProperty =
            CreateBindableOverridePixelPerfectProperty(typeof(ScreenSpaceOverlay));

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.pixelPerfect" />.
        /// </summary>
        public bool PixelPerfect
        {
            get
            {
                return (bool)GetValue(PixelPerfectProperty);
            }

            set
            {
                SetValue(PixelPerfectProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.Canvas.overridePixelPerfect" />.
        /// </summary>
        public bool OverridePixelPerfect
        {
            get
            {
                return (bool)GetValue(OverridePixelPerfectProperty);
            }

            set
            {
                SetValue(OverridePixelPerfectProperty, value);
            }
        }

        internal sealed override UnityEngine.RenderMode Mode => UnityEngine.RenderMode.ScreenSpaceOverlay;

        /// <inheritdoc />
        protected sealed override void InitializeBodyInMainThread()
        {
            base.InitializeBodyInMainThread();
            Body.pixelPerfect = PixelPerfect;
            Body.overridePixelPerfect = OverridePixelPerfect;
        }
    }
}
