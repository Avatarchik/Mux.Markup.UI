using System;
using Xamarin.Forms;

namespace Mux.Markup
{
    /// <summary>A <see cref="Selectable{T}" /> that represents <see cref="T:UnityEngine.UI.Scrollbar" />.</summary>
    public class Scrollbar : Selectable<UnityEngine.UI.Scrollbar>
    {
        private static Lazy<UnityEngine.Object> s_builtinSlidingAreaPrefab = new Lazy<UnityEngine.Object>(LoadSlidingAreaPrefab, false);

        private static UnityEngine.Object LoadSlidingAreaPrefab()
        {
            return UnityEngine.Resources.Load("Scrollbar/Sliding Area");
        }

        private UnityEngine.GameObject _builtinSlidingArea;

        /// <summary>Backing store for the <see cref="HandleRect" /> property.</summary>
        public static readonly BindableProperty HandleRectProperty = BindableProperty.Create(
            "HandleRect",
            typeof(UnityEngine.RectTransform),
            typeof(Scrollbar),
            null,
            Xamarin.Forms.BindingMode.OneWay,
            null,
            OnHandleRectChanged);

        /// <summary>Backing store for the <see cref="Direction" /> property.</summary>
        public static readonly BindableProperty DirectionProperty = CreateBindableComponentProperty<UnityEngine.UI.Scrollbar.Direction>(
            "Direction",
            typeof(Scrollbar),
            (component, value) => component.direction = value,
            UnityEngine.UI.Scrollbar.Direction.LeftToRight);

        /// <summary>Backing store for the <see cref="Value" /> property.</summary>
        public static readonly BindableProperty ValueProperty = CreateBindableComponentProperty<float>(
            "Value",
            typeof(Scrollbar),
            (component, value) => component.value = value,
            0f,
            BindingMode.TwoWay);

        /// <summary>Backing store for the <see cref="Size" /> property.</summary>
        public static readonly BindableProperty SizeProperty = CreateBindableComponentProperty<float>(
            "Size",
            typeof(Scrollbar),
            (component, value) => component.size = value,
            0.2f);

        /// <summary>Backing store for the <see cref="NumberOfSteps" /> property.</summary>
        public static readonly BindableProperty NumberOfStepsProperty = CreateBindableComponentProperty<int>(
            "NumberOfSteps",
            typeof(Scrollbar),
            (component, value) => component.numberOfSteps = value,
            0);

        private static void OnHandleRectChanged(BindableObject boxedScrollbar, object boxedOldValue, object boxedNewValue)
        {
            Forms.mainThread.Post(state =>
            {
                var scrollbar = (Scrollbar)state;
                var builtinHandleRect = scrollbar._builtinSlidingArea.transform.GetChild(0);

                if (scrollbar.HandleRect == builtinHandleRect)
                {
                    scrollbar._builtinSlidingArea.hideFlags = UnityEngine.HideFlags.None;

                    if (scrollbar.Component != null)
                    {
                        scrollbar._builtinSlidingArea.transform.SetParent(scrollbar.Component.gameObject.transform, false);
                        scrollbar._builtinSlidingArea.layer = scrollbar.Component.gameObject.layer;
                        scrollbar.HandleRect.gameObject.layer = scrollbar._builtinSlidingArea.layer;
                        scrollbar.Component.handleRect = scrollbar.HandleRect;
                    }
                }
                else
                {
                    scrollbar._builtinSlidingArea.hideFlags = UnityEngine.HideFlags.HideInHierarchy;

                    if (scrollbar.TargetGraphic == builtinHandleRect.gameObject.GetComponent<UnityEngine.UI.Image>())
                    {
                        scrollbar.SetValueCore(TargetGraphicProperty, null);
                    }

                    if (scrollbar.Component != null)
                    {
                        scrollbar._builtinSlidingArea.transform.SetParent(null);
                        scrollbar.Component.handleRect = scrollbar.HandleRect;
                    }
                }
            }, boxedScrollbar);
        }

        /// <summary>A property that represents <see cref="T:UnityEngine.UI.Scrollbar.handleRect" />.</summary>
        /// <seealso cref="RectTransform" />
        public UnityEngine.RectTransform HandleRect
        {
            get
            {
                return (UnityEngine.RectTransform)GetValue(HandleRectProperty);
            }

            set
            {
                SetValue(HandleRectProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="T:UnityEngine.UI.Scrollbar.direction" />.</summary>
        /// <seealso cref="RectTransform" />
        public UnityEngine.UI.Scrollbar.Direction Direction
        {
            get
            {
                return (UnityEngine.UI.Scrollbar.Direction)GetValue(DirectionProperty);
            }

            set
            {
                SetValue(DirectionProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Scrollbar.value" />.</summary>
        public float Value
        {
            get
            {
                return (float)GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Scrollbar.size" />.</summary>
        public float Size
        {
            get
            {
                return (float)GetValue(SizeProperty);
            }

            set
            {
                SetValue(SizeProperty, value);
            }
        }

        /// <summary>A property that represents <see cref="P:UnityEngine.UI.Scrollbar.numberOfSteps" />.</summary>
        public int NumberOfSteps
        {
            get
            {
                return (int)GetValue(NumberOfStepsProperty);
            }

            set
            {
                SetValue(NumberOfStepsProperty, value);
            }
        }

        public Scrollbar()
        {
            Forms.mainThread.Send(state =>
            {
                _builtinSlidingArea = (UnityEngine.GameObject)UnityEngine.Object.Instantiate(s_builtinSlidingAreaPrefab.Value);
                var builtinHandleRect = _builtinSlidingArea.transform.GetChild(0);
                SetValueCore(HandleRectProperty, builtinHandleRect);
                SetValueCore(TargetGraphicProperty, builtinHandleRect.gameObject.GetComponent<UnityEngine.UI.Image>());
            }, null);
        }

        /// <inheritdoc />
        protected sealed override void AddToInMainThread(UnityEngine.GameObject gameObject)
        {
            base.AddToInMainThread(gameObject);

            if (HandleRect == _builtinSlidingArea.transform.GetChild(0))
            {
                _builtinSlidingArea.transform.SetParent(gameObject.transform, false);
                _builtinSlidingArea.layer = gameObject.layer;
                HandleRect.gameObject.layer = _builtinSlidingArea.layer;
            }

            Component.handleRect = HandleRect;
            Component.direction = Direction;
            Component.value = Value;
            Component.size = Size;
            Component.numberOfSteps = NumberOfSteps;
            Component.onValueChanged.AddListener(newValue => SetValueCore(ValueProperty, newValue));
        }
    }
}
