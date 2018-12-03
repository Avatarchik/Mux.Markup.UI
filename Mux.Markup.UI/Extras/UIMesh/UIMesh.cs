using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Mux.Markup.Extras
{
    /// <summary>
    /// A class that allows creating or modifying meshes with XAML and data binding.
    /// </summary>
    [ContentProperty("Items")]
    public class UIMesh : Graphic<MuxUIMesh>
    {
        /// <summary>Backing store for the <see cref="Items" /> property.</summary>
        public static readonly BindableProperty ItemsProperty = BindableProperty.CreateReadOnly(
            "Items",
            typeof(ICollection<UIMeshItem>),
            typeof(UIMesh),
            null,
            BindingMode.OneWayToSource,
            null,
            null,
            null,
            null,
            CreateDefaultItems).BindableProperty;

        /// <summary>Backing store for the <see cref="ItemsSource" /> property.</summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(
            "ItemsSource",
            typeof(IEnumerable),
            typeof(UIMesh),
            null,
            BindingMode.OneWay,
            null,
            OnItemsSourceChanged);

        /// <summary>Backing store for the <see cref="ItemTemplate" /> property.</summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
            "ItemTemplate",
            typeof(DataTemplate),
            typeof(UIMesh),
            null,
            BindingMode.OneWay,
            null,
            OnItemTemplateChanged);

        private static object CreateDefaultItems(BindableObject sender)
        {
            return ((UIMesh)sender)._items;
        }

        private static void OnItemsSourceChanged(BindableObject sender, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                ((UIMesh)sender)._items.ChangeSource((IEnumerable)newValue);
            }
        }

        private static void OnItemTemplateChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((UIMesh)sender)._items.ChangeTemplate((DataTemplate)newValue);
        }

        private readonly UIMeshItemCollection _items;

        /// <summary>A property that represents a collection of <see cref="UIMeshItem" />.</summary>
        /// <remarks>This is the content property; you can write as child elements in XAML.</remarks>
        public ICollection<UIMeshItem> Items => (ICollection<UIMeshItem>)GetValue(ItemsProperty);

        /// <summary>Gets or sets the source of entries to template and add.</summary>
        public IEnumerable ItemsSource
        {
            get
            {
                return (IEnumerable)GetValue(ItemsSourceProperty);
            }

            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="DataTemplate" /> to apply to the <see cref="ItemsSource" />.
        /// </summary>
        public DataTemplate ItemTemplate
        {
            get
            {
                return (DataTemplate)GetValue(ItemTemplateProperty);
            }

            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }

        public UIMesh()
        {
            _items = new UIMeshItemCollection(this);
            _items.ChangeSource(ItemsSource);
            _items.ChangeTemplate(ItemTemplate);
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var item in _items)
            {
                SetInheritedBindingContext(item, BindingContext);
            }
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<MuxUIMesh>();
            Component.items = _items;
        }
    }

    /// <summary>Backing implementation for <see cref="UIMesh" />.</summary>
    public sealed class MuxUIMesh : UnityEngine.UI.MaskableGraphic
    {
        internal IEnumerable<UIMeshItem> items;

        /// <inheritdoc />
        protected override void OnPopulateMesh(UnityEngine.UI.VertexHelper helper)
        {
            helper.Clear();

            foreach (var item in items)
            {
                item.AddTo(helper);
            }
        }
    }
}
