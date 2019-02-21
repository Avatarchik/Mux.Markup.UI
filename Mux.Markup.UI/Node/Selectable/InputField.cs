using System;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.InputField" />.</summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mu:InputField
    ///         LineType="MultiLineNewline"
    ///         Placeholder="{Binding Path=Body, Source={x:Reference Name=placeholder}}"
    ///         TextComponent="{Binding Path=Body, Source={x:Reference Name=text}}">
    ///         <mu:InputField.Text>
    /// You have to give property name "Path" to Binding and "Name" to x:Reference
    /// only when you compile the interpreter with IL2CPP.
    /// It is because ContentPropertyAttribute does not work with IL2CPP.
    ///         </mu:InputField.Text>
    ///     </mu:InputField>
    ///     <mu:Image x:Name="placeholder" Color="{m:Color R=0, G=0, B=1}" />
    ///     <m:RectTransform X="{m:Stretch}" Y="{m:Stretch}">
    ///         <mu:Text x:Name="text" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class InputField : Selectable<UnityEngine.UI.InputField>
    {
        /// <summary>Backing store for the <see cref="TextComponent" /> property.</summary>
        public static readonly BindableProperty TextComponentProperty = BindableProperty.Create(
            "TextComponent",
            typeof(UnityEngine.UI.Text),
            typeof(InputField),
            null,
            BindingMode.OneWay,
            null,
            OnTextComponentChanged);

        /// <summary>Backing store for the <see cref="Text" /> property.</summary>
        public static readonly BindableProperty TextProperty = CreateBindableBodyProperty<string>(
            "Text",
            typeof(InputField),
            (body, value) =>
            {
                var old = body.onValueChanged;
                body.onValueChanged = new UnityEngine.UI.InputField.OnChangeEvent();

                try
                {
                    body.text = value;
                }
                finally
                {
                    body.onValueChanged = old;
                }
            },
            "",
            BindingMode.TwoWay);

        /// <summary>Backing store for the <see cref="ContentType" /> property.</summary>
        public static readonly BindableProperty ContentTypeProperty = CreateBindableBodyProperty<UnityEngine.UI.InputField.ContentType>(
            "ContentType",
            typeof(InputField),
            (body, value) => body.contentType = value,
            UnityEngine.UI.InputField.ContentType.Standard);

        /// <summary>Backing store for the <see cref="LineType" /> property.</summary>
        public static readonly BindableProperty LineTypeProperty = CreateBindableBodyProperty<UnityEngine.UI.InputField.LineType>(
            "LineType",
            typeof(InputField),
            (body, value) => body.lineType = value,
            UnityEngine.UI.InputField.LineType.SingleLine);

        /// <summary>Backing store for the <see cref="InputType" /> property.</summary>
        public static readonly BindableProperty InputTypeProperty = CreateBindableBodyProperty<UnityEngine.UI.InputField.InputType>(
            "InputType",
            typeof(InputField),
            (body, value) => body.inputType = value,
            UnityEngine.UI.InputField.InputType.Standard);

        /// <summary>Backing store for the <see cref="CharacterValidation" /> property.</summary>
        public static readonly BindableProperty CharacterValidationProperty = CreateBindableBodyProperty<UnityEngine.UI.InputField.CharacterValidation>(
            "CharacterValidation",
            typeof(InputField),
            (body, value) => body.characterValidation = value,
            UnityEngine.UI.InputField.CharacterValidation.None);

        /// <summary>Backing store for the <see cref="KeyboardType" /> property.</summary>
        public static readonly BindableProperty KeyboardTypeProperty = CreateBindableBodyProperty<UnityEngine.TouchScreenKeyboardType>(
            "KeyboardType",
            typeof(InputField),
            (body, value) => body.keyboardType = value,
            UnityEngine.TouchScreenKeyboardType.Default);

        /// <summary>Backing store for the <see cref="CharacterLimit" /> property.</summary>
        public static readonly BindableProperty CharacterLimitProperty = CreateBindableBodyProperty<int>(
            "CharacterLimit",
            typeof(InputField),
            (body, value) => body.characterLimit = value,
            0);

        /// <summary>Backing store for the <see cref="CaretBlinkRate" /> property.</summary>
        public static readonly BindableProperty CaretBlinkRateProperty = CreateBindableBodyProperty<float>(
            "CaretBlinkRate",
            typeof(InputField),
            (body, value) => body.caretBlinkRate = value,
            0.85f);

        /// <summary>Backing store for the <see cref="CaretWidth" /> property.</summary>
        public static readonly BindableProperty CaretWidthProperty = CreateBindableBodyProperty<int>(
            "CaretWidth",
            typeof(InputField),
            (body, value) => body.caretWidth = value,
            1);

        /// <summary>Backing store for the <see cref="CaretColor" /> property.</summary>
        public static readonly BindableProperty CaretColorProperty = CreateBindableBodyProperty<UnityEngine.Color>(
            "CaretColor",
            typeof(InputField),
            (body, value) => body.caretColor = value,
            new UnityEngine.Color32(50, 50, 50, 255));

        /// <summary>Backing store for the <see cref="CustomCaretColor" /> property.</summary>
        public static readonly BindableProperty CustomCaretColorProperty = CreateBindableBodyProperty<bool>(
            "CustomCaretColor",
            typeof(InputField),
            (body, value) => body.customCaretColor = value,
            false);

        /// <summary>Backing store for the <see cref="SelectionColor" /> property.</summary>
        public static readonly BindableProperty SelectionColorProperty = CreateBindableBodyProperty<UnityEngine.Color>(
            "SelectionColor",
            typeof(InputField),
            (body, value) => body.selectionColor = value,
            new UnityEngine.Color32(168, 206, 255, 192));

        /// <summary>Backing store for the <see cref="HideMobileInput" /> property.</summary>
        public static readonly BindableProperty HideMobileInputProperty = CreateBindableBodyProperty<bool>(
            "HideMobileInput",
            typeof(InputField),
            (body, value) => body.shouldHideMobileInput = value,
            false);

        /// <summary>Backing store for the <see cref="Placeholder" /> property.</summary>
        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            "Placeholder",
            typeof(UnityEngine.UI.Graphic),
            typeof(InputField),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnPlaceholderChanged);

        /// <summary>Backing store for the <see cref="ReadOnly" /> property.</summary>
        public static readonly BindableProperty ReadOnlyProperty = CreateBindableBodyProperty<bool>(
            "ReadOnly",
            typeof(InputField),
            (body, value) => body.readOnly = value,
            false);

        private static void OnTextComponentChanged(BindableObject boxedInputField, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var inputField = (InputField)state;

                if (inputField.TextComponent == inputField._builtinText.GetComponent<UnityEngine.UI.Text>())
                {
                    inputField._builtinText.hideFlags = UnityEngine.HideFlags.None;

                    if (inputField.Body != null)
                    {
                        inputField._builtinText.layer = inputField.Body.gameObject.layer;
                        inputField._builtinText.transform.SetParent(inputField.Body.gameObject.transform, false);
                        inputField.Body.textComponent = inputField.TextComponent;
                        inputField.Body.ForceLabelUpdate();
                    }
                }
                else
                {
                    inputField._builtinText.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (inputField.Body != null)
                    {
                        inputField._builtinText.layer = inputField.Body.gameObject.layer;
                        inputField._builtinText.transform.SetParent(inputField.Body.gameObject.transform, false);
                        inputField.Body.textComponent = inputField.TextComponent;
                        inputField.Body.ForceLabelUpdate();
                    }
                }
            }, boxedInputField);
        }

        private static void OnPlaceholderChanged(BindableObject boxedInputField, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var inputField = (InputField)state;

                if (inputField.Placeholder == inputField._builtinPlaceholder.GetComponent<UnityEngine.UI.Text>())
                {
                    inputField._builtinPlaceholder.hideFlags = UnityEngine.HideFlags.None;

                    if (inputField.Body != null)
                    {
                        inputField._builtinPlaceholder.layer = inputField.Body.gameObject.layer;
                        inputField._builtinPlaceholder.transform.SetParent(inputField.Body.gameObject.transform, false);
                        inputField.Body.placeholder = inputField.Placeholder;
                    }
                }
                else
                {
                    inputField._builtinPlaceholder.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (inputField.Body != null)
                    {
                        inputField._builtinText.transform.SetParent(null);
                        inputField.Body.placeholder = inputField.Placeholder;
                    }
                }
            }, boxedInputField);
        }

        private UnityEngine.GameObject _builtinText;
        private UnityEngine.GameObject _builtinPlaceholder;
        private UnityEngine.UI.InputField.SubmitEvent _onEndEdit;

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.textComponent" />.</summary>
        /// <seealso cref="Text" />
        public UnityEngine.UI.Text TextComponent
        {
            get
            {
                return (UnityEngine.UI.Text)GetValue(TextComponentProperty);
            }

            set
            {
                SetValue(TextComponentProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.text" />.</summary>
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }

            set
            {
                SetValue(TextProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.contentType" />.</summary>
        public UnityEngine.UI.InputField.ContentType ContentType
        {
            get
            {
                return (UnityEngine.UI.InputField.ContentType)GetValue(ContentTypeProperty);
            }

            set
            {
                SetValue(ContentTypeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.lineType" />.</summary>
        public UnityEngine.UI.InputField.LineType LineType
        {
            get
            {
                return (UnityEngine.UI.InputField.LineType)GetValue(LineTypeProperty);
            }

            set
            {
                SetValue(LineTypeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.inputType" />.</summary>
        public UnityEngine.UI.InputField.InputType InputType
        {
            get
            {
                return (UnityEngine.UI.InputField.InputType)GetValue(InputTypeProperty);
            }

            set
            {
                SetValue(InputTypeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.characterValidation" />.</summary>
        public UnityEngine.UI.InputField.CharacterValidation CharacterValidation
        {
            get
            {
                return (UnityEngine.UI.InputField.CharacterValidation)GetValue(CharacterValidationProperty);
            }

            set
            {
                SetValue(CharacterValidationProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.keyboardType" />.</summary>
        public UnityEngine.TouchScreenKeyboardType KeyboardType
        {
            get
            {
                return (UnityEngine.TouchScreenKeyboardType)GetValue(KeyboardTypeProperty);
            }

            set
            {
                SetValue(KeyboardTypeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.characterLimit" />.</summary>
        public int CharacterLimit
        {
            get
            {
                return (int)GetValue(CharacterLimitProperty);
            }

            set
            {
                SetValue(CharacterLimitProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.caretBlinkRate" />.</summary>
        public float CaretBlinkRate
        {
            get
            {
                return (float)GetValue(CaretBlinkRateProperty);
            }

            set
            {
                SetValue(CaretBlinkRateProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.caretWidth" />.</summary>
        /// <seealso cref="Color" />
        /// <seealso cref="Color32" />
        public int CaretWidth
        {
            get
            {
                return (int)GetValue(CaretWidthProperty);
            }

            set
            {
                SetValue(CaretWidthProperty, value);
            }
        }

        /// <summary>
        /// A property that represents
        /// <see cref="P:UnityEngine.UI.InputField.caretColor" />.
        /// </summary>
        /// <seealso cref="Color" />
        /// <seealso cref="Color32" />
        public UnityEngine.Color CaretColor
        {
            get
            {
                return (UnityEngine.Color)GetValue(CaretColorProperty);
            }

            set
            {
                SetValue(CaretColorProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.customCaretColor" />.</summary>
        public bool CustomCaretColor
        {
            get
            {
                return (bool)GetValue(CustomCaretColorProperty);
            }

            set
            {
                SetValue(CustomCaretColorProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.selectionColor" />.</summary>
        /// <seealso cref="Color" />
        /// <seealso cref="Color32" />
        public UnityEngine.Color SelectionColor
        {
            get
            {
                return (UnityEngine.Color)GetValue(SelectionColorProperty);
            }

            set
            {
                SetValue(SelectionColorProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.shouldHideMobileInput" />.</summary>
        public bool HideMobileInput
        {
            get
            {
                return (bool)GetValue(HideMobileInputProperty);
            }

            set
            {
                SetValue(HideMobileInputProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.placeholder" />.</summary>
        /// <seealso cref="Graphic{T}" />
        public UnityEngine.UI.Graphic Placeholder
        {
            get
            {
                return (UnityEngine.UI.Graphic)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.InputField.readOnly" />.</summary>
        public bool ReadOnly
        {
            get
            {
                return (bool)GetValue(ReadOnlyProperty);
            }

            set
            {
                SetValue(ReadOnlyProperty, value);
            }
        }

        /// <summary>An event that represents <see cref="P:UnityEngine.UI.InputField.onEndEdit" />.</summary>
        public event UnityEngine.Events.UnityAction<string> OnEndEdit
        {
            add
            {
                Forms.mainThread.Send(state => _onEndEdit.AddListener(value), null);
            }

            remove
            {
                Forms.mainThread.Send(state => _onEndEdit.RemoveListener(value), null);
            }
        }

        public InputField()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinText = new UnityEngine.GameObject("Mux Builtin InputField Text");
                _builtinPlaceholder = new UnityEngine.GameObject("Mux Builtin InputField Placeholder");
                _onEndEdit = new UnityEngine.UI.InputField.SubmitEvent();

                var builtinTextRect = _builtinText.AddComponent<UnityEngine.RectTransform>();
                builtinTextRect.anchorMin = UnityEngine.Vector2.zero;
                builtinTextRect.anchorMax = UnityEngine.Vector2.one;
                builtinTextRect.sizeDelta = UnityEngine.Vector2.zero;
                builtinTextRect.offsetMin = new UnityEngine.Vector2(10, 6);
                builtinTextRect.offsetMax = new UnityEngine.Vector2(-10, -7);

                var builtinTextComponent = _builtinText.AddComponent<UnityEngine.UI.Text>();
                builtinTextComponent.color = new UnityEngine.Color32(50, 50, 50, 255);
                builtinTextComponent.font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
                builtinTextComponent.text = "";
                builtinTextComponent.supportRichText = false;

                var builtinPlaceholderRect = _builtinPlaceholder.AddComponent<UnityEngine.RectTransform>();
                builtinPlaceholderRect.anchorMin = UnityEngine.Vector2.zero;
                builtinPlaceholderRect.anchorMax = UnityEngine.Vector2.one;
                builtinPlaceholderRect.sizeDelta = UnityEngine.Vector2.zero;
                builtinPlaceholderRect.offsetMin = new UnityEngine.Vector2(10, 6);
                builtinPlaceholderRect.offsetMax = new UnityEngine.Vector2(-10, -7);

                var builtinPlaceholderComponent = _builtinPlaceholder.AddComponent<UnityEngine.UI.Text>();
                builtinPlaceholderComponent.text = "Enter text...";
                builtinPlaceholderComponent.fontStyle = UnityEngine.FontStyle.Italic;
                var builtinPlaceholderColor = builtinTextComponent.color;
                builtinPlaceholderColor.a *= 0.5f;
                builtinPlaceholderComponent.color = builtinPlaceholderColor;

                SetValue(TextComponentProperty, builtinTextComponent);
                SetValue(PlaceholderProperty, builtinPlaceholderComponent);
            }, null);
        }

        /// <inheritdoc />
        protected override void AwakeInMainThread()
        {
            base.AwakeInMainThread();

            if (TextComponent == _builtinText.GetComponent<UnityEngine.UI.Text>())
            {
                _builtinText.layer = Body.gameObject.layer;
                _builtinText.transform.SetParent(Body.transform, false);
            }

            if (Placeholder == _builtinPlaceholder.GetComponent<UnityEngine.UI.Text>())
            {
                _builtinPlaceholder.layer = Body.gameObject.layer;
                _builtinPlaceholder.transform.SetParent(Body.transform, false);
            }

            Body.textComponent = TextComponent;
            Body.text = Text;
            Body.contentType = ContentType;
            Body.lineType = LineType;
            Body.inputType = InputType;
            Body.characterValidation = CharacterValidation;
            Body.keyboardType = KeyboardType;
            Body.characterLimit = CharacterLimit;
            Body.caretBlinkRate = CaretBlinkRate;
            Body.caretWidth = CaretWidth;
            Body.caretColor = CaretColor;
            Body.customCaretColor = CustomCaretColor;
            Body.selectionColor = SelectionColor;
            Body.shouldHideMobileInput = HideMobileInput;
            Body.placeholder = Placeholder;
            Body.readOnly = ReadOnly;
            Body.onEndEdit = _onEndEdit;
            Body.onValueChanged.AddListener(value => SetValueCore(TextProperty, value));

            // The following line has a long story of uGUI internals.
            // InputField registers a callback for vertices change of text
            // component to update the state of label. The callback, however,
            // does not only update the label according to the vertices but
            // also disables the label if necessary. Disabling a label would
            // tell CanvasUpdateRegistry to remove it from the layout.
            // That causes potential layout-rebuilding loop. A callback, which
            // depends on the layout state, may change the same state, and
            // trigger itself again! CanvasUpdateRegistry detects the potential
            // loop and stops removing the label. That is not good.
            // Fortunately, the decision of disabling or enabling the
            // label does not depend on the layout. So we can determine the
            // enabled/disabled state before changing the layout, and that is
            // whath this one line does.
            Body.ForceLabelUpdate();
        }
    }
}
