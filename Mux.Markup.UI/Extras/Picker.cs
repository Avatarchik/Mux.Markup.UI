using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Mux.Markup.Extras
{
    /// <summary>
    /// A component that represents a group of <see cref="Picker{T}">Pickers</see>.
    /// </summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:system="clr-namespace:System;assembly=mscorlib"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mue:PickerGroup x:Name="group" x:TypeArguments="system:Byte" />
    ///     <mu:VerticalLayoutGroup />
    ///     <m:RectTransform>
    ///         <!--
    ///             You have to give property name "Name" to x:Reference
    ///             only when you compile the interpreter with IL2CPP.
    ///             It is because ContentPropertyAttribute does not work with IL2CPP.
    ///         -->
    ///         <mue:Picker
    ///             x:TypeArguments="system:Byte"
    ///             PickerGroup="{x:Reference Name=group}"
    ///             Value="0" />
    ///     </m:RectTransform>
    ///     <m:RectTransform>
    ///         <mue:Picker
    ///             x:TypeArguments="system:Byte"
    ///             PickerGroup="{x:Reference Name=group}"
    ///             Value="1" />
    ///     </m:RectTransform>
    ///     <m:RectTransform>
    ///         <!-- Specifying "Path" property name for the same reason -->
    ///         <mu:Text Content="{Binding Path=Value, Source={x:Reference Name=group}}" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class PickerGroup<T> : ToggleGroup
    {
        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            "Value",
            typeof(T),
            typeof(PickerGroup<T>),
            default(T),
            BindingMode.TwoWay);

        /// <summary>A property that represents the picked value.</summary>
        public T Value
        {
            get
            {
                return (T)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }
    }

    /// <summary>An component that represents a value to pick.</summary>
    /// <example>
    /// <code language="xaml">
    /// <![CDATA[
    /// <m:RectTransform
    ///     xmlns="http://xamarin.com/schemas/2014/forms"
    ///     xmlns:m="clr-namespace:Mux.Markup;assembly=Mux.Markup"
    ///     xmlns:mu="clr-namespace:Mux.Markup;assembly=Mux.Markup.UI"
    ///     xmlns:mue="clr-namespace:Mux.Markup.Extras;assembly=Mux.Markup.UI"
    ///     xmlns:system="clr-namespace:System;assembly=mscorlib"
    ///     xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml">
    ///     <m:StandaloneInputModule />
    ///     <mu:EventSystem />
    ///     <mu:Canvas />
    ///     <mu:CanvasScaler UiScale="{mu:ConstantPhysicalSize}" />
    ///     <mu:GraphicRaycaster />
    ///     <mue:PickerGroup x:Name="group" x:TypeArguments="system:Byte" />
    ///     <mu:VerticalLayoutGroup />
    ///     <m:RectTransform>
    ///         <!--
    ///             You have to give property name "Name" to x:Reference
    ///             only when you compile the interpreter with IL2CPP.
    ///             It is because ContentPropertyAttribute does not work with IL2CPP.
    ///         -->
    ///         <mue:Picker
    ///             x:TypeArguments="system:Byte"
    ///             PickerGroup="{x:Reference Name=group}"
    ///             Value="0" />
    ///     </m:RectTransform>
    ///     <m:RectTransform>
    ///         <mue:Picker
    ///             x:TypeArguments="system:Byte"
    ///             PickerGroup="{x:Reference Name=group}"
    ///             Value="1" />
    ///     </m:RectTransform>
    ///     <m:RectTransform>
    ///         <!-- Specifying "Path" property name for the same reason -->
    ///         <mu:Text Content="{Binding Path=Value, Source={x:Reference Name=group}}" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Picker<T> : Toggle
    {
        /// <summary>Backing store for the <see cref="PickerGroup" /> property.</summary>
        public static readonly BindableProperty PickerGroupProperty = BindableProperty.Create(
            "PickerGroup",
            typeof(PickerGroup<T>),
            typeof(Picker<T>),
            null,
            BindingMode.OneWay,
            null,
            OnPickerGroupChanged);

        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            "Value",
            typeof(T),
            typeof(Picker<T>),
            default(T),
            BindingMode.OneWay,
            null,
            OnValueChanged);

        private static void OnPickerGroupChanged(BindableObject sender, object oldValue, object newValue)
        {
            var picker = (Picker<T>)sender;

            if (oldValue != null)
            {
                ((PickerGroup<T>)oldValue).PropertyChanged -= picker.OnGroupPropertyChanged;
            }

            if (newValue != null)
            {
                var group = (PickerGroup<T>)newValue;
                var isOn = EqualityComparer<T>.Default.Equals(picker.Value, group.Value);

                group.PropertyChanged += picker.OnGroupPropertyChanged;
                picker.SetValueCore(GroupProperty, group.Component);
                picker.SetValueCore(IsOnProperty, isOn);
            }
        }

        private static void OnValueChanged(BindableObject sender, object oldValue, object newValue)
        {
            var picker = (Picker<T>)sender;

            if (picker.PickerGroup != null)
            {
                var isOn = EqualityComparer<T>.Default.Equals(picker.PickerGroup.Value, (T)newValue);
                picker.SetValueCore(IsOnProperty, isOn);
            }
        }

        /// <summary>Group the picker belongs to.</summary>
        public PickerGroup<T> PickerGroup
        {
            get
            {
                return (PickerGroup<T>)GetValue(PickerGroupProperty);
            }

            set
            {
                SetValue(PickerGroupProperty, value);
            }
        }

        /// <summary>Value the picker represents.</summary>
        public T Value
        {
            get
            {
                return (T)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);

            if (PickerGroup == null)
            {
                return;
            }

            switch (name)
            {
                case "IsOn":
                    if (IsOn)
                    {
                        PickerGroup.SetValueCore(PickerGroup<T>.ValueProperty, Value);
                    }
                    break;

                case "Group":
                    if (Group != PickerGroup.Component)
                    {
                        SetValueCore(GroupProperty, PickerGroup.Component);
                        throw new Exception("setting Group property of Picker is prohibited");
                    }
                    break;
            }
        }

        internal override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();

            if (PickerGroup != null)
            {
                PickerGroup.PropertyChanged -= OnGroupPropertyChanged;
            }
        }

        private void OnGroupPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var group = (PickerGroup<T>)sender;

            switch (args.PropertyName)
            {
                case "Component":
                    SetValueCore(GroupProperty, group.Component);
                    break;

                case "Value":
                    var isOn = EqualityComparer<T>.Default.Equals(group.Value, Value);
                    SetValueCore(IsOnProperty, isOn);
                    break;
            }
        }
    }
}
