using System;
using System.Runtime.InteropServices;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An <see cref="Object{T}" /> that represents <see cref="T:UnityEngine.UI.ScrollRect" />.</summary>
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
    ///     <m:RectTransform x:Name="viewport">
    ///         <mu:RectMask2D />
    ///         <m:RectTransform x:Name="content" X="{m:Sized SizeDelta=999}" Y="{m:Sized SizeDelta=999}">
    ///             <mue:UIMesh>
    ///                 <mue:UIMesh.Items>
    ///                     <mue:UIVertexTriangleStream>
    ///                         <mue:UIVertexTriangleStream.Verts>
    ///                             <m:UIVertex Color="{m:Color R=0, G=0, B=1}" Position="{m:Vector3 X=-0.5, Y=-0.5, Z=0}" />
    ///                             <m:UIVertex Color="{m:Color R=0, G=1, B=0}" Position="{m:Vector3 X=0, Y=0.5, Z=0}" />
    ///                             <m:UIVertex Color="{m:Color R=1, G=0, B=0}" Position="{m:Vector3 X=0.5, Y=-0.5, Z=0}" />
    ///                         </mue:UIVertexTriangleStream.Verts>
    ///                     </mue:UIVertexTriangleStream>
    ///                 </mue:UIMesh.Items>
    ///             </mue:UIMesh>
    ///         </m:RectTransform>
    ///     </m:RectTransform>
    ///     <m:RectTransform X="{m:Stretch}" Y="{m:Sized Anchor=0, Pivot=0, SizeDelta=15}">
    ///         <mu:Scrollbar x:Name="horizontalScrollbar" Direction="LeftToRight" />
    ///     </m:RectTransform>
    ///     <m:RectTransform X="{m:Sized Anchor=1, Pivot=1, SizeDelta=15}" Y="{m:Stretch}">
    ///         <mu:Scrollbar x:Name="verticalScrollbar" Direction="BottomToTop" />
    ///     </m:RectTransform>
    ///     <mu:ScrollRect
    ///         HorizontalScrollbar="{Binding Path=Component, Source={x:Reference Name=horizontalScrollbar}}"
    ///         HorizontalScrollbarSpacing="15"
    ///         VerticalScrollbar="{Binding Path=Component, Source={x:Reference Name=verticalScrollbar}}"
    ///         VerticalScrollbarSpacing="15"
    ///         Viewport="{Binding Path=Component, Source={x:Reference Name=viewport}}"
    ///         Content="{Binding Path=Component, Source={x:Reference Name=content}}" />
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class ScrollRect : Object<UnityEngine.UI.ScrollRect>
    {
        [StructLayout(LayoutKind.Auto)]
        private readonly struct BuiltinScrollbarPrefabs
        {
            public readonly UnityEngine.ResourceRequest horizontal;
            public readonly UnityEngine.ResourceRequest vertical;

            public BuiltinScrollbarPrefabs(UnityEngine.ResourceRequest horizontal, UnityEngine.ResourceRequest vertical)
            {
                this.horizontal = horizontal;
                this.vertical = vertical;
            }
        }

        private static readonly Lazy<BuiltinScrollbarPrefabs> s_builtinScrollbarPrefabs = new Lazy<BuiltinScrollbarPrefabs>(LoadBuiltinScrollbarPrefabs, false);

        private static BuiltinScrollbarPrefabs LoadBuiltinScrollbarPrefabs()
        {
            return new BuiltinScrollbarPrefabs(
                UnityEngine.Resources.LoadAsync("Mux/ScrollRect/Scrollbar Horizontal"),
                UnityEngine.Resources.LoadAsync("Mux/ScrollRect/Scrollbar Vertical"));
        }

        private UnityEngine.GameObject _builtinHorizontalScrollbar;
        private UnityEngine.GameObject _builtinVerticalScrollbar;

        /// <summary>Backing store for the <see cref="Content" /> property.</summary>
        public static readonly BindableProperty ContentProperty = CreateBindableComponentProperty<UnityEngine.RectTransform>(
            "Content",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.content = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            });

        /// <summary>Backing store for the <see cref="Horizontal" /> property.</summary>
        public static readonly BindableProperty HorizontalProperty = CreateBindableComponentProperty<bool>(
            "Horizontal",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.horizontal = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            true);

        /// <summary>Backing store for the <see cref="Vertical" /> property.</summary>
        public static readonly BindableProperty VerticalProperty = CreateBindableComponentProperty<bool>(
            "Vertical",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.vertical = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            true);

        /// <summary>Backing store for the <see cref="Movement" /> property.</summary>
        public static readonly BindableProperty MovementProperty = CreateBindableModifierProperty(
            "Movement",
            typeof(ScrollRect),
            sender => new Elastic());

        /// <summary>Backing store for the <see cref="Inertia" /> property.</summary>
        public static readonly BindableProperty InertiaProperty = BindableProperty.Create(
            "Inertia",
            typeof(Modifier),
            typeof(ScrollRect),
            null,
            BindingMode.OneWay,
            null,
            OnInertiaChanged,
            null,
            null,
            sender => new Inertia());

        /// <summary>Backing store for the <see cref="ScrollSensitivity" /> property.</summary>
        public static readonly BindableProperty ScrollSensitivityProperty = CreateBindableComponentProperty<float>(
            "ScrollSensitivity",
            typeof(ScrollRect),
            (component, value) => component.scrollSensitivity = value,
            1.0f);

        /// <summary>Backing store for the <see cref="Viewport" /> property.</summary>
        public static readonly BindableProperty ViewportProperty = CreateBindableComponentProperty<UnityEngine.RectTransform>(
            "Viewport",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.viewport = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            });

        /// <summary>Backing store for the <see cref="HorizontalScrollbar" /> property.</summary>
        public static readonly BindableProperty HorizontalScrollbarProperty = BindableProperty.Create(
            "HorizontalScrollbar",
            typeof(UnityEngine.UI.Scrollbar),
            typeof(ScrollRect),
            null,
            BindingMode.OneWay,
            null,
            OnHorizontalScrollbarChanged);

        /// <summary>Backing store for the <see cref="VerticalScrollbar" /> property.</summary>
        public static readonly BindableProperty VerticalScrollbarProperty = BindableProperty.Create(
            "VerticalScrollbar",
            typeof(UnityEngine.UI.Scrollbar),
            typeof(ScrollRect),
            null,
            BindingMode.OneWay,
            null,
            OnVerticalScrollbarChanged);

        /// <summary>Backing store for the <see cref="HorizontalScrollbarVisibility" /> property.</summary>
        public static readonly BindableProperty HorizontalScrollbarVisibilityProperty = CreateBindableComponentProperty<UnityEngine.UI.ScrollRect.ScrollbarVisibility>(
            "HorizontalScrollbarVisibility",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.horizontalScrollbarVisibility = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);

        /// <summary>Backing store for the <see cref="VerticalScrollbarVisibility" /> property.</summary>
        public static readonly BindableProperty VerticalScrollbarVisibilityProperty = CreateBindableComponentProperty<UnityEngine.UI.ScrollRect.ScrollbarVisibility>(
            "VerticalScrollbarVisibility",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.verticalScrollbarVisibility = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);

        /// <summary>Backing store for the <see cref="HorizontalScrollbarSpacing" /> property.</summary>
        public static readonly BindableProperty HorizontalScrollbarSpacingProperty = CreateBindableComponentProperty<float>(
            "HorizontalScrollbarSpacing",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.horizontalScrollbarSpacing = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            -3f);

        /// <summary>Backing store for the <see cref="VerticalScrollbarSpacing" /> property.</summary>
        public static readonly BindableProperty VerticalScrollbarSpacingProperty = CreateBindableComponentProperty<float>(
            "VerticalScrollbarSpacing",
            typeof(ScrollRect),
            (component, value) =>
            {
                component.verticalScrollbarSpacing = value;
                UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
            },
            -3f);

        /// <summary>Backing store for the <see cref="NormalizedPosition" /> property.</summary>
        public static readonly BindableProperty NormalizedPositionProperty = CreateBindableComponentProperty<UnityEngine.Vector2>(
            "NormalizedPosition",
            typeof(ScrollRect),
            (component, value) =>
            {
                var old = component.onValueChanged;
                component.onValueChanged = new UnityEngine.UI.ScrollRect.ScrollRectEvent();
                component.onValueChanged.AddListener(newValue => component.onValueChanged = old);
                component.normalizedPosition = value;
            },
            UnityEngine.Vector2.zero,
            BindingMode.TwoWay);

        private static void OnInertiaChanged(BindableObject sender, object oldValue, object newValue)
        {
            ((Modifier)oldValue)?.DestroyMux();

            var component = ((ScrollRect)sender).Component;

            if (component != null)
            {
                Forms.mainThread.Send(state => component.inertia = state != null, newValue);
            }

            if (newValue != null)
            {
                var modifier = (Modifier)newValue;
                modifier.SetBinding(Modifier.ComponentProperty, new Binding("Component"));
                SetInheritedBindingContext(modifier, sender.BindingContext);
            }
        }

        private static void OnHorizontalScrollbarChanged(BindableObject boxedSender, object boxedOldValue, object boxedNewValue)
        {
            var sender = (ScrollRect)boxedSender;
            var component = sender.Component;

            Forms.mainThread.Send(state =>
            {
                var newValue = (UnityEngine.UI.Scrollbar)state;

                if (newValue == sender._builtinHorizontalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>())
                {
                    sender._builtinHorizontalScrollbar.hideFlags = UnityEngine.HideFlags.None;

                    if (component != null)
                    {
                        var child = sender._builtinVerticalScrollbar.transform.GetChild(0);

                        sender._builtinHorizontalScrollbar.transform.SetParent(component.gameObject.transform, false);
                        sender._builtinHorizontalScrollbar.layer = component.gameObject.layer;
                        child.gameObject.layer = component.gameObject.layer;
                        child.GetChild(0).gameObject.layer = component.gameObject.layer;
                        component.horizontalScrollbar = newValue;
                        UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
                    }
                }
                else
                {
                    sender._builtinHorizontalScrollbar.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (component != null)
                    {
                        sender._builtinHorizontalScrollbar.transform.SetParent(null);
                        component.horizontalScrollbar = newValue;
                        UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
                    }
                }
            }, boxedNewValue);
        }

        private static void OnVerticalScrollbarChanged(BindableObject boxedSender, object boxedOldValue, object boxedNewValue)
        {
            var sender = (ScrollRect)boxedSender;
            var component = sender.Component;

            Forms.mainThread.Send(state =>
            {
                var newValue = (UnityEngine.UI.Scrollbar)state;

                if (newValue == sender._builtinVerticalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>())
                {
                    sender._builtinVerticalScrollbar.hideFlags = UnityEngine.HideFlags.None;

                    if (component != null)
                    {
                        var child = sender._builtinVerticalScrollbar.transform.GetChild(0);

                        sender._builtinVerticalScrollbar.transform.SetParent(component.gameObject.transform, false);
                        sender._builtinVerticalScrollbar.layer = component.gameObject.layer;
                        child.gameObject.layer = component.gameObject.layer;
                        child.GetChild(0).gameObject.layer = component.gameObject.layer;
                        component.verticalScrollbar = newValue;
                        UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
                    }
                }
                else
                {
                    sender._builtinVerticalScrollbar.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (component != null)
                    {
                        sender._builtinVerticalScrollbar.transform.SetParent(null);
                        component.verticalScrollbar = newValue;
                        UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(component);
                    }
                }
            }, boxedNewValue);
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.content" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        /// <seealso cref="RectTransform" />
        public UnityEngine.RectTransform Content
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(ContentProperty);
            }

            set
            {
                SetValue(ContentProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.horizontal" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public bool Horizontal
        {
            get
            {
                return (bool)GetValue(HorizontalProperty);
            }

            set
            {
                SetValue(HorizontalProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.vertical" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public bool Vertical
        {
            get
            {
                return (bool)GetValue(VerticalProperty);
            }

            set
            {
                SetValue(VerticalProperty, value);
            }
        }

        /// <summary>A property that represents the movement of <see cref="T:UnityEngine.UI.ScrollRect" />.</summary>
        /// <remarks>Setting <see cref="Object{T:UnityEngine.UI.ScrollRect}.Modifier" /> to this property binds its lifetime to the lifetime of this object.</remarks>
        /// <seealso cref="Unrestricted" />
        /// <seealso cref="Elastic" />
        /// <seealso cref="Clamped" />
        public Modifier Movement
        {
            get
            {
                return (Modifier)GetValue(MovementProperty);
            }

            set
            {
                SetValue(MovementProperty, value);
            }
        }

        /// <summary>A property that represents the movement inertia of <see cref="T:UnityEngine.UI.ScrollRect" />.</summary>
        /// <remarks>Setting <see cref="Object{T:UnityEngine.UI.ScrollRect}.Modifier" /> to this property binds its lifetime to the lifetime of this object.</remarks>
        /// <seealso cref="Inertia" />
        public Modifier Inertia
        {
            get
            {
                return (Modifier)GetValue(InertiaProperty);
            }

            set
            {
                SetValue(InertiaProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.scrollSensitivity" />.</summary>
        public float ScrollSensitivity
        {
            get
            {
                return (float)GetValue(ScrollSensitivityProperty);
            }

            set
            {
                SetValue(ScrollSensitivityProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.viewport" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        /// <seealso cref="RectTransform" />
        public UnityEngine.RectTransform Viewport
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(ViewportProperty);
            }

            set
            {
                SetValue(ViewportProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.horizontalScrollbar" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        /// <seealso cref="Scrollbar" />
        public UnityEngine.UI.Scrollbar HorizontalScrollbar
        {
            get
            {
                return (UnityEngine.UI.Scrollbar)GetValue(HorizontalScrollbarProperty);
            }

            set
            {
                SetValue(HorizontalScrollbarProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.verticalScrollbar" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        /// <seealso cref="Scrollbar" />
        public UnityEngine.UI.Scrollbar VerticalScrollbar
        {
            get
            {
                return (UnityEngine.UI.Scrollbar)GetValue(VerticalScrollbarProperty);
            }

            set
            {
                SetValue(VerticalScrollbarProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.horizontalScrollbarVisibility" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public UnityEngine.UI.ScrollRect.ScrollbarVisibility HorizontalScrollbarVisibility
        {
            get
            {
                return (UnityEngine.UI.ScrollRect.ScrollbarVisibility)GetValue(HorizontalScrollbarVisibilityProperty);
            }

            set
            {
                SetValue(HorizontalScrollbarVisibilityProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.verticalScrollbarVisibility" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public UnityEngine.UI.ScrollRect.ScrollbarVisibility VerticalScrollbarVisibility
        {
            get
            {
                return (UnityEngine.UI.ScrollRect.ScrollbarVisibility)GetValue(VerticalScrollbarVisibilityProperty);
            }

            set
            {
                SetValue(VerticalScrollbarVisibilityProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.horizontalScrollbarSpacing" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public float HorizontalScrollbarSpacing
        {
            get
            {
                return (float)GetValue(HorizontalScrollbarSpacingProperty);
            }

            set
            {
                SetValue(HorizontalScrollbarSpacingProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.verticalScrollbarSpacing" />.</summary>
        /// <remarks>Setting a value to this property rebuilds the layout.</remarks>
        public float VerticalScrollbarSpacing
        {
            get
            {
                return (float)GetValue(VerticalScrollbarSpacingProperty);
            }

            set
            {
                SetValue(VerticalScrollbarSpacingProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.ScrollRect.normalizedPosition" />.</summary>
        /// <seealso cref="Vector2" />
        public UnityEngine.Vector2 NormalizedPosition
        {
            get
            {
                return (UnityEngine.Vector2)GetValue(NormalizedPositionProperty);
            }

            set
            {
                SetValue(NormalizedPositionProperty, value);
            }
        }

        public ScrollRect()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinHorizontalScrollbar =
                    (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinScrollbarPrefabs.Value.horizontal.asset);

                _builtinVerticalScrollbar =
                    (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinScrollbarPrefabs.Value.vertical.asset);

                SetValueCore(HorizontalScrollbarProperty, _builtinHorizontalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>());
                SetValueCore(VerticalScrollbarProperty, _builtinVerticalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>());
            }, null);
        }

        /// <inheritdoc />
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            SetInheritedBindingContext(Movement, BindingContext);
            SetInheritedBindingContext(Inertia, BindingContext);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<UnityEngine.UI.ScrollRect>();
            Component.horizontal = Horizontal;
            Component.vertical = Vertical;
            Component.inertia = Inertia != null;
            Component.scrollSensitivity = ScrollSensitivity;
            Component.viewport = Viewport;
            Component.content = Content;
            Component.horizontalScrollbar = HorizontalScrollbar;
            Component.verticalScrollbar = VerticalScrollbar;
            Component.horizontalScrollbarVisibility = HorizontalScrollbarVisibility;
            Component.verticalScrollbarVisibility = VerticalScrollbarVisibility;
            Component.horizontalScrollbarSpacing = HorizontalScrollbarSpacing;
            Component.verticalScrollbarSpacing = VerticalScrollbarSpacing;
            Component.onValueChanged.AddListener(value => SetValueCore(NormalizedPositionProperty, value));

            if (Content != null)
            {
                Component.normalizedPosition = NormalizedPosition;
            }

            if (HorizontalScrollbar == _builtinHorizontalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>())
            {
                var child = _builtinHorizontalScrollbar.transform.GetChild(0);
                _builtinHorizontalScrollbar.transform.SetParent(gameObject.transform, false);
                _builtinHorizontalScrollbar.layer = gameObject.layer;
                child.gameObject.layer = gameObject.layer;
                child.GetChild(0).gameObject.layer = gameObject.layer;
            }

            if (VerticalScrollbar == _builtinVerticalScrollbar.GetComponent<UnityEngine.UI.Scrollbar>())
            {
                var child = _builtinVerticalScrollbar.transform.GetChild(0);
                _builtinVerticalScrollbar.transform.SetParent(gameObject.transform, false);
                _builtinVerticalScrollbar.layer = gameObject.layer;
                child.gameObject.layer = gameObject.layer;
                child.GetChild(0).gameObject.layer = gameObject.layer;
            }

            Movement.Component = Component;

            if (Inertia != null)
            {
                Inertia.Component = Component;
            }

            UnityEngine.UI.CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(Component);
        }

        internal override void DestroyMuxInMainThread()
        {
            base.DestroyMuxInMainThread();
            Movement.DestroyMux();
            Inertia?.DestroyMux();
        }
    }
}
