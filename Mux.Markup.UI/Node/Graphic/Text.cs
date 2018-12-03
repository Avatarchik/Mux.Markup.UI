using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Graphic{T}" /> that represents <see cref="T:UnityEngine.UI.Text" />.</summary>
    public class Text : Graphic<UnityEngine.UI.Text>
    {
        /// <summary>Backing store for the <see cref="Content" /> property.</summary>
        public static readonly BindableProperty ContentProperty = CreateBindableComponentProperty<string>(
            "Content",
            typeof(Text),
            (component, value) => component.text = value,
            "New Text");

        /// <summary>Backing store for the <see cref="Font" /> property.</summary>
        public static readonly BindableProperty FontProperty = CreateBindableComponentProperty<UnityEngine.Font>(
            "Font",
            typeof(Text),
            (component, value) => component.font = value,
            UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf"));

        /// <summary>Backing store for the <see cref="FontSize" /> property.</summary>
        public static readonly BindableProperty FontSizeProperty = CreateBindableComponentProperty<int>(
            "FontSize",
            typeof(Text),
            (component, value) => component.fontSize = value,
            14);

        /// <summary>Backing store for the <see cref="FontStyle" /> property.</summary>
        public static readonly BindableProperty FontStyleProperty = CreateBindableComponentProperty<UnityEngine.FontStyle>(
            "FontStyle",
            typeof(Text),
            (component, value) => component.fontStyle = value,
            UnityEngine.FontStyle.Normal);

        /// <summary>Backing store for the <see cref="ResizeTextForBestFit" /> property.</summary>
        public static readonly BindableProperty ResizeTextForBestFitProperty = CreateBindableComponentProperty<bool>(
            "ResizeTextForBestFit",
            typeof(Text),
            (component, value) => component.resizeTextForBestFit = value,
            false);

        /// <summary>Backing store for the <see cref="ResizeTextMinSize" /> property.</summary>
        public static readonly BindableProperty ResizeTextMinSizeProperty = CreateBindableComponentProperty<int>(
            "ResizeTextMinSize",
            typeof(Text),
            (component, value) => component.resizeTextMinSize = value,
            10);

        /// <summary>Backing store for the <see cref="ResizeTextMaxSize" /> property.</summary>
        public static readonly BindableProperty ResizeTextMaxSizeProperty = CreateBindableComponentProperty<int>(
            "ResizeTextMaxSize",
            typeof(Text),
            (component, value) => component.resizeTextMaxSize = value,
            40);

        /// <summary>Backing store for the <see cref="Alignment" /> property.</summary>
        public static readonly BindableProperty AlignmentProperty = CreateBindableComponentProperty<UnityEngine.TextAnchor>(
            "Alignment",
            typeof(Text),
            (component, value) => component.alignment = value,
            UnityEngine.TextAnchor.UpperLeft);

        /// <summary>Backing store for the <see cref="AlignByGeometry" /> property.</summary>
        public static readonly BindableProperty AlignByGeometryProperty = CreateBindableComponentProperty<bool>(
            "AlignByGeometry",
            typeof(Text),
            (component, value) => component.alignByGeometry = value,
            false);

        /// <summary>Backing store for the <see cref="SupportRichText" /> property.</summary>
        public static readonly BindableProperty SupportRichTextProperty = CreateBindableComponentProperty<bool>(
            "SupportRichText",
            typeof(Text),
            (component, value) => component.supportRichText = value,
            true);

        /// <summary>Backing store for the <see cref="HorizontalOverflow" /> property.</summary>
        public static readonly BindableProperty HorizontalOverflowProperty = CreateBindableComponentProperty<UnityEngine.HorizontalWrapMode>(
            "HorizontalOverflow",
            typeof(Text),
            (component, value) => component.horizontalOverflow = value,
            UnityEngine.HorizontalWrapMode.Wrap);

        /// <summary>Backing store for the <see cref="VerticalOverflow" /> property.</summary>
        public static readonly BindableProperty VerticalOverflowProperty = CreateBindableComponentProperty<UnityEngine.VerticalWrapMode>(
            "VerticalOverflow",
            typeof(Text),
            (component, value) => component.verticalOverflow = value,
            UnityEngine.VerticalWrapMode.Truncate);

        /// <summary>Backing store for the <see cref="LineSpacing" /> property.</summary>
        public static readonly BindableProperty LineSpacingProperty = CreateBindableComponentProperty<float>(
            "LineSpacing",
            typeof(Text),
            (component, value) => component.lineSpacing = value,
            1f);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.text" />.</summary>
        public string Content
        {
            get
            {
                return (string)GetValue(ContentProperty);
            }

            set
            {
                SetValue(ContentProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.font" />.</summary>
        public UnityEngine.Font Font
        {
            get
            {
                return (UnityEngine.Font)GetValue(FontProperty);
            }

            set
            {
                SetValue(FontProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.fontSize" />.</summary>
        public int FontSize
        {
            get
            {
                return (int)GetValue(FontSizeProperty);
            }

            set
            {
                SetValue(FontSizeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.fontStyle" />.</summary>
        public UnityEngine.FontStyle FontStyle
        {
            get
            {
                return (UnityEngine.FontStyle)GetValue(FontStyleProperty);
            }

            set
            {
                SetValue(FontStyleProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.resizeTextForBestFit" />.</summary>
        public bool ResizeTextForBestFit
        {
            get
            {
                return (bool)GetValue(ResizeTextForBestFitProperty);
            }

            set
            {
                SetValue(ResizeTextForBestFitProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.resizeTextMinSize" />.</summary>
        public int ResizeTextMinSize
        {
            get
            {
                return (int)GetValue(ResizeTextMinSizeProperty);
            }

            set
            {
                SetValue(ResizeTextMinSizeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.resizeTextMaxSize" />.</summary>
        public int ResizeTextMaxSize
        {
            get
            {
                return (int)GetValue(ResizeTextMaxSizeProperty);
            }

            set
            {
                SetValue(ResizeTextMaxSizeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.alignment" />.</summary>
        public UnityEngine.TextAnchor Alignment
        {
            get
            {
                return (UnityEngine.TextAnchor)GetValue(AlignmentProperty);
            }

            set
            {
                SetValue(AlignmentProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.alignByGeometry" />.</summary>
        public bool AlignByGeometry
        {
            get
            {
                return (bool)GetValue(AlignByGeometryProperty);
            }

            set
            {
                SetValue(AlignByGeometryProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.supportRichText" />.</summary>
        public bool SupportRichText
        {
            get
            {
                return (bool)GetValue(SupportRichTextProperty);
            }

            set
            {
                SetValue(SupportRichTextProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.horizontalOverflow" />.</summary>
        public UnityEngine.HorizontalWrapMode HorizontalOverflow
        {
            get
            {
                return (UnityEngine.HorizontalWrapMode)GetValue(HorizontalOverflowProperty);
            }

            set
            {
                SetValue(HorizontalOverflowProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.verticalOverflow" />.</summary>
        public UnityEngine.VerticalWrapMode VerticalOverflow
        {
            get
            {
                return (UnityEngine.VerticalWrapMode)GetValue(VerticalOverflowProperty);
            }

            set
            {
                SetValue(VerticalOverflowProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Text.lineSpacing" />.</summary>
        public float LineSpacing
        {
            get
            {
                return (float)GetValue(LineSpacingProperty);
            }

            set
            {
                SetValue(LineSpacingProperty, value);
            }
        }

        public Text()
        {
            SetValueCore(ColorProperty, new UnityEngine.Color32(50, 50, 50, 255));
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            Component.text = Content;
            Component.font = Font;
            Component.fontSize = FontSize;
            Component.fontStyle = FontStyle;
            Component.resizeTextForBestFit = ResizeTextForBestFit;
            Component.resizeTextMinSize = ResizeTextMinSize;
            Component.resizeTextMaxSize = ResizeTextMaxSize;
            Component.alignment = Alignment;
            Component.alignByGeometry = AlignByGeometry;
            Component.supportRichText = SupportRichText;
            Component.horizontalOverflow = HorizontalOverflow;
            Component.verticalOverflow = VerticalOverflow;
            Component.lineSpacing = LineSpacing;
        }
    }
}
