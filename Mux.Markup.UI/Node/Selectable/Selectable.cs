using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>An abstract class to represent <see cref="T:UnityEngine.UI.Selectable" /> or its subclass.</summary>
    public abstract class Selectable<T> : Object<T> where T : UnityEngine.UI.Selectable
    {
        /// <summary>Backing store for the <see cref="Interactable" /> property.</summary>
        public static readonly BindableProperty InteractableProperty = CreateBindableComponentProperty<bool>(
            "Interactable",
            typeof(Selectable<T>),
            (component, value) => component.interactable = value,
            true);

        /// <summary>Backing store for the <see cref="TargetGraphic" /> property.</summary>
        public static readonly BindableProperty TargetGraphicProperty = CreateBindableComponentProperty<UnityEngine.UI.Graphic>(
            "TargetGraphic",
            typeof(Selectable<T>),
            (component, value) => component.targetGraphic = value);

        /// <summary>Backing store for the <see cref="Transition" /> property.</summary>
        public static readonly BindableProperty TransitionProperty = CreateBindableComponentProperty<UnityEngine.UI.Selectable.Transition>(
            "Transition",
            typeof(Selectable<T>),
            (component, value) => component.transition = value,
            UnityEngine.UI.Selectable.Transition.ColorTint);

        /// <summary>Backing store for the <see cref="Colors" /> property.</summary>
        public static readonly BindableProperty ColorsProperty = CreateBindableComponentProperty<UnityEngine.UI.ColorBlock>(
            "Colors",
            typeof(Selectable<T>),
            (component, value) => component.colors = value,
            UnityEngine.UI.ColorBlock.defaultColorBlock);

        /// <summary>Backing store for the <see cref="SpriteState" /> property.</summary>
        public static readonly BindableProperty SpriteStateProperty = CreateBindableComponentProperty<UnityEngine.UI.SpriteState>(
            "SpriteState",
            typeof(Selectable<T>),
            (component, value) => component.spriteState = value);

        /// <summary>Backing store for the <see cref="AnimationTriggers" /> property.</summary>
        public static readonly BindableProperty AnimationTriggersProperty = CreateBindableComponentProperty<UnityEngine.UI.AnimationTriggers>(
            "AnimationTriggers",
            typeof(Selectable<T>),
            (component, value) => component.animationTriggers = value,
            null,
            BindingMode.OneWay,
            bindable => new UnityEngine.UI.AnimationTriggers());

        /// <summary>Backing store for the <see cref="Navigation" /> property.</summary>
        public static readonly BindableProperty NavigationProperty = CreateBindableComponentProperty<UnityEngine.UI.Navigation>(
            "Navigation",
            typeof(Selectable<T>),
            (component, value) => component.navigation = value,
            UnityEngine.UI.Navigation.defaultNavigation);

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.interactable" />.</summary>
        public bool Interactable
        {
            get
            {
                return (bool)GetValue(InteractableProperty);
            }

            set
            {
                SetValue(InteractableProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.targetGraphic" />.</summary>
        /// <seealso cref="Graphic{T}" />
        public UnityEngine.UI.Graphic TargetGraphic
        {
            get
            {
                return (UnityEngine.UI.Graphic)GetValue(TargetGraphicProperty);
            }

            set
            {
                SetValue(TargetGraphicProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.transition" />.</summary>
        public UnityEngine.UI.Selectable.Transition Transition
        {
            get
            {
                return (UnityEngine.UI.Selectable.Transition)GetValue(TransitionProperty);
            }

            set
            {
                SetValue(TransitionProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.colors" />.</summary>
        /// <seealso cref="ColorBlock" />
        public UnityEngine.UI.ColorBlock Colors
        {
            get
            {
                return (UnityEngine.UI.ColorBlock)GetValue(ColorsProperty);
            }

            set
            {
                SetValue(ColorsProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.spriteState" />.</summary>
        public UnityEngine.UI.SpriteState SpriteState
        {
            get
            {
                return (UnityEngine.UI.SpriteState)GetValue(SpriteStateProperty);
            }

            set
            {
                SetValue(SpriteStateProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.animationTriggers" />.</summary>
        /// <seealso cref="AnimationTriggers" />
        public UnityEngine.UI.AnimationTriggers AnimationTriggers
        {
            get
            {
                return (UnityEngine.UI.AnimationTriggers)GetValue(AnimationTriggersProperty);
            }

            set
            {
                SetValue(AnimationTriggersProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Selectable.navigation" />.</summary>
        public UnityEngine.UI.Navigation Navigation
        {
            get
            {
                return (UnityEngine.UI.Navigation)GetValue(NavigationProperty);
            }

            set
            {
                SetValue(NavigationProperty, value);
            }
        }

        /// <inheritdoc />
        protected override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            Component = gameObject.AddComponent<T>();
            Component.interactable = Interactable;
            Component.targetGraphic = TargetGraphic;
            Component.transition = Transition;
            Component.colors = Colors;
            Component.spriteState = SpriteState;
            Component.animationTriggers = AnimationTriggers;
            Component.navigation = Navigation;
        }
    }

    /// <summary>A class to represent <see cref="T:UnityEngine.UI.Selectable" />.</summary>
    public class Selectable : Selectable<UnityEngine.UI.Selectable>
    {
        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);
        }
    }
}
