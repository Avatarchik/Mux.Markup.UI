﻿using System;
using System.Collections;
using System.Collections.Generic;
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
            Forms.mainThread.Post(state =>
            {
                var data = (DropdownOptionData)state;
                data.data.image = data.Image;
                data.dropdown?.Component?.RefreshShownValue();
            }, sender);
        }

        private static void OnTextChanged(BindableObject sender, object oldValue, object newValue)
        {
            Forms.mainThread.Post(state =>
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
    [ContentProperty("Options")]
    public class Dropdown : Selectable<UnityEngine.UI.Dropdown>
    {
        private sealed class TemplatableOptionData : TemplatableCollection<DropdownOptionData>
        {
            public readonly TemplatableCollectionList<UnityEngine.UI.Dropdown.OptionData> data =
                new TemplatableCollectionList<UnityEngine.UI.Dropdown.OptionData>();

            public TemplatableOptionData(BindableObject container) : base(container)
            {
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

                base.InsertListRange(index, enumerable);
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
                base.RemoveListRange(index, count);
                data.RemoveListRange(index, count);
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
                    Forms.mainThread.Post(state => ((UnityEngine.UI.Dropdown)state).RefreshShownValue(), component);
                }
            }
        }

        private static Lazy<UnityEngine.Object> s_builtinTemplatePrefab = new Lazy<UnityEngine.Object>(LoadBuiltinTemplatePrefab, false);

        private static UnityEngine.Object LoadBuiltinTemplatePrefab()
        {
            return UnityEngine.Resources.Load("Dropdown/Template");
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
            Forms.mainThread.Post(state =>
            {
                var dropdown = (Dropdown)state;

                if (dropdown.Template == dropdown._builtinTemplate.GetComponent<UnityEngine.RectTransform>())
                {
                    dropdown._builtinTemplate.hideFlags = UnityEngine.HideFlags.None;

                    if (dropdown.Component != null)
                    {
                        dropdown._builtinTemplate.layer = dropdown.Component.gameObject.layer;
                        dropdown.Template.SetParent(dropdown.Component.gameObject.transform, false);
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
                        dropdown.Template.SetParent(null);
                        dropdown.Component.template = dropdown.Template;
                    }
                }
            }, boxedDropdown);
        }

        private static void OnCaptionTextChanged(BindableObject boxedDropdown, object boxedOldValue, object boxedNewValue)
        {
            var dropdown = (Dropdown)boxedDropdown;
            var component = dropdown.Component;

            Forms.mainThread.Post(state =>
            {
                var newValue = (UnityEngine.UI.Text)state;

                if (newValue == dropdown._builtinCaption.GetComponent<UnityEngine.UI.Text>())
                {
                    dropdown._builtinCaption.hideFlags = UnityEngine.HideFlags.None;

                    if (component != null)
                    {
                        dropdown._builtinCaption.layer = component.gameObject.layer;
                        dropdown._builtinCaption.transform.SetParent(component.gameObject.transform, false);
                        component.captionText = newValue;
                    }
                }
                else
                {
                    dropdown._builtinCaption.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (component != null)
                    {
                        dropdown._builtinCaption.transform.SetParent(null);
                        component.captionText = newValue;
                    }
                }
            }, boxedNewValue);
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
            foreach (var option in _options)
            {
                SetInheritedBindingContext(option, BindingContext);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            if (Template == _builtinTemplate.GetComponent<UnityEngine.RectTransform>())
            {
                _builtinTemplate.layer = gameObject.layer;
                Template.SetParent(gameObject.transform, false);
            }

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
