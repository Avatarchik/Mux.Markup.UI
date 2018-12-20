using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Graphic{T}" /> that represents <see cref="T:UnityEngine.UI.Image" />.</summary>
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
    ///     <mu:Image />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Image : Graphic<UnityEngine.UI.Image>
    {
        /// <summary>Backing store for the <see cref="Sprite" /> property.</summary>
        public static readonly BindableProperty SpriteProperty = CreateBindableComponentProperty<UnityEngine.Sprite>(
            "Sprite",
            typeof(Image),
            (component, value) => component.sprite = value);

        /// <summary>Backing store for the <see cref="Type" /> property.</summary>
        public static readonly BindableProperty TypeProperty = CreateBindableComponentProperty<UnityEngine.UI.Image.Type>(
            "Type",
            typeof(Image),
            (component, value) => component.type = value,
            UnityEngine.UI.Image.Type.Simple);

        /// <summary>Backing store for the <see cref="FillCenter" /> property.</summary>
        public static readonly BindableProperty FillCenterProperty = CreateBindableComponentProperty<bool>(
            "FillCenter",
            typeof(Image),
            (component, value) => component.fillCenter = value,
            true);

        /// <summary>Backing store for the <see cref="FillMethod" /> property.</summary>
        public static readonly BindableProperty FillMethodProperty = CreateBindableComponentProperty<UnityEngine.UI.Image.FillMethod>(
            "FillMethod",
            typeof(Image),
            (component, value) => component.fillMethod = value,
            UnityEngine.UI.Image.FillMethod.Radial360);

        /// <summary>Backing store for the <see cref="FillOrigin" /> property.</summary>
        public static readonly BindableProperty FillOriginProperty = CreateBindableComponentProperty<int>(
            "FillOrigin",
            typeof(Image),
            (component, value) => component.fillOrigin = value,
            0);

        /// <summary>Backing store for the <see cref="FillClockwise" /> property.</summary>
        public static readonly BindableProperty FillClockwiseProperty = CreateBindableComponentProperty<bool>(
            "FillClockwise",
            typeof(Image),
            (component, value) => component.fillClockwise = value,
            true);

        /// <summary>Backing store for the <see cref="FillAmount" /> property.</summary>
        public static readonly BindableProperty FillAmountProperty = CreateBindableComponentProperty<float>(
            "FillAmount",
            typeof(Image),
            (component, value) => component.fillAmount = value,
            1f);

        /// <summary>Backing store for the <see cref="PreserveAspect" /> property.</summary>
        public static readonly BindableProperty PreserveAspectProperty = CreateBindableComponentProperty<bool>(
            "PreserveAspect",
            typeof(Image),
            (component, value) => component.preserveAspect = value,
            false);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.sprite" />.</summary>
        public UnityEngine.Sprite Sprite
        {
            get
            {
                return (UnityEngine.Sprite)GetValue(SpriteProperty);
            }

            set
            {
                SetValue(SpriteProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.type" />.</summary>
        public UnityEngine.UI.Image.Type Type
        {
            get
            {
                return (UnityEngine.UI.Image.Type)GetValue(TypeProperty);
            }

            set
            {
                SetValue(TypeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.fillCenter" />.</summary>
        public bool FillCenter
        {
            get
            {
                return (bool)GetValue(FillCenterProperty);
            }

            set
            {
                SetValue(FillCenterProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.fillMethod" />.</summary>
        public UnityEngine.UI.Image.FillMethod FillMethod
        {
            get
            {
                return (UnityEngine.UI.Image.FillMethod)GetValue(FillMethodProperty);
            }

            set
            {
                SetValue(FillMethodProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.fillOrigin" />.</summary>
        public int FillOrigin
        {
            get
            {
                return (int)GetValue(FillOriginProperty);
            }

            set
            {
                SetValue(FillOriginProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.fillClockwise" />.</summary>
        public bool FillClockwise
        {
            get
            {
                return (bool)GetValue(FillClockwiseProperty);
            }

            set
            {
                SetValue(FillClockwiseProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.fillAmount" />.</summary>
        public float FillAmount
        {
            get
            {
                return (float)GetValue(FillAmountProperty);
            }

            set
            {
                SetValue(FillAmountProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Image.preserveAspect" />.</summary>
        public bool PreserveAspect
        {
            get
            {
                return (bool)GetValue(PreserveAspectProperty);
            }

            set
            {
                SetValue(PreserveAspectProperty, value);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            Component.sprite = Sprite;
            Component.type = Type;
            Component.fillCenter = FillCenter;
            Component.fillMethod = FillMethod;
            Component.fillOrigin = FillOrigin;
            Component.fillClockwise = FillClockwise;
            Component.fillAmount = FillAmount;
            Component.preserveAspect = PreserveAspect;
        }
    }
}
