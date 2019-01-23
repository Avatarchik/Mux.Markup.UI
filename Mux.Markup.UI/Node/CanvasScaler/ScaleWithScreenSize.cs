using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A class that represents <see cref="F:UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize" />
    /// and its scaling properties.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ScaleWithScreenSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:Text>
    ///         <mu:Text.Content>
    /// > Using the Scale With Screen Size mode, positions and sizes can be specified according to the pixels of a specified reference resolution.
    /// > If the current screen resolution is larger than the reference resolution, the Canvas will keep having only the resolution of the reference resolution,
    /// > but will scale up in order to fit the screen. If the current screen resolution is smaller than the reference resolution,
    /// > the Canvas will similarly be scaled down to fit.
    /// Unity - Scripting API: UI.CanvasScaler.ScaleMode.ScaleWithScreenSize
    /// https://docs.unity3d.com/ScriptReference/UI.CanvasScaler.ScaleMode.ScaleWithScreenSize.html
    ///         </mu:Text.Content>
    ///     </mu:Text>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
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
                Forms.mainThread.Send(
                    state => component.referenceResolution = (UnityEngine.Vector2)state,
                    newValue);
            }
        }

        private static void OnScreenMatchModeChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScaleWithScreenSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(
                    state => component.screenMatchMode = (UnityEngine.UI.CanvasScaler.ScreenMatchMode)state,
                    newValue);
            }
        }

        private static void OnMatchWidthOrHeightChanged(BindableObject sender, object oldValue, object newValue)
        {
            var component = ((ScaleWithScreenSize)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.matchWidthOrHeight = (float)state, newValue);
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
