using System;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Toggle" />.</summary>
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
    ///     <!--
    ///         You have to give property name "Path" to Binding and "Name" to x:Reference
    ///         only when you compile the interpreter with IL2CPP.
    ///         It is because ContentPropertyAttribute does not work with IL2CPP.
    ///     -->
    ///     <mu:Toggle Graphic="{Binding Path=Body, Source={x:Reference Name=graphic}}" />
    ///     <m:RectTransform X="{m:Stretch}" Y="{m:Stretch}">
    ///         <mu:Image x:Name="graphic" Color="{m:Color R=0, G=0, B=1}" />
    ///     </m:RectTransform>
    /// </m:RectTransform>
    /// ]]>
    /// </code>
    /// </example>
    public class Toggle : Selectable<UnityEngine.UI.Toggle>
    {
        private static Lazy<UnityEngine.Object> s_builtinBackgroundPrefab = new Lazy<UnityEngine.Object>(LoadBuiltinBackgroundPrefab, false);

        private static UnityEngine.Object LoadBuiltinBackgroundPrefab()
        {
            return UnityEngine.Resources.Load("Mux/Toggle/Background");
        }

        private UnityEngine.GameObject _builtinBackground;

        /// <summary>Backing store for the <see cref="ToggleTransition" /> property.</summary>
        public static readonly BindableProperty ToggleTransitionProperty = CreateBindableBodyProperty<UnityEngine.UI.Toggle.ToggleTransition>(
            "ToggleTransition",
            typeof(Toggle),
            (body, value) => body.toggleTransition = value,
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
        public static readonly BindableProperty GroupProperty = CreateBindableBodyProperty<UnityEngine.UI.ToggleGroup>(
            "Group",
            typeof(Toggle),
            (body, value) => body.group = value);

        /// <summary>Backing store for the <see cref="IsOn" /> property.</summary>
        public static readonly BindableProperty IsOnProperty = CreateBindableBodyProperty<bool>(
            "IsOn",
            typeof(Toggle),
            (body, value) =>
            {
                var old = body.onValueChanged;
                body.onValueChanged = new UnityEngine.UI.Toggle.ToggleEvent();

                try
                {
                    body.isOn = value;
                }
                finally
                {
                    body.onValueChanged = old;
                }
            },
            true,
            BindingMode.TwoWay);

        private static void OnGraphicChanged(BindableObject boxedToggle, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Send(state =>
            {
                var toggle = (Toggle)state;
                var builtinCheckmark = toggle._builtinBackground.transform.GetChild(0).gameObject;

                if (toggle.Graphic == builtinCheckmark.GetComponent<UnityEngine.UI.Image>())
                {
                    toggle._builtinBackground.hideFlags = UnityEngine.HideFlags.None;

                    if (toggle.Body != null)
                    {
                        toggle._builtinBackground.transform.parent?.SetParent(toggle.Body.transform, false);
                        toggle._builtinBackground.layer = toggle.Body.gameObject.layer;
                        builtinCheckmark.layer = toggle._builtinBackground.layer;
                        toggle.SetGraphicToBody(toggle.Graphic);
                    }
                }
                else
                {
                    toggle._builtinBackground.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (toggle.TargetGraphic == toggle._builtinBackground.GetComponent<UnityEngine.UI.Image>())
                    {
                        toggle.SetValueCore(TargetGraphicProperty, null);
                    }

                    if (toggle.Body != null)
                    {
                        toggle._builtinBackground.transform.parent?.SetParent(null);
                        toggle.SetGraphicToBody(toggle.Graphic);
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
        protected override void AwakeInMainThread()
        {
            var builtinCheckmark = _builtinBackground.transform.GetChild(0).gameObject;

            if (Graphic == builtinCheckmark.GetComponent<UnityEngine.UI.Image>())
            {
                _builtinBackground.transform.SetParent(Body.transform, false);
                _builtinBackground.layer = Body.gameObject.layer;
                builtinCheckmark.layer = _builtinBackground.layer;
            }

            Body.toggleTransition = ToggleTransition;
            Body.graphic = Graphic;
            Body.group = Group;
            Body.isOn = IsOn;
            Body.onValueChanged.AddListener(value => SetValueCore(IsOnProperty, value));

            base.AwakeInMainThread();
        }

        private void SetGraphicToBody(UnityEngine.UI.Graphic graphic)
        {
            Body.graphic = graphic;

            // This triggers an effect such as hiding graphic if off to be played.
            Body.group = Group;
        }
    }
}
