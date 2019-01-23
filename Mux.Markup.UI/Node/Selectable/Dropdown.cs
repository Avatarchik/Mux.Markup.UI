using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>
    /// A <see cref="BindableObject" /> that represents <see cref="T:UnityEngine.UI.Dropdown.OptionData" />.
    /// </summary>
    /// <remarks>
    /// You cannot add this to multiple <see cref="Dropdown" />.
    ///
    /// The lifetime will be bound to the lifetime of the <see cref="Dropdown" />.
    /// </remarks>
    public class DropdownOptionData : BindableObject
    {
        /// <summary>Backing store for the <see cref="Image" /> property.</summary>
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(
            "Image",
            typeof(UnityEngine.Sprite),
            typeof(DropdownOptionData),
            null,
            BindingMode.OneWay,
            null,
            OnImageChanged);

        /// <summary>Backing store for the <see cref="Text" /> property.</summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(
            "Text",
            typeof(string),
            typeof(DropdownOptionData),
            null,
            BindingMode.OneWay,
            null,
            OnTextChanged);

        internal Dropdown dropdown = null;

        private static void OnImageChanged(BindableObject sender, object oldValue, object newValue)
        {
            Forms.mainThread.Send(state =>
            {
                var data = (DropdownOptionData)state;
                data.data.image = data.Image;
                data.dropdown?.Component?.RefreshShownValue();
            }, sender);
        }

        private static void OnTextChanged(BindableObject sender, object oldValue, object newValue)
        {
            Forms.mainThread.Send(state =>
            {
                var data = (DropdownOptionData)state;
                data.data.text = data.Text;
                data.dropdown?.Component?.RefreshShownValue();
            }, sender);
        }

        internal readonly UnityEngine.UI.Dropdown.OptionData data = new UnityEngine.UI.Dropdown.OptionData();

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.OptionData.image" />.</summary>
        public UnityEngine.Sprite Image
        {
            get
            {
                return (UnityEngine.Sprite)GetValue(ImageProperty);
            }

            set
            {
                SetValue(ImageProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.OptionData.text" />.</summary>
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
    }

    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Dropdown" />.</summary>
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
    ///     <m:RectTransform>
    ///         <mu:ContentSizeFitter VerticalFit="PreferredSize" />
    ///         <mu:Dropdown>
    ///             <mu:Dropdown.Template>
    ///                 <Binding Path="Component" Source="{x:Reference Name=template}" />
    ///             </mu:Dropdown.Template>
    ///             <mu:Dropdown.CaptionText>
    ///                 <Binding Path="Component" Source="{x:Reference Name=captionText}" />
    ///             </mu:Dropdown.CaptionText>
    ///             <mu:Dropdown.ItemText>
    ///                 <Binding Path="Component" Source="{x:Reference Name=itemText}" />
    ///             </mu:Dropdown.ItemText>
    ///             <mu:Dropdown.Options>
    ///                 <mu:DropdownOptionData Text="A" />
    ///                 <mu:DropdownOptionData Text="B" />
    ///             </mu:Dropdown.Options>
    ///         </mu:Dropdown>
    ///         <mu:Text x:Name="captionText" />
    ///         <m:RectTransform x:Name="template" ActiveSelf="False" X="{m:Stretch}" Y="{m:Sized AnchoredPosition=2, SizeDelta=150, Anchor=0, Pivot=1}">
    ///             <m:RectTransform x:Name="viewport" X="{m:Stretch Pivot=0, OffsetMax=-18}" Y="{m:Stretch Pivot=1}">
    ///                 <m:RectTransform x:Name="content" Y="{m:Sized SizeDelta=28, Anchor=1, Pivot=1}">
    ///                     <m:RectTransform X="{m:Stretch}" Y="{m:Sized SizeDelta=21}">
    ///                         <mu:Toggle
    ///                             Graphic="{Binding Path=Component, Source={x:Reference Name=itemGraphic}}"
    ///                             TargetGraphic="{Binding Path=Component, Source={x:Reference Name=itemTargetGraphic}}" />
    ///                         <mu:Image x:Name="itemTargetGraphic" />
    ///                         <m:RectTransform X="{m:Stretch}" Y="{m:Stretch}">
    ///                             <mu:Image x:Name="itemGraphic" Color="{m:Color R=0, G=0, B=1, A=0.5}" />
    ///                         </m:RectTransform>
    ///                         <m:RectTransform X="{m:Stretch}" Y="{m:Stretch}">
    ///                             <mu:Text x:Name="itemText" />
    ///                         </m:RectTransform>
    ///                     </m:RectTransform>
    ///                 </m:RectTransform>
    ///             </m:RectTransform>
    ///             <mu:ScrollRect
    ///                 Viewport="{Binding Path=Component, Source={x:Reference Name=viewport}}"
    ///                 Content="{Binding Path=Component, Source={x:Reference Name=content}}" />
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    [ContentProperty("Options")]
    public class Dropdown : Selectable<UnityEngine.UI.Dropdown>
    {
        private sealed class TemplatableUnityOptionData : TemplatableCollectionList<UnityEngine.UI.Dropdown.OptionData>
        {
            public readonly List<UnityEngine.UI.Dropdown.OptionData> list = new List<UnityEngine.UI.Dropdown.OptionData>();

            protected override IList<UnityEngine.UI.Dropdown.OptionData> GetList()
            {
                return list;
            }

            public override void InsertListRange(int index, IEnumerable<UnityEngine.UI.Dropdown.OptionData> enumerable)
            {
                list.InsertRange(index, enumerable);
            }

            public override void RemoveListRange(int index, int count)
            {
                list.RemoveRange(index, count);
            }
        }

        private sealed class TemplatableOptionData : TemplatableCollection<DropdownOptionData>
        {
            private readonly ImmutableList<DropdownOptionData>.Builder _builder =
                ImmutableList.CreateBuilder<DropdownOptionData>();

            public readonly TemplatableUnityOptionData data = new TemplatableUnityOptionData();

            public TemplatableOptionData(BindableObject container) : base(container)
            {
            }

            protected override IList<DropdownOptionData> GetList()
            {
                return _builder;
            }

            public override void ClearList()
            {
                base.ClearList();
                data.ClearList();
                RefreshShownValue();
            }

            public override void InsertListRange(int index, IEnumerable<DropdownOptionData> enumerable)
            {
                foreach (var data in enumerable)
                {
                    data.dropdown = (Dropdown)container;
                }

                _builder.InsertRange(index, enumerable);
                data.InsertListRange(index, enumerable.Select(item => item.data));
                RefreshShownValue();
            }

            public override void MoveListRange(int from, int to, int count)
            {
                base.MoveListRange(from, to, count);
                data.MoveListRange(from, to, count);
                RefreshShownValue();
            }

            public override void RemoveListRange(int index, int count)
            {
                data.RemoveListRange(index, count);

                while (count > 0)
                {
                    _builder.RemoveAt(index);
                    count--;
                }

                RefreshShownValue();
            }

            public override void ReplaceListRange(int index, int count, IEnumerable<DropdownOptionData> enumerable)
            {
                foreach (var data in enumerable)
                {
                    data.dropdown = (Dropdown)container;
                }

                base.ReplaceListRange(index, count, enumerable);
                data.ReplaceListRange(index, count, enumerable.Select(item => item.data));
                RefreshShownValue();
            }

            private void RefreshShownValue()
            {
                var component = ((Dropdown)container).Component;

                if (component != null)
                {
                    Forms.mainThread.Send(state => component.RefreshShownValue(), null);
                }
            }

            public ImmutableList<DropdownOptionData> ToImmutable()
            {
                return _builder.ToImmutable();
            }
        }

        private static Lazy<UnityEngine.Object> s_builtinTemplatePrefab = new Lazy<UnityEngine.Object>(LoadBuiltinTemplatePrefab, false);

        private static UnityEngine.Object LoadBuiltinTemplatePrefab()
        {
            return UnityEngine.Resources.Load("Mux/Dropdown/Template");
        }

        private UnityEngine.GameObject _builtinTemplate;
        private UnityEngine.GameObject _builtinCaption;

        /// <summary>Backing store for the <see cref="Template" /> property.</summary>
        public static readonly BindableProperty TemplateProperty = BindableProperty.Create(
            "Template",
            typeof(UnityEngine.RectTransform),
            typeof(Dropdown),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnTemplateChanged);

        /// <summary>Backing store for the <see cref="CaptionText" /> property.</summary>
        public static readonly BindableProperty CaptionTextProperty = BindableProperty.Create(
            "CaptionText",
            typeof(UnityEngine.UI.Text),
            typeof(Dropdown),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnCaptionTextChanged);

        /// <summary>Backing store for the <see cref="CaptionImage" /> property.</summary>
        public static readonly BindableProperty CaptionImageProperty = CreateBindableComponentProperty<UnityEngine.UI.Image>(
            "CaptionImage",
            typeof(Dropdown),
            (component, value) => component.captionImage = value);

        /// <summary>Backing store for the <see cref="ItemText" /> property.</summary>
        public static readonly BindableProperty ItemTextProperty = CreateBindableComponentProperty<UnityEngine.UI.Text>(
            "ItemText",
            typeof(Dropdown),
            (component, value) => component.itemText = value);

        /// <summary>Backing store for the <see cref="ItemImage" /> property.</summary>
        public static readonly BindableProperty ItemImageProperty = CreateBindableComponentProperty<UnityEngine.UI.Image>(
            "ItemImage",
            typeof(Dropdown),
            (component, value) => component.itemImage = value);

        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = CreateBindableComponentProperty<int>(
            "Value",
            typeof(Dropdown),
            (component, value) =>
            {
                var old = component.onValueChanged;
                component.onValueChanged = new UnityEngine.UI.Dropdown.DropdownEvent();

                try
                {
                    component.value = value;
                }
                finally
                {
                    component.onValueChanged = old;
                }
            },
            0,
            BindingMode.TwoWay);

        /// <summary>Backing store for the <see cref="Options" /> property.</summary>
        /// <remarks>
        /// You cannot add the same <see cref="DropdownOptionData" /> to this property of multiple instances.
        ///
        /// This binds the lifetime of <see cref="DropdownOptionData" /> to the lifetime of the instance.
        /// </remarks>
        public static readonly BindableProperty OptionsProperty = BindableProperty.CreateReadOnly(
            "Options",
            typeof(ICollection<DropdownOptionData>),
            typeof(Dropdown),
            null,
            BindingMode.OneWayToSource,
            null,
            null,
            null,
            null,
            CreateDefaultOptions).BindableProperty;

        /// <summary>Backing store for the <see cref="OptionsSource" /> property.</summary>
        public static readonly BindableProperty OptionsSourceProperty = BindableProperty.Create(
            "OptionsSource",
            typeof(IEnumerable),
            typeof(Dropdown),
            null,
            BindingMode.OneWay,
            null,
            OnOptionsSourceChanged);

        /// <summary>Backing store for the <see cref="OptionTemplate" /> property.</summary>
        public static readonly BindableProperty OptionTemplateProperty = BindableProperty.Create(
            "OptionTemplate",
            typeof(DataTemplate),
            typeof(Dropdown),
            null,
            BindingMode.OneWay,
            null,
            OnOptionTemplateChanged);

        private static void OnTemplateChanged(BindableObject boxedDropdown, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var dropdown = (Dropdown)state;

                if (dropdown.Template == dropdown._builtinTemplate.GetComponent<UnityEngine.RectTransform>())
                {
                    dropdown._builtinTemplate.hideFlags = UnityEngine.HideFlags.None;

                    if (dropdown.Component != null)
                    {
                        dropdown._builtinTemplate.layer = dropdown.Component.gameObject.layer;
                        dropdown.Component.template = dropdown.Template;
                    }
                }
                else
                {
                    dropdown._builtinTemplate.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (dropdown.ItemText == dropdown._builtinTemplate.GetComponentInChildren<UnityEngine.UI.Text>())
                    {
                        dropdown.SetValueCore(ItemTextProperty, null);
                    }

                    if (dropdown.Component != null)
                    {
                        dropdown.Component.template = dropdown.Template;
                    }
                }
            }, boxedDropdown);
        }

        private static void OnCaptionTextChanged(BindableObject boxedDropdown, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var dropdown = (Dropdown)state;

                if (dropdown.CaptionText == dropdown._builtinCaption.GetComponent<UnityEngine.UI.Text>())
                {
                    dropdown._builtinCaption.hideFlags = UnityEngine.HideFlags.None;

                    if (dropdown.Component != null)
                    {
                        dropdown.Component.captionText = dropdown.CaptionText;
                    }
                }
                else
                {
                    dropdown._builtinCaption.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (dropdown.Component != null)
                    {
                        dropdown._builtinCaption.transform.SetParent(null);
                        dropdown.Component.captionText = dropdown.CaptionText;
                    }
                }
            }, boxedDropdown);
        }

        private static object CreateDefaultOptions(BindableObject sender)
        {
            return ((Dropdown)sender)._options;
        }

        private static void OnOptionsSourceChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                ((Dropdown)sender)._options.ChangeSource((IEnumerable)newValue);
            }
        }

        private static void OnOptionTemplateChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Dropdown)sender)._options.ChangeTemplate((DataTemplate)newValue);
        }

        private readonly TemplatableOptionData _options;

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.template" />.</summary>
        public UnityEngine.RectTransform Template
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(TemplateProperty);
            }

            set
            {
                SetValue(TemplateProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.captionText" />.</summary>
        public UnityEngine.UI.Text CaptionText
        {
            get
            {
                return (UnityEngine.UI.Text)GetValue(CaptionTextProperty);
            }

            set
            {
                SetValue(CaptionTextProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.captionImage" />.</summary>
        public UnityEngine.UI.Image CaptionImage
        {
            get
            {
                return (UnityEngine.UI.Image)GetValue(CaptionImageProperty);
            }

            set
            {
                SetValue(CaptionImageProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.itemText" />.</summary>
        public UnityEngine.UI.Text ItemText
        {
            get
            {
                return (UnityEngine.UI.Text)GetValue(ItemTextProperty);
            }

            set
            {
                SetValue(ItemTextProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.itemImage" />.</summary>
        public UnityEngine.UI.Image ItemImage
        {
            get
            {
                return (UnityEngine.UI.Image)GetValue(ItemImageProperty);
            }

            set
            {
                SetValue(ItemImageProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.value" />.</summary>
        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Dropdown.options" />.</summary>
        /// <remarks>This is the content property; you can write as child elements in XAML.</remarks>
        public ICollection<DropdownOptionData> Options => (ICollection<DropdownOptionData>)GetValue(OptionsProperty);

        /// <summary>Gets or sets the source of options to template and display.</summary>
        public IEnumerable OptionsSource
        {
            get
            {
                return (IEnumerable)GetValue(OptionsSourceProperty);
            }

            set
            {
                SetValue(OptionsSourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate" /> to apply to the <see cref="OptionsSource" />.
        /// </summary>
        public DataTemplate OptionTemplate
        {
            get
            {
                return (DataTemplate)GetValue(OptionTemplateProperty);
            }

            set
            {
                SetValue(OptionTemplateProperty, value);
            }
        }

        public Dropdown()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinTemplate = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinTemplatePrefab.Value);
                _builtinCaption = new UnityEngine.GameObject("Mux Builtin Dropdown Caption");

                SetValue(TemplateProperty, _builtinTemplate.GetComponent<UnityEngine.RectTransform>());
                SetValue(ItemTextProperty, _builtinTemplate.GetComponentInChildren<UnityEngine.UI.Text>());

                var builtinCaptionRect = _builtinCaption.AddComponent<UnityEngine.RectTransform>();
                builtinCaptionRect.anchorMin = UnityEngine.Vector2.zero;
                builtinCaptionRect.anchorMax = UnityEngine.Vector2.one;
                builtinCaptionRect.offsetMin = new UnityEngine.Vector2(10, 6);
                builtinCaptionRect.offsetMax = new UnityEngine.Vector2(-25, -7);

                SetValue(CaptionTextProperty, _builtinCaption.AddComponent<UnityEngine.UI.Text>());
                CaptionText.color = new UnityEngine.Color32(50, 50, 50, 255);
                CaptionText.font = UnityEngine.Resources.GetBuiltinResource<UnityEngine.Font>("Arial.ttf");
                CaptionText.alignment = UnityEngine.TextAnchor.MiddleLeft;
            }, null);

            _options = new TemplatableOptionData(this);
            _options.ChangeSource(OptionsSource);
            _options.ChangeTemplate(OptionTemplate);
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            foreach (var option in _options.ToImmutable())
            {
                SetInheritedBindingContext(option, BindingContext);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            _builtinTemplate.layer = gameObject.layer;
            _builtinTemplate.transform.SetParent(gameObject.transform, false);

            if (CaptionText == _builtinCaption.GetComponent<UnityEngine.UI.Text>())
            {
                _builtinCaption.layer = gameObject.layer;
                _builtinCaption.transform.SetParent(gameObject.transform, false);
            }

            Component.template = Template;
            Component.captionText = CaptionText;
            Component.captionImage = CaptionImage;
            Component.itemText = ItemText;
            Component.itemImage = ItemImage;
            Component.onValueChanged.AddListener(value => SetValueCore(ValueProperty, value));
            Component.value = Value;
            Component.options = _options.data.list;
            Component.RefreshShownValue();
        }
    }
}
