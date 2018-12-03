using System;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Toggle" />.</summary>
    public class Toggle : Selectable<UnityEngine.UI.Toggle>
    {
        private static Lazy<UnityEngine.Object> s_builtinBackgroundPrefab = new Lazy<UnityEngine.Object>(LoadBuiltinBackgroundPrefab, false);

        private static UnityEngine.Object LoadBuiltinBackgroundPrefab()
        {
            return UnityEngine.Resources.Load("Toggle/Background");
        }

        private UnityEngine.GameObject _builtinBackground;

        /// <summary>Backing store for the <see cref="ToggleTransition" /> property.</summary>
        public static readonly BindableProperty ToggleTransitionProperty = CreateBindableComponentProperty<UnityEngine.UI.Toggle.ToggleTransition>(
            "ToggleTransition",
            typeof(Toggle),
            (component, value) => component.toggleTransition = value,
            UnityEngine.UI.Toggle.ToggleTransition.Fade);

        /// <summary>Backing store for the <see cref="Graphic" /> property.</summary>
        public static readonly BindableProperty GraphicProperty = BindableProperty.Create(
            "Graphic",
            typeof(UnityEngine.UI.Graphic),
            typeof(Toggle),
            null,
            BindingMode.OneWay,
            null,
            OnGraphicChanged);

        /// <summary>Backing store for the <see cref="Group" /> property.</summary>
        public static readonly BindableProperty GroupProperty = CreateBindableComponentProperty<UnityEngine.UI.ToggleGroup>(
            "Group",
            typeof(Toggle),
            (component, value) => component.group = value);

        /// <summary>Backing store for the <see cref="IsOn" /> property.</summary>
        public static readonly BindableProperty IsOnProperty = CreateBindableComponentProperty<bool>(
            "IsOn",
            typeof(Toggle),
            (component, value) =>
            {
                var old = component.onValueChanged;
                component.onValueChanged = new UnityEngine.UI.Toggle.ToggleEvent();

                try
                {
                    component.isOn = value;
                }
                finally
                {
                    component.onValueChanged = old;
                }
            },
            true,
            BindingMode.TwoWay);

        private static void OnGraphicChanged(BindableObject boxedToggle, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Post(state =>
            {
                var toggle = (Toggle)state;
                var builtinCheckmark = toggle._builtinBackground.transform.GetChild(0).gameObject;

                if (toggle.Graphic == builtinCheckmark.GetComponent<UnityEngine.UI.Image>())
                {
                    toggle._builtinBackground.hideFlags = UnityEngine.HideFlags.None;

                    if (toggle.Component != null)
                    {
                        toggle._builtinBackground.transform.parent?.SetParent(toggle.Component.transform, false);
                        toggle._builtinBackground.layer = toggle.Component.gameObject.layer;
                        builtinCheckmark.layer = toggle._builtinBackground.layer;
                        toggle.SetGraphicToComponent(toggle.Graphic);
                    }
                }
                else
                {
                    toggle._builtinBackground.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (toggle.TargetGraphic == toggle._builtinBackground.GetComponent<UnityEngine.UI.Image>())
                    {
                        toggle.SetValueCore(TargetGraphicProperty, null);
                    }

                    if (toggle.Component != null)
                    {
                        toggle._builtinBackground.transform.parent?.SetParent(null);
                        toggle.SetGraphicToComponent(toggle.Graphic);
                    }
                }
            }, boxedToggle);
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Toggle.toggleTransition" />.</summary>
        public UnityEngine.UI.Toggle.ToggleTransition ToggleTransition
        {
            get
            {
                return (UnityEngine.UI.Toggle.ToggleTransition)GetValue(ToggleTransitionProperty);
            }

            set
            {
                SetValue(ToggleTransitionProperty, value);
            }
        }

        /// <summary>A property that represents <see href="P:UnityEngine.UI.Toggle.graphic" />.</summary>
        /// <seealso cref="Graphic" />
        public UnityEngine.UI.Graphic Graphic
        {
            get
            {
                return (UnityEngine.UI.Graphic)GetValue(GraphicProperty);
            }

            set
            {
                SetValue(GraphicProperty, value);
            }
        }

        /// <summary>A property that represents <see href="P:UnityEngine.UI.Toggle.group" />.</summary>
        /// <seealso cref="ToggleGroup" />
        public UnityEngine.UI.ToggleGroup Group
        {
            get
            {
                return (UnityEngine.UI.ToggleGroup)GetValue(GroupProperty);
            }

            set
            {
                SetValue(GroupProperty, value);
            }
        }

        /// <summary>A property that represents <see href="P:UnityEngine.UI.Toggle.isOn" />.</summary>
        public bool IsOn
        {
            get
            {
                return (bool)GetValue(IsOnProperty);
            }

            set
            {
                SetValue(IsOnProperty, value);
            }
        }

        public Toggle()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinBackground = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinBackgroundPrefab.Value);
                SetValueCore(GraphicProperty, _builtinBackground.transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Image>());
                SetValueCore(TargetGraphicProperty, _builtinBackground.GetComponent<UnityEngine.UI.Image>());
            }, null);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            var builtinCheckmark = _builtinBackground.transform.GetChild(0).gameObject;

            if (Graphic == builtinCheckmark.GetComponent<UnityEngine.UI.Image>())
            {
                _builtinBackground.transform.SetParent(gameObject.transform, false);
                _builtinBackground.layer = gameObject.layer;
                builtinCheckmark.layer = _builtinBackground.layer;
            }

            Component.toggleTransition = ToggleTransition;
            Component.graphic = Graphic;
            Component.group = Group;
            Component.isOn = IsOn;
            Component.onValueChanged.AddListener(value => SetValueCore(IsOnProperty, value));
        }

        private void SetGraphicToComponent(UnityEngine.UI.Graphic graphic)
        {
            Component.graphic = graphic;

            // This triggers an effect such as hiding graphic if off to be played.
            Component.group = Group;
        }
    }
}
