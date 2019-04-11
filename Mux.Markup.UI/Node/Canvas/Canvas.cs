using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Behaviour{T}" /> that represents <see cref="T:UnityEngine.Canvas" />.</summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <mu:StandaloneInputModule />
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
    public class Canvas : Behaviour<UnityEngine.Canvas>
    {
        /// <summary>Backing store for the <see cref="Render" /> property.</summary>
        public static readonly BindableProperty RenderProperty = BindableProperty.Create(
            "Render",
            typeof(Render),
            typeof(Canvas),
            null,
            BindingMode.OneWay,
            null,
            OnRenderChanged,
            null,
            null,
            sender => new ScreenSpaceOverlay());

        /// <summary>Backing store for the <see cref="AdditionalShaderChannels" /> property.</summary>
        public static readonly BindableProperty AdditionalShaderChannelsProperty = CreateBindableBodyProperty<UnityEngine.AdditionalCanvasShaderChannels>(
            "AdditionalShaderChannels",
            typeof(Canvas),
            (body, value) => body.additionalShaderChannels = value,
            UnityEngine.AdditionalCanvasShaderChannels.None);

        private static void OnRenderChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Render)oldValue)?.DestroyMux();

            if (newValue != null)
            {
                var body = ((Canvas)sender).Body;
                var render = (Render)newValue;

                if (body != null)
                {
                    body.renderMode = render.Mode;
                    render.Body = body;
                }

                SetInheritedBindingContext(render, sender.BindingContext);
            }
        }

        /// <summary>A property that represents <see cref="T:UnityEngine.RenderMode.ScreenSpaceCamera" /> and its rendering properties.</summary>
        /// <remarks>Setting <see cref="Render" /> to this property binds its lifetime to the lifetime of this object.</remarks>
        public Render Render
        {
            get
            {
                return (Render)GetValue(RenderProperty);
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

            if (Render != null)
            {
                // Setting renderMode of an enabled component causes properties of
                // RectTransform modified. Therefore, it must be set in AddToInMainThread.
                Body.renderMode = Render.Mode;
            }
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            Body.additionalShaderChannels = AdditionalShaderChannels;
            Render.Body = Body;

            base.AwakeInMainThread();
        }

        /// <inheritdoc />
        protected override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            Render.DestroyMux();
        }
    }
}
