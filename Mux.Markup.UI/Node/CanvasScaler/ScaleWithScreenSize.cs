using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="F:UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize" />
    /// and its scaling properties.
    /// </summary>
    public sealed class ScaleWithScreenSize : CanvasScaler.Modifier
    {
        /// <summary>Backing store for the <see cref="ReferenceResolution" /> property.</summary>
        public static readonly BindableProperty ReferenceResolutionProperty = BindableProperty.Create(
            "ReferenceResolution",
            typeof(UnityEngine.Vector2),
            typeof(ScaleWithScreenSize),
            new UnityEngine.Vector2(800, 600),
            BindingMode.OneWay,
            null,
            OnReferenceResolutionChanged);

        /// <summary>Backing store for the <see cref="ScreenMatchMode" /> property.</summary>
        public static readonly BindableProperty ScreenMatchModeProperty = BindableProperty.Create(
            "ScreenMatchMode",
            typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode),
            typeof(ScaleWithScreenSize),
            UnityEngine.UI.CanvasScaler.ScreenMatchMode.MatchWidthOrHeight,
            BindingMode.OneWay,
            null,
            OnScreenMatchModeChanged);

        /// <summary>Backing store for the <see cref="MatchWidthOrHeight" /> property.</summary>
        public static readonly BindableProperty MatchWidthOrHeightProperty = BindableProperty.Create(
            "MatchWidthOrHeight",
            typeof(float),
            typeof(ScaleWithScreenSize),
            0f,
            BindingMode.OneWay,
            null,
            OnMatchWidthOrHeightChanged);

        private static void OnReferenceResolutionChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScaleWithScreenSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(
                    state => component.referenceResolution = (UnityEngine.Vector2)state,
                    newValue);
            }
        }

        private static void OnScreenMatchModeChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScaleWithScreenSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(
                    state => component.screenMatchMode = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)state,
                    newValue);
            }
        }

        private static void OnMatchWidthOrHeightChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScaleWithScreenSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Post(state => component.matchWidthOrHeight = (float)state, newValue);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.referenceResolution" />.
        /// </summary>
        public UnityEngine.Vector2 ReferenceResolution
        {
            get
            {
                return (UnityEngine.Vector2)GetValue(ReferenceResolutionProperty);
            }

            set
            {
                SetValue(ReferenceResolutionProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.screenMatchMode" />.
        /// </summary>
        public UnityEngine.UI.CanvasScaler.ScreenMatchMode ScreenMatchMode
        {
            get
            {
                return (UnityEngine.UI.CanvasScaler.ScreenMatchMode)GetValue(ScreenMatchModeProperty);
            }

            set
            {
                SetValue(ScreenMatchModeProperty, value);
            }
        }

        /// <summary>
        /// A property that represents <see cref="P:UnityEngine.UI.CanvasScaler.matchWidthOrHeight" />.
        /// </summary>
        public float MatchWidthOrHeight
        {
            get
            {
                return (float)GetValue(MatchWidthOrHeightProperty);
            }

            set
            {
                SetValue(MatchWidthOrHeightProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void InitializeComponentInMainThread()
        {
            Component.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
            Component.referenceResolution = ReferenceResolution;
            Component.screenMatchMode = ScreenMatchMode;
            Component.matchWidthOrHeight = MatchWidthOrHeight;
        }
    }
}
