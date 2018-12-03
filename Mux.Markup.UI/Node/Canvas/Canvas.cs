using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.Canvas" />.</summary>
    public class Canvas : Object<UnityEngine.Canvas>
    {
        /// <summary>Backing store for the <see cref="Render" /> property.</summary>
        public static readonly BindableProperty RenderProperty = CreateBindableModifierProperty(
            "Render",
            typeof(Canvas),
            sender => new ScreenSpaceOverlay());

        /// <summary>Backing store for the <see cref="AdditionalShaderChannels" /> property.</summary>
        public static readonly BindableProperty AdditionalShaderChannelsProperty = CreateBindableComponentProperty<UnityEngine.AdditionalCanvasShaderChannels>(
            "AdditionalShaderChannels",
            typeof(Canvas),
            (component, value) => component.additionalShaderChannels = value,
            UnityEngine.AdditionalCanvasShaderChannels.None);

        /// <summary>A property that represents <see cref="T:UnityEngine.RenderMode.ScreenSpaceCamera" /> and its rendering properties.</summary>
        /// <remarks>Setting <see cref="Object{T:UnityEngine.Canvas}.Modifier" /> to this property binds its lifetime to the lifetime of this object.</remarks>
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

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.Canvas>();
            Component.additionalShaderChannels = AdditionalShaderChannels;
            Render.Component = Component;
        }

        internal override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            Render.DestroyMux();
        }
    }
}
