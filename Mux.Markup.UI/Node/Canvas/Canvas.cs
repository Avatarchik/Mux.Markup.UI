using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Component{T}" /> that represents <see cref="T:UnityEngine.Canvas" />.</summary>
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
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:Text>
    ///         <mu:Text.Content>
    /// mu:Canvas is required anything based on uGUI.
    /// See what happens to this text if you remove the mu:Canvas!
    ///         </mu:Text.Content>
    ///     </mu:Text>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Canvas : Component<UnityEngine.Canvas>
    {
        /// <summary>Backing store for the <see cref="Render" /> property.</summary>
        public static readonly BindableProperty RenderProperty = CreateBindableModifierProperty(
            "Render",
            typeof(Canvas),
            sender => new ScreenSpaceOverlay());

        /// <summary>Backing store for the <see cref="AdditionalShaderChannels" /> property.</summary>
        public static readonly BindableProperty AdditionalShaderChannelsProperty = CreateBindableBodyProperty<UnityEngine.AdditionalCanvasShaderChannels>(
            "AdditionalShaderChannels",
            typeof(Canvas),
            (body, value) => body.additionalShaderChannels = value,
            UnityEngine.AdditionalCanvasShaderChannels.None);

        /// <summary>A property that represents <see cref="T:UnityEngine.RenderMode.ScreenSpaceCamera" /> and its rendering properties.</summary>
        /// <remarks>Setting <see cref="Component{T:UnityEngine.Canvas}.Modifier" /> to this property binds its lifetime to the lifetime of this object.</remarks>
        public Modifier Render
        {
            get
            {
                return (Modifier)GetValue(RenderProperty);
            }

            set
            {
                SetValue(RenderProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.Canvas.additionalShaderChannels" />.</summary>
        public UnityEngine.AdditionalCanvasShaderChannels AdditionalShaderChannels
        {
            get
            {
                return (UnityEngine.AdditionalCanvasShaderChannels)GetValue(AdditionalShaderChannelsProperty);
            }

            set
            {
                SetValue(AdditionalShaderChannelsProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(Render, BindingContext);
        }

        /// <inhertidoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            // Setting renderMode of an enabled component causes properties of
            // RectTransform modified. Therefore, those values must be set in
            // AddToInMainThread.
            Render.Body = Body;
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();

            Body.additionalShaderChannels = AdditionalShaderChannels;
        }

        /// <inheritdoc />
        protected override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            Render.DestroyMux();
        }
    }
}
